using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.SqlServer.NodaTime.Extensions;
using NodaTime;
using System.Collections.Generic;
using System.Reflection;

namespace Microsoft.EntityFrameworkCore.SqlServer.Query.ExpressionTranslators
{
    internal class InstantMethodTranslator : BaseNodaTimeMethodCallTranslator
    {
        private static readonly Dictionary<MethodInfo, string> _methodInfoDateAddExtensionMapping = new Dictionary<MethodInfo, string>
        {
            { typeof(InstantExtensions).GetRuntimeMethod(nameof(InstantExtensions.PlusYears), new[] { typeof(Instant), typeof(int) }), "year" },
            { typeof(InstantExtensions).GetRuntimeMethod(nameof(InstantExtensions.PlusMonths), new[] { typeof(Instant), typeof(int) }), "month" },
            { typeof(InstantExtensions).GetRuntimeMethod(nameof(InstantExtensions.PlusDays), new[] { typeof(Instant), typeof(double) }), "day" },
            { typeof(InstantExtensions).GetRuntimeMethod(nameof(InstantExtensions.PlusHours), new[] { typeof(Instant), typeof(double) }), "hour" },
            { typeof(InstantExtensions).GetRuntimeMethod(nameof(InstantExtensions.PlusMinutes), new[] { typeof(Instant), typeof(double) }), "minute" },
            { typeof(InstantExtensions).GetRuntimeMethod(nameof(InstantExtensions.PlusSeconds), new[] { typeof(Instant), typeof(double) }), "second" },
            { typeof(InstantExtensions).GetRuntimeMethod(nameof(InstantExtensions.PlusMilliseconds), new[] { typeof(Instant), typeof(double) }), "millisecond" },
        };

        private static readonly Dictionary<MethodInfo, string> _methodInfoDatePartExtensionMapping = new Dictionary<MethodInfo, string>
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

