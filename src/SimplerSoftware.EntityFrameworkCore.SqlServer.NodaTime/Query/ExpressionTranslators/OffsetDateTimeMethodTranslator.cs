using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.SqlServer.NodaTime.Extensions;
using NodaTime;
using System.Collections.Generic;
using System.Reflection;

namespace Microsoft.EntityFrameworkCore.SqlServer.Query.ExpressionTranslators
{
    internal class OffsetDateTimeMethodTranslator : BaseNodaTimeMethodCallTranslator
    {
        private static readonly Dictionary<MethodInfo, string> _methodInfoDateAddMapping = new Dictionary<MethodInfo, string>
        {
            { typeof(OffsetDateTime).GetRuntimeMethod(nameof(OffsetDateTime.PlusHours), new[] { typeof(int) }), "hour" },
            { typeof(OffsetDateTime).GetRuntimeMethod(nameof(OffsetDateTime.PlusMinutes), new[] { typeof(int) }), "minute" },
            { typeof(OffsetDateTime).GetRuntimeMethod(nameof(OffsetDateTime.PlusSeconds), new[] { typeof(long) }), "second" },
            { typeof(OffsetDateTime).GetRuntimeMethod(nameof(OffsetDateTime.PlusMilliseconds), new[] { typeof(long) }), "millisecond" },
            { typeof(OffsetDateTime).GetRuntimeMethod(nameof(OffsetDateTime.PlusNanoseconds), new[] { typeof(long) }), "nanosecond" },
        };

        private static readonly Dictionary<MethodInfo, string> _methodInfoDateAddExtensionMapping = new Dictionary<MethodInfo, string>
        {
            { typeof(OffsetDateTimeExtensions).GetRuntimeMethod(nameof(OffsetDateTimeExtensions.PlusYears), new[] { typeof(OffsetDateTime), typeof(int) }), "year" },
            { typeof(OffsetDateTimeExtensions).GetRuntimeMethod(nameof(OffsetDateTimeExtensions.PlusMonths), new[] { typeof(OffsetDateTime), typeof(int) }), "month" },
            { typeof(OffsetDateTimeExtensions).GetRuntimeMethod(nameof(OffsetDateTimeExtensions.PlusDays), new[] { typeof(OffsetDateTime), typeof(int) }), "day" },
            { typeof(OffsetDateTimeExtensions).GetRuntimeMethod(nameof(OffsetDateTimeExtensions.PlusWeeks), new[] { typeof(OffsetDateTime), typeof(int) }), "week" },
            { typeof(OffsetDateTimeExtensions).GetRuntimeMethod(nameof(OffsetDateTimeExtensions.PlusQuarters), new[] { typeof(OffsetDateTime), typeof(int) }), "quarter" },
            { typeof(OffsetDateTimeExtensions).GetRuntimeMethod(nameof(OffsetDateTimeExtensions.PlusMicroseconds), new[] { typeof(OffsetDateTime), typeof(long) }), "microsecond" },
        };

        private static readonly Dictionary<MethodInfo, string> _methodInfoDatePartExtensionMapping = new Dictionary<MethodInfo, string>
        {
            { typeof(OffsetDateTimeExtensions).GetRuntimeMethod(nameof(OffsetDateTimeExtensions.Quarter), new[] { typeof(OffsetDateTime) }), "quarter" },
            { typeof(OffsetDateTimeExtensions).GetRuntimeMethod(nameof(OffsetDateTimeExtensions.Week), new[] { typeof(OffsetDateTime) }), "week" },
            { typeof(OffsetDateTimeExtensions).GetRuntimeMethod(nameof(OffsetDateTimeExtensions.IsoWeek), new[] { typeof(OffsetDateTime) }), "iso_week" },
            { typeof(OffsetDateTimeExtensions).GetRuntimeMethod(nameof(OffsetDateTimeExtensions.TzOffset), new[] { typeof(OffsetDateTime) }), "tzoffset" },
            { typeof(OffsetDateTimeExtensions).GetRuntimeMethod(nameof(OffsetDateTimeExtensions.WeekDay), new[] { typeof(OffsetDateTime) }), "weekday" },
            { typeof(OffsetDateTimeExtensions).GetRuntimeMethod(nameof(OffsetDateTimeExtensions.Microsecond), new[] { typeof(OffsetDateTime) }), "microsecond" },
        };

