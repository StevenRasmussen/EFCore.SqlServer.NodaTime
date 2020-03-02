using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.EntityFrameworkCore.SqlServer.NodaTime.Extensions;
using Microsoft.EntityFrameworkCore.Storage;
using NodaTime;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Microsoft.EntityFrameworkCore.SqlServer.Query.ExpressionTranslators
{
    internal class LocalDateMethodTranslator : IMethodCallTranslator
    {
        private readonly Dictionary<MethodInfo, string> _methodInfoDateAddMapping = new Dictionary<MethodInfo, string>
        {
            { typeof(LocalDate).GetRuntimeMethod(nameof(LocalDate.PlusYears), new[] { typeof(int) }), "year" },
            { typeof(LocalDate).GetRuntimeMethod(nameof(LocalDate.PlusMonths), new[] { typeof(int) }), "month" },
            { typeof(LocalDate).GetRuntimeMethod(nameof(LocalDate.PlusDays), new[] { typeof(int) }), "day" },
            { typeof(LocalDate).GetRuntimeMethod(nameof(LocalDate.PlusWeeks), new[] { typeof(int) }), "week" },
        };

        private readonly Dictionary<MethodInfo, string> _methodInfoDateAddExtensionMapping = new Dictionary<MethodInfo, string>
        {
            { typeof(LocalDateExtensions).GetRuntimeMethod(nameof(LocalDateExtensions.PlusQuarters), new[] { typeof(LocalDate), typeof(int) }), "quarter" },
        };

        private readonly Dictionary<MethodInfo, string> _methodInfoDatePartExtensionMapping = new Dictionary<MethodInfo, string>
        {
            { typeof(LocalDateExtensions).GetRuntimeMethod(nameof(LocalDateExtensions.Quarter), new[] { typeof(LocalDate) }), "quarter" },
            { typeof(LocalDateExtensions).GetRuntimeMethod(nameof(LocalDateExtensions.Week), new[] { typeof(LocalDate) }), "week" },
        };

        private readonly ISqlExpressionFactory _sqlExpressionFactory;

        public LocalDateMethodTranslator(
            ISqlExpressionFactory sqlExpressionFactory)
        {
            _sqlExpressionFactory = sqlExpressionFactory;
        }

        public SqlExpression Translate(SqlExpression instance, MethodInfo method, IReadOnlyList<SqlExpression> arguments)
        {
            if (_methodInfoDateAddMapping.TryGetValue(method, out var dateAddPart))
            {
                return _sqlExpressionFactory.Function(
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
                return _sqlExpressionFactory.Function(
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
