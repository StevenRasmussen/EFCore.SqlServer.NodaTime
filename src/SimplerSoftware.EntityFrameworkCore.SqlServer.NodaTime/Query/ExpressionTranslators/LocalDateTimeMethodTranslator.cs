using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.SqlServer.NodaTime.Extensions;
using NodaTime;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Microsoft.EntityFrameworkCore.SqlServer.Query.ExpressionTranslators
{
    internal class LocalDateTimeMethodTranslator : BaseNodaTimeMethodCallTranslator
    {
        private static readonly Dictionary<MethodInfo, string> _methodInfoDateAddMapping = new Dictionary<MethodInfo, string>
        {
            { typeof(LocalDateTime).GetRuntimeMethod(nameof(LocalDateTime.PlusYears), new[] { typeof(int) }), "year" },
            { typeof(LocalDateTime).GetRuntimeMethod(nameof(LocalDateTime.PlusMonths), new[] { typeof(int) }), "month" },
            { typeof(LocalDateTime).GetRuntimeMethod(nameof(LocalDateTime.PlusDays), new[] { typeof(int) }), "day" },
            { typeof(LocalDateTime).GetRuntimeMethod(nameof(LocalDateTime.PlusWeeks), new[] { typeof(int) }), "week" },
            { typeof(LocalDateTime).GetRuntimeMethod(nameof(LocalDateTime.PlusHours), new[] { typeof(long) }), "hour" },
            { typeof(LocalDateTime).GetRuntimeMethod(nameof(LocalDateTime.PlusMinutes), new[] { typeof(long) }), "minute" },
            { typeof(LocalDateTime).GetRuntimeMethod(nameof(LocalDateTime.PlusSeconds), new[] { typeof(long) }), "second" },
            { typeof(LocalDateTime).GetRuntimeMethod(nameof(LocalDateTime.PlusMilliseconds), new[] { typeof(long) }), "millisecond" },
            { typeof(LocalDateTime).GetRuntimeMethod(nameof(LocalDateTime.PlusNanoseconds), new[] { typeof(long) }), "nanosecond" },
        };

        private static readonly Dictionary<MethodInfo, string> _methodInfoDateAddExtensionMapping = new Dictionary<MethodInfo, string>
        {
            { typeof(LocalDateTimeExtensions).GetRuntimeMethod(nameof(LocalDateTimeExtensions.PlusQuarters), new[] { typeof(LocalDateTime), typeof(int) }), "quarter" },
            { typeof(LocalDateTimeExtensions).GetRuntimeMethod(nameof(LocalDateTimeExtensions.PlusMicroseconds), new[] { typeof(LocalDateTime), typeof(long) }), "microsecond" },
        };

        private static readonly Dictionary<MethodInfo, string> _methodInfoDatePartExtensionMapping = new Dictionary<MethodInfo, string>
        {
            { typeof(LocalDateTimeExtensions).GetRuntimeMethod(nameof(LocalDateTimeExtensions.Quarter), new[] { typeof(LocalDateTime) }), "quarter" },
            { typeof(LocalDateTimeExtensions).GetRuntimeMethod(nameof(LocalDateTimeExtensions.Week), new[] { typeof(LocalDateTime) }), "week" },
            { typeof(LocalDateTimeExtensions).GetRuntimeMethod(nameof(LocalDateTimeExtensions.Microsecond), new[] { typeof(LocalDateTime) }), "microsecond" },
        };

        private static readonly Dictionary<MethodInfo, string> _methodInfoContainsMapping = new Dictionary<MethodInfo, string>
        {
            { BaseNodaTimeMethodCallTranslator.ContainsMethod.MakeGenericMethod(typeof(LocalDateTime)) , "contains" },
        };

        private static readonly Dictionary<MethodInfo, string> _methodInfoDateDiffMapping = new Dictionary<MethodInfo, string>
        {
            {
                typeof(SqlServerNodaTimeDbFunctionsExtensions).GetRuntimeMethod(
                    nameof(SqlServerNodaTimeDbFunctionsExtensions.DateDiffYear),
                    new[] { typeof(DbFunctions), typeof(LocalDateTime), typeof(LocalDateTime) }),
                "YEAR"
            },
            {
                typeof(SqlServerNodaTimeDbFunctionsExtensions).GetRuntimeMethod(
                    nameof(SqlServerNodaTimeDbFunctionsExtensions.DateDiffYear),
                    new[] { typeof(DbFunctions), typeof(LocalDateTime?), typeof(LocalDateTime?) }),
                "YEAR"
            },
            {
                typeof(SqlServerNodaTimeDbFunctionsExtensions).GetRuntimeMethod(
                    nameof(SqlServerNodaTimeDbFunctionsExtensions.DateDiffMonth),
                    new[] { typeof(DbFunctions), typeof(LocalDateTime), typeof(LocalDateTime) }),
                "MONTH"
            },
            {
                typeof(SqlServerNodaTimeDbFunctionsExtensions).GetRuntimeMethod(
                    nameof(SqlServerNodaTimeDbFunctionsExtensions.DateDiffMonth),
                    new[] { typeof(DbFunctions), typeof(LocalDateTime?), typeof(LocalDateTime?) }),
                "MONTH"
            },
            {
                typeof(SqlServerNodaTimeDbFunctionsExtensions).GetRuntimeMethod(
                    nameof(SqlServerNodaTimeDbFunctionsExtensions.DateDiffDay),
                    new[] { typeof(DbFunctions), typeof(LocalDateTime), typeof(LocalDateTime) }),
                "DAY"
            },
            {
                typeof(SqlServerNodaTimeDbFunctionsExtensions).GetRuntimeMethod(
                    nameof(SqlServerNodaTimeDbFunctionsExtensions.DateDiffDay),
                    new[] { typeof(DbFunctions), typeof(LocalDateTime?), typeof(LocalDateTime?) }),
                "DAY"
            },
            {
                typeof(SqlServerNodaTimeDbFunctionsExtensions).GetRuntimeMethod(
                    nameof(SqlServerNodaTimeDbFunctionsExtensions.DateDiffWeek),
                    new[] { typeof(DbFunctions), typeof(LocalDateTime), typeof(LocalDateTime) }),
                "WEEK"
            },
            {
                typeof(SqlServerNodaTimeDbFunctionsExtensions).GetRuntimeMethod(
                    nameof(SqlServerNodaTimeDbFunctionsExtensions.DateDiffWeek),
                    new[] { typeof(DbFunctions), typeof(LocalDateTime?), typeof(LocalDateTime?) }),
                "WEEK"
            },
            {
                typeof(SqlServerNodaTimeDbFunctionsExtensions).GetRuntimeMethod(
                    nameof(SqlServerNodaTimeDbFunctionsExtensions.DateDiffHour),
                    new[] { typeof(DbFunctions), typeof(LocalDateTime), typeof(LocalDateTime) }),
                "HOUR"
            },
            {
                typeof(SqlServerNodaTimeDbFunctionsExtensions).GetRuntimeMethod(
                    nameof(SqlServerNodaTimeDbFunctionsExtensions.DateDiffHour),
                    new[] { typeof(DbFunctions), typeof(LocalDateTime?), typeof(LocalDateTime?) }),
                "HOUR"
            },
            {
                typeof(SqlServerNodaTimeDbFunctionsExtensions).GetRuntimeMethod(
                    nameof(SqlServerNodaTimeDbFunctionsExtensions.DateDiffMinute),
                    new[] { typeof(DbFunctions), typeof(LocalDateTime), typeof(LocalDateTime) }),
                "MINUTE"
            },
            {
                typeof(SqlServerNodaTimeDbFunctionsExtensions).GetRuntimeMethod(
                    nameof(SqlServerNodaTimeDbFunctionsExtensions.DateDiffMinute),
                    new[] { typeof(DbFunctions), typeof(LocalDateTime?), typeof(LocalDateTime?) }),
                "MINUTE"
            },
            {
                typeof(SqlServerNodaTimeDbFunctionsExtensions).GetRuntimeMethod(
                    nameof(SqlServerNodaTimeDbFunctionsExtensions.DateDiffSecond),
                    new[] { typeof(DbFunctions), typeof(LocalDateTime), typeof(LocalDateTime) }),
                "SECOND"
            },
            {
                typeof(SqlServerNodaTimeDbFunctionsExtensions).GetRuntimeMethod(
                    nameof(SqlServerNodaTimeDbFunctionsExtensions.DateDiffSecond),
                    new[] { typeof(DbFunctions), typeof(LocalDateTime?), typeof(LocalDateTime?) }),
                "SECOND"
            },
            {
                typeof(SqlServerNodaTimeDbFunctionsExtensions).GetRuntimeMethod(
                    nameof(SqlServerNodaTimeDbFunctionsExtensions.DateDiffMillisecond),
                    new[] { typeof(DbFunctions), typeof(LocalDateTime), typeof(LocalDateTime) }),
                "MILLISECOND"
            },
            {
                typeof(SqlServerNodaTimeDbFunctionsExtensions).GetRuntimeMethod(
                    nameof(SqlServerNodaTimeDbFunctionsExtensions.DateDiffMillisecond),
                    new[] { typeof(DbFunctions), typeof(LocalDateTime?), typeof(LocalDateTime?) }),
                "MILLISECOND"
            },
            {
                typeof(SqlServerNodaTimeDbFunctionsExtensions).GetRuntimeMethod(
                    nameof(SqlServerNodaTimeDbFunctionsExtensions.DateDiffMicrosecond),
                    new[] { typeof(DbFunctions), typeof(LocalDateTime), typeof(LocalDateTime) }),
                "MICROSECOND"
            },
            {
                typeof(SqlServerNodaTimeDbFunctionsExtensions).GetRuntimeMethod(
                    nameof(SqlServerNodaTimeDbFunctionsExtensions.DateDiffMicrosecond),
                    new[] { typeof(DbFunctions), typeof(LocalDateTime?), typeof(LocalDateTime?) }),
                "MICROSECOND"
            },
            {
                typeof(SqlServerNodaTimeDbFunctionsExtensions).GetRuntimeMethod(
                    nameof(SqlServerNodaTimeDbFunctionsExtensions.DateDiffNanosecond),
                    new[] { typeof(DbFunctions), typeof(LocalDateTime), typeof(LocalDateTime) }),
                "NANOSECOND"
            },
            {
                typeof(SqlServerNodaTimeDbFunctionsExtensions).GetRuntimeMethod(
                    nameof(SqlServerNodaTimeDbFunctionsExtensions.DateDiffNanosecond),
                    new[] { typeof(DbFunctions), typeof(LocalDateTime?), typeof(LocalDateTime?) }),
                "NANOSECOND"
            },
        };

        private static readonly Dictionary<MethodInfo, string> _methodInfoDateDiffBigMapping = new Dictionary<MethodInfo, string>
        {
            {
                typeof(SqlServerNodaTimeDbFunctionsExtensions).GetRuntimeMethod(
                    nameof(SqlServerNodaTimeDbFunctionsExtensions.DateDiffBigSecond),
                    new[] { typeof(DbFunctions), typeof(LocalDateTime), typeof(LocalDateTime) }),
                "SECOND"
            },
            {
                typeof(SqlServerNodaTimeDbFunctionsExtensions).GetRuntimeMethod(
                    nameof(SqlServerNodaTimeDbFunctionsExtensions.DateDiffBigSecond),
                    new[] { typeof(DbFunctions), typeof(LocalDateTime?), typeof(LocalDateTime?) }),
                "SECOND"
            },
            {
                typeof(SqlServerNodaTimeDbFunctionsExtensions).GetRuntimeMethod(
                    nameof(SqlServerNodaTimeDbFunctionsExtensions.DateDiffBigMillisecond),
                    new[] { typeof(DbFunctions), typeof(LocalDateTime), typeof(LocalDateTime) }),
                "MILLISECOND"
            },
            {
                typeof(SqlServerNodaTimeDbFunctionsExtensions).GetRuntimeMethod(
                    nameof(SqlServerNodaTimeDbFunctionsExtensions.DateDiffBigMillisecond),
                    new[] { typeof(DbFunctions), typeof(LocalDateTime?), typeof(LocalDateTime?) }),
                "MILLISECOND"
            },
            {
                typeof(SqlServerNodaTimeDbFunctionsExtensions).GetRuntimeMethod(
                    nameof(SqlServerNodaTimeDbFunctionsExtensions.DateDiffBigMicrosecond),
                    new[] { typeof(DbFunctions), typeof(LocalDateTime), typeof(LocalDateTime) }),
                "MICROSECOND"
            },
            {
                typeof(SqlServerNodaTimeDbFunctionsExtensions).GetRuntimeMethod(
                    nameof(SqlServerNodaTimeDbFunctionsExtensions.DateDiffBigMicrosecond),
                    new[] { typeof(DbFunctions), typeof(LocalDateTime?), typeof(LocalDateTime?) }),
                "MICROSECOND"
            },
            {
                typeof(SqlServerNodaTimeDbFunctionsExtensions).GetRuntimeMethod(
                    nameof(SqlServerNodaTimeDbFunctionsExtensions.DateDiffBigNanosecond),
                    new[] { typeof(DbFunctions), typeof(LocalDateTime), typeof(LocalDateTime) }),
                "NANOSECOND"
            },
            {
                typeof(SqlServerNodaTimeDbFunctionsExtensions).GetRuntimeMethod(
                    nameof(SqlServerNodaTimeDbFunctionsExtensions.DateDiffBigNanosecond),
                    new[] { typeof(DbFunctions), typeof(LocalDateTime?), typeof(LocalDateTime?) }),
                "NANOSECOND"
            },
        };

        public LocalDateTimeMethodTranslator(ISqlExpressionFactory sqlExpressionFactory)
            : base(sqlExpressionFactory, _methodInfoDateAddMapping, _methodInfoDateAddExtensionMapping, _methodInfoDatePartExtensionMapping, _methodInfoDateDiffMapping, _methodInfoDateDiffBigMapping, _methodInfoContainsMapping)
        {
        }
    }
}
