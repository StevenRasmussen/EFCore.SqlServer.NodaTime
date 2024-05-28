using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Microsoft.EntityFrameworkCore.SqlServer.Query.ExpressionTranslators
{
    public abstract class BaseNodaTimeMethodCallTranslator : IMethodCallTranslator
    {
        private readonly ISqlExpressionFactory _sqlExpressionFactory;

        private readonly Dictionary<MethodInfo, string> _methodInfoDateAddMapping;
        private readonly Dictionary<MethodInfo, string> _methodInfoDateAddExtensionMapping;
        private readonly Dictionary<MethodInfo, string> _methodInfoDatePartExtensionMapping;
        private readonly Dictionary<MethodInfo, string> _methodInfoDateDiffMapping;
        private readonly Dictionary<MethodInfo, string> _methodInfoDateDiffBigMapping;
        private readonly Dictionary<MethodInfo, string> _methodInfoContainsMapping;

        protected static MethodInfo ContainsMethod { get; } = typeof(Enumerable).GetMethods()
                .First(x => x.Name == nameof(Enumerable.Contains) && x.GetParameters().Count() == 2);

        public BaseNodaTimeMethodCallTranslator(
            ISqlExpressionFactory sqlExpressionFactory,
            Dictionary<MethodInfo, string> methodInfoDateAddMapping,
            Dictionary<MethodInfo, string> methodInfoDateAddExtensionMapping,
            Dictionary<MethodInfo, string> methodInfoDatePartExtensionMapping,
            Dictionary<MethodInfo, string> methodInfoDateDiffMapping,
            Dictionary<MethodInfo, string> methodInfoDateDiffBigMapping,
            Dictionary<MethodInfo, string> methodInfoContainsMapping)
        {
            _sqlExpressionFactory = sqlExpressionFactory;
            _methodInfoDateAddMapping = methodInfoDateAddMapping;
            _methodInfoDateAddExtensionMapping = methodInfoDateAddExtensionMapping;
            _methodInfoDatePartExtensionMapping = methodInfoDatePartExtensionMapping;
            _methodInfoDateDiffMapping = methodInfoDateDiffMapping;
            _methodInfoDateDiffBigMapping = methodInfoDateDiffBigMapping;
            _methodInfoContainsMapping = methodInfoContainsMapping;
        }

        public SqlExpression Translate(SqlExpression instance, MethodInfo method, IReadOnlyList<SqlExpression> arguments, IDiagnosticsLogger<DbLoggerCategory.Query> logger)
        {
            if (_methodInfoDateAddMapping != null && _methodInfoDateAddMapping.TryGetValue(method, out var dateAddPart))
            {
                return arguments[0] is SqlConstantExpression sqlConstant
                    && ((sqlConstant.Value is double && ((double)sqlConstant.Value >= int.MaxValue || (double)sqlConstant.Value <= int.MinValue))
                    || (sqlConstant.Value is long && ((long)sqlConstant.Value >= int.MaxValue || (long)sqlConstant.Value <= int.MinValue)))
                        ? null
                        : _sqlExpressionFactory.Function(
                            "DATEADD",
                            new[]
                            {
                                _sqlExpressionFactory.Fragment(dateAddPart),
                                _sqlExpressionFactory.Convert(arguments[0], typeof(int)),
                                instance
                            },
                            false,
                            null,
                            instance.Type,
                            instance.TypeMapping);
            }
            else if (_methodInfoDateAddExtensionMapping != null && _methodInfoDateAddExtensionMapping.TryGetValue(method, out var dateAddExtensionPart))
            {
                return arguments[1] is SqlConstantExpression sqlConstant
                    && ((sqlConstant.Value is double && ((double)sqlConstant.Value >= int.MaxValue || (double)sqlConstant.Value <= int.MinValue))
                    || (sqlConstant.Value is long && ((long)sqlConstant.Value >= int.MaxValue || (long)sqlConstant.Value <= int.MinValue)))
                        ? null
                        : _sqlExpressionFactory.Function(
                            "DATEADD",
                            new[]
                            {
                                _sqlExpressionFactory.Fragment(dateAddExtensionPart),
                                _sqlExpressionFactory.Convert(arguments[1], typeof(int)),
                                arguments[0]
                            },
                            false,
                            null,
                            arguments[0].Type,
                            arguments[0].TypeMapping);
            }
            else if (_methodInfoDatePartExtensionMapping != null && _methodInfoDatePartExtensionMapping.TryGetValue(method, out var datePart))
            {
                return _sqlExpressionFactory.Function(
                            "DATEPART",
                            new[]
                            {
                                _sqlExpressionFactory.Fragment(datePart),
                                arguments[0]
                            },
                            false,
                            null,
                            method.ReturnType,
                            null);
            }
            else if (_methodInfoDateDiffMapping != null && _methodInfoDateDiffMapping.TryGetValue(method, out var dateDiffDatePart))
            {
                var startDate = arguments[1];
                var endDate = arguments[2];
                var typeMapping = ExpressionExtensions.InferTypeMapping(startDate, endDate);

                startDate = _sqlExpressionFactory.ApplyTypeMapping(startDate, typeMapping);
                endDate = _sqlExpressionFactory.ApplyTypeMapping(endDate, typeMapping);

                return _sqlExpressionFactory.Function(
                    "DATEDIFF",
                    new[] { _sqlExpressionFactory.Fragment(dateDiffDatePart), startDate, endDate },
                    false,
                    null,
                    method.ReturnType,
                    null);
            }
            else if (_methodInfoDateDiffBigMapping != null && _methodInfoDateDiffBigMapping.TryGetValue(method, out var dateDiffBigDatePart))
            {
                var startDate = arguments[1];
                var endDate = arguments[2];
                var typeMapping = ExpressionExtensions.InferTypeMapping(startDate, endDate);

                startDate = _sqlExpressionFactory.ApplyTypeMapping(startDate, typeMapping);
                endDate = _sqlExpressionFactory.ApplyTypeMapping(endDate, typeMapping);

                return _sqlExpressionFactory.Function(
                    "DATEDIFF_BIG",
                    new[] { _sqlExpressionFactory.Fragment(dateDiffBigDatePart), startDate, endDate },
                    false,
                    null,
                    method.ReturnType,
                    null);
            }
            else if ((_methodInfoContainsMapping?.TryGetValue(method, out var containsMapping) ?? false)
                && ValidateValues(arguments[0]))
            {
                // Note that almost all forms of Contains are queryable (e.g. over inline/parameter collections), and translated in
                // RelationalQueryableMethodTranslatingExpressionVisitor.TranslateContains.
                // This enumerable Contains translation is still needed for entity Contains (#30712)
                SqlExpression itemExpression = null, valuesExpression = null;

                // Identify static Enumerable.Contains and instance List.Contains
                if (method.IsGenericMethod
                    && ValidateValues(arguments[0]))
                {
                    (itemExpression, valuesExpression) = (RemoveObjectConvert(arguments[1]), arguments[0]);
                }

                if (arguments.Count == 1
                    && instance != null
                    && ValidateValues(instance))
                {
                    (itemExpression, valuesExpression) = (RemoveObjectConvert(arguments[0]), instance);
                }

                if (itemExpression is not null && valuesExpression is not null)
                {
                    switch (valuesExpression)
                    {
                        case SqlParameterExpression parameter:
                            return _sqlExpressionFactory.In(itemExpression, parameter);

                        case SqlConstantExpression { Value: IEnumerable values }:
                            var valuesExpressions = new List<SqlExpression>();

                            foreach (var value in values)
                            {
                                valuesExpressions.Add(_sqlExpressionFactory.Constant(value));
                            }

                            return _sqlExpressionFactory.In(itemExpression, valuesExpressions);
                    }
                }
            }

            return null;
        }

        private static bool ValidateValues(SqlExpression values)
            => values is SqlConstantExpression or SqlParameterExpression;

        private static SqlExpression RemoveObjectConvert(SqlExpression expression)
            => expression is SqlUnaryExpression { OperatorType: ExpressionType.Convert } sqlUnaryExpression
                && sqlUnaryExpression.Type == typeof(object)
                    ? sqlUnaryExpression.Operand
                    : expression;
    }
}
