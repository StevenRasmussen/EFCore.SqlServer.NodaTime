using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.EntityFrameworkCore.SqlServer.NodaTime.Extensions;
using Microsoft.EntityFrameworkCore.SqlServer.Storage;
using Microsoft.EntityFrameworkCore.Storage;
using NodaTime;
using System.Collections.Generic;
using System.Reflection;

namespace Microsoft.EntityFrameworkCore.SqlServer.Query.ExpressionTranslators
{
    internal class InstantMethodTranslator : IMethodCallTranslator
    {
        private readonly Dictionary<MethodInfo, string> _methodInfoDateAddMapping = new Dictionary<MethodInfo, string>
        {
            { typeof(InstantExtensions).GetRuntimeMethod(nameof(InstantExtensions.PlusYears), new[] { typeof(Instant), typeof(int) }), "year" },
            { typeof(InstantExtensions).GetRuntimeMethod(nameof(InstantExtensions.PlusMonths), new[] { typeof(Instant), typeof(int) }), "month" },
            { typeof(InstantExtensions).GetRuntimeMethod(nameof(InstantExtensions.PlusDays), new[] { typeof(Instant), typeof(double) }), "day" },
            { typeof(InstantExtensions).GetRuntimeMethod(nameof(InstantExtensions.PlusHours), new[] { typeof(Instant), typeof(double) }), "hour" },
            { typeof(InstantExtensions).GetRuntimeMethod(nameof(InstantExtensions.PlusMinutes), new[] { typeof(Instant), typeof(double) }), "minute" },
            { typeof(InstantExtensions).GetRuntimeMethod(nameof(InstantExtensions.PlusSeconds), new[] { typeof(Instant), typeof(double) }), "second" },
            { typeof(InstantExtensions).GetRuntimeMethod(nameof(InstantExtensions.PlusMilliseconds), new[] { typeof(Instant), typeof(double) }), "millisecond" },
        };

        private readonly Dictionary<MethodInfo, string> _methodInfoDatePartMapping = new Dictionary<MethodInfo, string>
        {
            { typeof(InstantExtensions).GetRuntimeMethod(nameof(InstantExtensions.Year), new[] { typeof(Instant), }), "year" },
            { typeof(InstantExtensions).GetRuntimeMethod(nameof(InstantExtensions.Quarter), new[] { typeof(Instant), }), "quarter" },
            { typeof(InstantExtensions).GetRuntimeMethod(nameof(InstantExtensions.Month), new[] { typeof(Instant), }), "month" },
            { typeof(InstantExtensions).GetRuntimeMethod(nameof(InstantExtensions.DayOfYear), new[] { typeof(Instant), }), "dayofyear" },
            { typeof(InstantExtensions).GetRuntimeMethod(nameof(InstantExtensions.Day), new[] { typeof(Instant), }), "day" },
            { typeof(InstantExtensions).GetRuntimeMethod(nameof(InstantExtensions.Week), new[] { typeof(Instant), }), "week" },
            { typeof(InstantExtensions).GetRuntimeMethod(nameof(InstantExtensions.WeekDay), new[] { typeof(Instant), }), "weekday" },
            { typeof(InstantExtensions).GetRuntimeMethod(nameof(InstantExtensions.Hour), new[] { typeof(Instant), }), "hour" },
            { typeof(InstantExtensions).GetRuntimeMethod(nameof(InstantExtensions.Minute), new[] { typeof(Instant), }), "minute" },
            { typeof(InstantExtensions).GetRuntimeMethod(nameof(InstantExtensions.Second), new[] { typeof(Instant), }), "second" },
            { typeof(InstantExtensions).GetRuntimeMethod(nameof(InstantExtensions.Millisecond), new[] { typeof(Instant), }), "millisecond" },
            { typeof(InstantExtensions).GetRuntimeMethod(nameof(InstantExtensions.Microsecond), new[] { typeof(Instant), }), "microsecond" },
            { typeof(InstantExtensions).GetRuntimeMethod(nameof(InstantExtensions.Nanosecond), new[] { typeof(Instant), }), "nanosecond" },
            { typeof(InstantExtensions).GetRuntimeMethod(nameof(InstantExtensions.TzOffset), new[] { typeof(Instant), }), "tzoffset" },
            { typeof(InstantExtensions).GetRuntimeMethod(nameof(InstantExtensions.IsoWeek), new[] { typeof(Instant), }), "iso_week" },
        };

        private readonly ISqlExpressionFactory _sqlExpressionFactory;

        public InstantMethodTranslator(
            ISqlExpressionFactory sqlExpressionFactory)
        {
            _sqlExpressionFactory = sqlExpressionFactory;
        }

        public virtual SqlExpression Translate(SqlExpression instance, MethodInfo method, IReadOnlyList<SqlExpression> arguments)
        {
            if (_methodInfoDateAddMapping.TryGetValue(method, out var dateAddPart))
            {
                return !dateAddPart.Equals("year")
                    && !dateAddPart.Equals("month")
                    && arguments[0] is SqlConstantExpression sqlConstant
                    && ((double)sqlConstant.Value >= int.MaxValue
                        || (double)sqlConstant.Value <= int.MinValue)
                        ? null
                        : _sqlExpressionFactory.Function(
                            "DATEADD",
                            new[]
                            {
                                _sqlExpressionFactory.Fragment(dateAddPart),
                                _sqlExpressionFactory.Convert(arguments[1], typeof(int)),
                                arguments[0]
                            },
                            arguments[0].Type,
                            arguments[0].TypeMapping);
            }
            else if (_methodInfoDatePartMapping.TryGetValue(method, out var datePart))
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
