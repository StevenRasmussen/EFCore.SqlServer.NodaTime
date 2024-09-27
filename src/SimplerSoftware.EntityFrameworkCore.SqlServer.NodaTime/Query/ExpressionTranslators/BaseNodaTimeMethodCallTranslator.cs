﻿using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System.Collections.Generic;
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

        private static List<bool> _2argumentsPropagateNullability = new List<bool>
        {
            false,
            false,
        };

        private static List<bool> _3argumentsPropagateNullability = new List<bool>
        {
            false,
            false,
            false,
        };

        public BaseNodaTimeMethodCallTranslator(
            ISqlExpressionFactory sqlExpressionFactory,
            Dictionary<MethodInfo, string> methodInfoDateAddMapping,
            Dictionary<MethodInfo, string> methodInfoDateAddExtensionMapping,
            Dictionary<MethodInfo, string> methodInfoDatePartExtensionMapping,
            Dictionary<MethodInfo, string> methodInfoDateDiffMapping,
            Dictionary<MethodInfo, string> methodInfoDateDiffBigMapping)
        {
            _sqlExpressionFactory = sqlExpressionFactory;
            _methodInfoDateAddMapping = methodInfoDateAddMapping;
            _methodInfoDateAddExtensionMapping = methodInfoDateAddExtensionMapping;
            _methodInfoDatePartExtensionMapping = methodInfoDatePartExtensionMapping;
            _methodInfoDateDiffMapping = methodInfoDateDiffMapping;
            _methodInfoDateDiffBigMapping = methodInfoDateDiffBigMapping;
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
                            name: "DATEADD",
                            arguments: new[]
                            {
                                _sqlExpressionFactory.Fragment(dateAddPart),
                                _sqlExpressionFactory.Convert(arguments[0], typeof(int)),
                                instance
                            },
                            nullable: false,
                            argumentsPropagateNullability: _3argumentsPropagateNullability,
                            returnType: instance.Type,
                            typeMapping: instance.TypeMapping);
            }
            else if (_methodInfoDateAddExtensionMapping != null && _methodInfoDateAddExtensionMapping.TryGetValue(method, out var dateAddExtensionPart))
            {
                return arguments[1] is SqlConstantExpression sqlConstant
                    && ((sqlConstant.Value is double && ((double)sqlConstant.Value >= int.MaxValue || (double)sqlConstant.Value <= int.MinValue))
                    || (sqlConstant.Value is long && ((long)sqlConstant.Value >= int.MaxValue || (long)sqlConstant.Value <= int.MinValue)))
                        ? null
                        : _sqlExpressionFactory.Function(
                            name: "DATEADD",
                            arguments: new[]
                            {
                                _sqlExpressionFactory.Fragment(dateAddExtensionPart),
                                _sqlExpressionFactory.Convert(arguments[1], typeof(int)),
                                arguments[0]
                            },
                            nullable: false,
                            argumentsPropagateNullability: _3argumentsPropagateNullability,
                            returnType: arguments[0].Type,
                            typeMapping: arguments[0].TypeMapping);
            }
            else if (_methodInfoDatePartExtensionMapping != null && _methodInfoDatePartExtensionMapping.TryGetValue(method, out var datePart))
            {
                return _sqlExpressionFactory.Function(
                            name: "DATEPART",
                            arguments: new[]
                            {
                                _sqlExpressionFactory.Fragment(datePart),
                                arguments[0]
                            },
                            nullable: false,
                            argumentsPropagateNullability: _2argumentsPropagateNullability,
                            returnType: method.ReturnType,
                            typeMapping: null);
            }
            else if (_methodInfoDateDiffMapping != null && _methodInfoDateDiffMapping.TryGetValue(method, out var dateDiffDatePart))
            {
                var startDate = arguments[1];
                var endDate = arguments[2];
                var typeMapping = ExpressionExtensions.InferTypeMapping(startDate, endDate);

                startDate = _sqlExpressionFactory.ApplyTypeMapping(startDate, typeMapping);
                endDate = _sqlExpressionFactory.ApplyTypeMapping(endDate, typeMapping);

                return _sqlExpressionFactory.Function(
                    name: "DATEDIFF",
                    arguments: new[] { _sqlExpressionFactory.Fragment(dateDiffDatePart), startDate, endDate },
                    nullable: false,
                    argumentsPropagateNullability: _3argumentsPropagateNullability,
                    returnType: method.ReturnType,
                    typeMapping: null);
            }
            else if (_methodInfoDateDiffBigMapping != null && _methodInfoDateDiffBigMapping.TryGetValue(method, out var dateDiffBigDatePart))
            {
                var startDate = arguments[1];
                var endDate = arguments[2];
                var typeMapping = ExpressionExtensions.InferTypeMapping(startDate, endDate);

                startDate = _sqlExpressionFactory.ApplyTypeMapping(startDate, typeMapping);
                endDate = _sqlExpressionFactory.ApplyTypeMapping(endDate, typeMapping);

                return _sqlExpressionFactory.Function(
                    name: "DATEDIFF_BIG",
                    arguments: new[] { _sqlExpressionFactory.Fragment(dateDiffBigDatePart), startDate, endDate },
                    nullable: false,
                    argumentsPropagateNullability: _3argumentsPropagateNullability,
                    returnType: method.ReturnType,
                    typeMapping: null);
            }
            return null;
        }
    }
}