        private static readonly Dictionary<MethodInfo, string> _methodInfoDateDiffMapping = new Dictionary<MethodInfo, string>
        {
            // Offset Date Time
            {
                typeof(SqlServerNodaTimeDbFunctionsExtensions).GetRuntimeMethod(
                    nameof(SqlServerNodaTimeDbFunctionsExtensions.DateDiffYear),
                    new[] { typeof(DbFunctions), typeof(OffsetDateTime), typeof(OffsetDateTime) }),
                "YEAR"
            },
            {
                typeof(SqlServerNodaTimeDbFunctionsExtensions).GetRuntimeMethod(
                    nameof(SqlServerNodaTimeDbFunctionsExtensions.DateDiffYear),
                    new[] { typeof(DbFunctions), typeof(OffsetDateTime?), typeof(OffsetDateTime?) }),
                "YEAR"
            },
            {
                typeof(SqlServerNodaTimeDbFunctionsExtensions).GetRuntimeMethod(
                    nameof(SqlServerNodaTimeDbFunctionsExtensions.DateDiffMonth),
                    new[] { typeof(DbFunctions), typeof(OffsetDateTime), typeof(OffsetDateTime) }),
                "MONTH"
            },
            {
                typeof(SqlServerNodaTimeDbFunctionsExtensions).GetRuntimeMethod(
                    nameof(SqlServerNodaTimeDbFunctionsExtensions.DateDiffMonth),
                    new[] { typeof(DbFunctions), typeof(OffsetDateTime?), typeof(OffsetDateTime?) }),
                "MONTH"
            },
            {
                typeof(SqlServerNodaTimeDbFunctionsExtensions).GetRuntimeMethod(
                    nameof(SqlServerNodaTimeDbFunctionsExtensions.DateDiffDay),
                    new[] { typeof(DbFunctions), typeof(OffsetDateTime), typeof(OffsetDateTime) }),
                "DAY"
            },
            {
                typeof(SqlServerNodaTimeDbFunctionsExtensions).GetRuntimeMethod(
                    nameof(SqlServerNodaTimeDbFunctionsExtensions.DateDiffDay),
                    new[] { typeof(DbFunctions), typeof(OffsetDateTime?), typeof(OffsetDateTime?) }),
                "DAY"
            },
            {
                typeof(SqlServerNodaTimeDbFunctionsExtensions).GetRuntimeMethod(
                    nameof(SqlServerNodaTimeDbFunctionsExtensions.DateDiffWeek),
                    new[] { typeof(DbFunctions), typeof(OffsetDateTime), typeof(OffsetDateTime) }),
                "WEEK"
            },
            {
                typeof(SqlServerNodaTimeDbFunctionsExtensions).GetRuntimeMethod(
                    nameof(SqlServerNodaTimeDbFunctionsExtensions.DateDiffWeek),
                    new[] { typeof(DbFunctions), typeof(OffsetDateTime?), typeof(OffsetDateTime?) }),
                "WEEK"
            },
            {
                typeof(SqlServerNodaTimeDbFunctionsExtensions).GetRuntimeMethod(
                    nameof(SqlServerNodaTimeDbFunctionsExtensions.DateDiffHour),
                    new[] { typeof(DbFunctions), typeof(OffsetDateTime), typeof(OffsetDateTime) }),
                "HOUR"
            },
            {
                typeof(SqlServerNodaTimeDbFunctionsExtensions).GetRuntimeMethod(
                    nameof(SqlServerNodaTimeDbFunctionsExtensions.DateDiffHour),
                    new[] { typeof(DbFunctions), typeof(OffsetDateTime?), typeof(OffsetDateTime?) }),
                "HOUR"
            },
            {
                typeof(SqlServerNodaTimeDbFunctionsExtensions).GetRuntimeMethod(
                    nameof(SqlServerNodaTimeDbFunctionsExtensions.DateDiffMinute),
                    new[] { typeof(DbFunctions), typeof(OffsetDateTime), typeof(OffsetDateTime) }),
                "MINUTE"
            },
            {
                typeof(SqlServerNodaTimeDbFunctionsExtensions).GetRuntimeMethod(
                    nameof(SqlServerNodaTimeDbFunctionsExtensions.DateDiffMinute),
                    new[] { typeof(DbFunctions), typeof(OffsetDateTime?), typeof(OffsetDateTime?) }),
                "MINUTE"
            },
            {
                typeof(SqlServerNodaTimeDbFunctionsExtensions).GetRuntimeMethod(
                    nameof(SqlServerNodaTimeDbFunctionsExtensions.DateDiffSecond),
                    new[] { typeof(DbFunctions), typeof(OffsetDateTime), typeof(OffsetDateTime) }),
                "SECOND"
            },
            {
                typeof(SqlServerNodaTimeDbFunctionsExtensions).GetRuntimeMethod(
                    nameof(SqlServerNodaTimeDbFunctionsExtensions.DateDiffSecond),
                    new[] { typeof(DbFunctions), typeof(OffsetDateTime?), typeof(OffsetDateTime?) }),
                "SECOND"
            },
            {
                typeof(SqlServerNodaTimeDbFunctionsExtensions).GetRuntimeMethod(
                    nameof(SqlServerNodaTimeDbFunctionsExtensions.DateDiffMillisecond),
                    new[] { typeof(DbFunctions), typeof(OffsetDateTime), typeof(OffsetDateTime) }),
                "MILLISECOND"
            },
            {
                typeof(SqlServerNodaTimeDbFunctionsExtensions).GetRuntimeMethod(
                    nameof(SqlServerNodaTimeDbFunctionsExtensions.DateDiffMillisecond),
                    new[] { typeof(DbFunctions), typeof(OffsetDateTime?), typeof(OffsetDateTime?) }),
                "MILLISECOND"
            },
            {
                typeof(SqlServerNodaTimeDbFunctionsExtensions).GetRuntimeMethod(
                    nameof(SqlServerNodaTimeDbFunctionsExtensions.DateDiffMicrosecond),
                    new[] { typeof(DbFunctions), typeof(OffsetDateTime), typeof(OffsetDateTime) }),
                "MICROSECOND"
            },
            {
                typeof(SqlServerNodaTimeDbFunctionsExtensions).GetRuntimeMethod(
                    nameof(SqlServerNodaTimeDbFunctionsExtensions.DateDiffMicrosecond),
                    new[] { typeof(DbFunctions), typeof(OffsetDateTime?), typeof(OffsetDateTime?) }),
                "MICROSECOND"
            },
            {
                typeof(SqlServerNodaTimeDbFunctionsExtensions).GetRuntimeMethod(
                    nameof(SqlServerNodaTimeDbFunctionsExtensions.DateDiffNanosecond),
                    new[] { typeof(DbFunctions), typeof(OffsetDateTime), typeof(OffsetDateTime) }),
                "NANOSECOND"
            },
            {
                typeof(SqlServerNodaTimeDbFunctionsExtensions).GetRuntimeMethod(
                    nameof(SqlServerNodaTimeDbFunctionsExtensions.DateDiffNanosecond),
                    new[] { typeof(DbFunctions), typeof(OffsetDateTime?), typeof(OffsetDateTime?) }),
                "NANOSECOND"
            },
        };

        public OffsetDateTimeMethodTranslator(ISqlExpressionFactory sqlExpressionFactory)
            :base(sqlExpressionFactory, _methodInfoDateAddMapping, _methodInfoDateAddExtensionMapping, _methodInfoDatePartExtensionMapping, _methodInfoDateDiffMapping)
        {
        }
    }
}