        private static readonly Dictionary<MethodInfo, string> _methodInfoDateDiffMapping = new Dictionary<MethodInfo, string>
        {
            {
                typeof(SqlServerNodaTimeDbFunctionsExtensions).GetRuntimeMethod(
                    nameof(SqlServerNodaTimeDbFunctionsExtensions.DateDiffYear),
                    new[] { typeof(DbFunctions), typeof(Instant), typeof(Instant) }),
                "YEAR"
            },
            {
                typeof(SqlServerNodaTimeDbFunctionsExtensions).GetRuntimeMethod(
                    nameof(SqlServerNodaTimeDbFunctionsExtensions.DateDiffYear),
                    new[] { typeof(DbFunctions), typeof(Instant?), typeof(Instant?) }),
                "YEAR"
            },
            {
                typeof(SqlServerNodaTimeDbFunctionsExtensions).GetRuntimeMethod(
                    nameof(SqlServerNodaTimeDbFunctionsExtensions.DateDiffMonth),
                    new[] { typeof(DbFunctions), typeof(Instant), typeof(Instant) }),
                "MONTH"
            },
            {
                typeof(SqlServerNodaTimeDbFunctionsExtensions).GetRuntimeMethod(
                    nameof(SqlServerNodaTimeDbFunctionsExtensions.DateDiffMonth),
                    new[] { typeof(DbFunctions), typeof(Instant?), typeof(Instant?) }),
                "MONTH"
            },
            {
                typeof(SqlServerNodaTimeDbFunctionsExtensions).GetRuntimeMethod(
                    nameof(SqlServerNodaTimeDbFunctionsExtensions.DateDiffDay),
                    new[] { typeof(DbFunctions), typeof(Instant), typeof(Instant) }),
                "DAY"
            },
            {
                typeof(SqlServerNodaTimeDbFunctionsExtensions).GetRuntimeMethod(
                    nameof(SqlServerNodaTimeDbFunctionsExtensions.DateDiffDay),
                    new[] { typeof(DbFunctions), typeof(Instant?), typeof(Instant?) }),
                "DAY"
            },
            {
                typeof(SqlServerNodaTimeDbFunctionsExtensions).GetRuntimeMethod(
                    nameof(SqlServerNodaTimeDbFunctionsExtensions.DateDiffWeek),
                    new[] { typeof(DbFunctions), typeof(Instant), typeof(Instant) }),
                "WEEK"
            },
            {
                typeof(SqlServerNodaTimeDbFunctionsExtensions).GetRuntimeMethod(
                    nameof(SqlServerNodaTimeDbFunctionsExtensions.DateDiffWeek),
                    new[] { typeof(DbFunctions), typeof(Instant?), typeof(Instant?) }),
                "WEEK"
            },
            {
                typeof(SqlServerNodaTimeDbFunctionsExtensions).GetRuntimeMethod(
                    nameof(SqlServerNodaTimeDbFunctionsExtensions.DateDiffHour),
                    new[] { typeof(DbFunctions), typeof(Instant), typeof(Instant) }),
                "HOUR"
            },
            {
                typeof(SqlServerNodaTimeDbFunctionsExtensions).GetRuntimeMethod(
                    nameof(SqlServerNodaTimeDbFunctionsExtensions.DateDiffHour),
                    new[] { typeof(DbFunctions), typeof(Instant?), typeof(Instant?) }),
                "HOUR"
            },
            {
                typeof(SqlServerNodaTimeDbFunctionsExtensions).GetRuntimeMethod(
                    nameof(SqlServerNodaTimeDbFunctionsExtensions.DateDiffMinute),
                    new[] { typeof(DbFunctions), typeof(Instant), typeof(Instant) }),
                "MINUTE"
            },
            {
                typeof(SqlServerNodaTimeDbFunctionsExtensions).GetRuntimeMethod(
                    nameof(SqlServerNodaTimeDbFunctionsExtensions.DateDiffMinute),
                    new[] { typeof(DbFunctions), typeof(Instant?), typeof(Instant?) }),
                "MINUTE"
            },
            {
                typeof(SqlServerNodaTimeDbFunctionsExtensions).GetRuntimeMethod(
                    nameof(SqlServerNodaTimeDbFunctionsExtensions.DateDiffSecond),
                    new[] { typeof(DbFunctions), typeof(Instant), typeof(Instant) }),
                "SECOND"
            },
            {
                typeof(SqlServerNodaTimeDbFunctionsExtensions).GetRuntimeMethod(
                    nameof(SqlServerNodaTimeDbFunctionsExtensions.DateDiffSecond),
                    new[] { typeof(DbFunctions), typeof(Instant?), typeof(Instant?) }),
                "SECOND"
            },
            {
                typeof(SqlServerNodaTimeDbFunctionsExtensions).GetRuntimeMethod(
                    nameof(SqlServerNodaTimeDbFunctionsExtensions.DateDiffMillisecond),
                    new[] { typeof(DbFunctions), typeof(Instant), typeof(Instant) }),
                "MILLISECOND"
            },
            {
                typeof(SqlServerNodaTimeDbFunctionsExtensions).GetRuntimeMethod(
                    nameof(SqlServerNodaTimeDbFunctionsExtensions.DateDiffMillisecond),
                    new[] { typeof(DbFunctions), typeof(Instant?), typeof(Instant?) }),
                "MILLISECOND"
            },
            {
                typeof(SqlServerNodaTimeDbFunctionsExtensions).GetRuntimeMethod(
                    nameof(SqlServerNodaTimeDbFunctionsExtensions.DateDiffMicrosecond),
                    new[] { typeof(DbFunctions), typeof(Instant), typeof(Instant) }),
                "MICROSECOND"
            },
            {
                typeof(SqlServerNodaTimeDbFunctionsExtensions).GetRuntimeMethod(
                    nameof(SqlServerNodaTimeDbFunctionsExtensions.DateDiffMicrosecond),
                    new[] { typeof(DbFunctions), typeof(Instant?), typeof(Instant?) }),
                "MICROSECOND"
            },
            {
                typeof(SqlServerNodaTimeDbFunctionsExtensions).GetRuntimeMethod(
                    nameof(SqlServerNodaTimeDbFunctionsExtensions.DateDiffNanosecond),
                    new[] { typeof(DbFunctions), typeof(Instant), typeof(Instant) }),
                "NANOSECOND"
            },
            {
                typeof(SqlServerNodaTimeDbFunctionsExtensions).GetRuntimeMethod(
                    nameof(SqlServerNodaTimeDbFunctionsExtensions.DateDiffNanosecond),
                    new[] { typeof(DbFunctions), typeof(Instant?), typeof(Instant?) }),
                "NANOSECOND"
            },
        };

