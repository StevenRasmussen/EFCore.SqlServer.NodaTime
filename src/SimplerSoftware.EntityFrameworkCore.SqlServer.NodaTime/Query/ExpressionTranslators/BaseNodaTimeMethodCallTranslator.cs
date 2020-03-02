using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Microsoft.EntityFrameworkCore.SqlServer.Query.ExpressionTranslators
{
    public abstract class BaseNodaTimeMethodCallTranslator : IMethodCallTranslator
    {
        private readonly ISqlExpressionFactory _sqlExpressionFactory;

        private readonly Dictionary<MethodInfo, string> _methodInfoDateAddMapping;
        private readonly Dictionary<MethodInfo, string> _methodInfoDateAddExtensionMapping;
        private readonly Dictionary<MethodInfo, string> _methodInfoDatePartExtensionMapping;

        public BaseNodaTimeMethodCallTranslator(
            ISqlExpressionFactory sqlExpressionFactory,
            Dictionary<MethodInfo, string> methodInfoDateAddMapping,
            Dictionary<MethodInfo, string> methodInfoDateAddExtensionMapping,
            Dictionary<MethodInfo, string> methodInfoDatePartExtensionMapping)
        {
            _sqlExpressionFactory = sqlExpressionFactory;
            _methodInfoDateAddMapping = methodInfoDateAddMapping ?? new Dictionary<MethodInfo, string>();
            _methodInfoDateAddExtensionMapping = methodInfoDateAddExtensionMapping ?? new Dictionary<MethodInfo, string>();
            _methodInfoDatePartExtensionMapping = methodInfoDatePartExtensionMapping ?? new Dictionary<MethodInfo, string>();
        }

        public SqlExpression Translate(SqlExpression instance, MethodInfo method, IReadOnlyList<SqlExpression> arguments)
        {
            if (_methodInfoDateAddMapping.TryGetValue(method, out var dateAddPart))
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
                            instance.Type,
                            instance.TypeMapping);
            }
            else if (_methodInfoDateAddExtensionMapping.TryGetValue(method, out var dateAddExtensionPart))
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
                            arguments[0].Type,
                            arguments[0].TypeMapping);
            }
            else if (_methodInfoDatePartExtensionMapping.TryGetValue(method, out var datePart))
            {
                return _sqlExpressionFactory.Function(
                            "DATEPART",
                            new[]
                            {
                                _sqlExpressionFactory.Fragment(datePart),
                                arguments[0]
                            },
                            method.ReturnType,
                            null);
            }
            return null;
        }
    }
}