        private static readonly Dictionary<MethodInfo, string> _methodInfoDateDiffBigMapping = new Dictionary<MethodInfo, string>
        {
            {
                typeof(SqlServerNodaTimeDbFunctionsExtensions).GetRuntimeMethod(
                    nameof(SqlServerNodaTimeDbFunctionsExtensions.DateDiffSecondBig),
                    new[] { typeof(DbFunctions), typeof(Instant), typeof(Instant) }),
                "SECOND"
            },
            {
                typeof(SqlServerNodaTimeDbFunctionsExtensions).GetRuntimeMethod(
                    nameof(SqlServerNodaTimeDbFunctionsExtensions.DateDiffSecondBig),
                    new[] { typeof(DbFunctions), typeof(Instant?), typeof(Instant?) }),
                "SECOND"
            },
            {
                typeof(SqlServerNodaTimeDbFunctionsExtensions).GetRuntimeMethod(
                    nameof(SqlServerNodaTimeDbFunctionsExtensions.DateDiffMillisecondBig),
                    new[] { typeof(DbFunctions), typeof(Instant), typeof(Instant) }),
                "MILLISECOND"
            },
            {
                typeof(SqlServerNodaTimeDbFunctionsExtensions).GetRuntimeMethod(
                    nameof(SqlServerNodaTimeDbFunctionsExtensions.DateDiffMillisecondBig),
                    new[] { typeof(DbFunctions), typeof(Instant?), typeof(Instant?) }),
                "MILLISECOND"
            },
            {
                typeof(SqlServerNodaTimeDbFunctionsExtensions).GetRuntimeMethod(
                    nameof(SqlServerNodaTimeDbFunctionsExtensions.DateDiffMicrosecondBig),
                    new[] { typeof(DbFunctions), typeof(Instant), typeof(Instant) }),
                "MICROSECOND"
            },
            {
                typeof(SqlServerNodaTimeDbFunctionsExtensions).GetRuntimeMethod(
                    nameof(SqlServerNodaTimeDbFunctionsExtensions.DateDiffMicrosecondBig),
                    new[] { typeof(DbFunctions), typeof(Instant?), typeof(Instant?) }),
                "MICROSECOND"
            },
            {
                typeof(SqlServerNodaTimeDbFunctionsExtensions).GetRuntimeMethod(
                    nameof(SqlServerNodaTimeDbFunctionsExtensions.DateDiffNanosecondBig),
                    new[] { typeof(DbFunctions), typeof(Instant), typeof(Instant) }),
                "NANOSECOND"
            },
            {
                typeof(SqlServerNodaTimeDbFunctionsExtensions).GetRuntimeMethod(
                    nameof(SqlServerNodaTimeDbFunctionsExtensions.DateDiffNanosecondBig),
                    new[] { typeof(DbFunctions), typeof(Instant?), typeof(Instant?) }),
                "NANOSECOND"
            },
        };

        public InstantMethodTranslator(ISqlExpressionFactory sqlExpressionFactory)
            : base(sqlExpressionFactory, null, _methodInfoDateAddExtensionMapping, _methodInfoDatePartExtensionMapping, _methodInfoDateDiffMapping, _methodInfoDateDiffBigMapping)
        {
        }
    }
}
