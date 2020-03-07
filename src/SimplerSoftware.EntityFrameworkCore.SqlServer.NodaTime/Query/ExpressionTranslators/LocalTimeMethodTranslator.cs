using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.SqlServer.NodaTime.Extensions;
using NodaTime;
using System.Collections.Generic;
using System.Reflection;

namespace Microsoft.EntityFrameworkCore.SqlServer.Query.ExpressionTranslators
{
    internal class LocalTimeMethodTranslator : BaseNodaTimeMethodCallTranslator
    {
        private static readonly Dictionary<MethodInfo, string> _methodInfoDateAddMapping = new Dictionary<MethodInfo, string>
        {
            { typeof(LocalTime).GetRuntimeMethod(nameof(LocalTime.PlusHours), new[] { typeof(long) }), "hour" },
            { typeof(LocalTime).GetRuntimeMethod(nameof(LocalTime.PlusMinutes), new[] { typeof(long) }), "minute" },
            { typeof(LocalTime).GetRuntimeMethod(nameof(LocalTime.PlusSeconds), new[] { typeof(long) }), "second" },
            { typeof(LocalTime).GetRuntimeMethod(nameof(LocalTime.PlusMilliseconds), new[] { typeof(long) }), "millisecond" },
            { typeof(LocalTime).GetRuntimeMethod(nameof(LocalTime.PlusNanoseconds), new[] { typeof(long) }), "nanosecond" },
        };

        private static readonly Dictionary<MethodInfo, string> _methodInfoDateAddExtensionMapping = new Dictionary<MethodInfo, string>
        {
            { typeof(LocalTimeExtensions).GetRuntimeMethod(nameof(LocalTimeExtensions.PlusMicroseconds), new[] { typeof(LocalTime), typeof(long) }), "microsecond" },
        };

        private static readonly Dictionary<MethodInfo, string> _methodInfoDatePartExtensionMapping = new Dictionary<MethodInfo, string>
        {
            { typeof(LocalTimeExtensions).GetRuntimeMethod(nameof(LocalTimeExtensions.Microsecond), new[] { typeof(LocalTime) }), "microsecond" },
        };

        private static readonly Dictionary<MethodInfo, string> _methodInfoDateDiffMapping = new Dictionary<MethodInfo, string>
        {
            {
                typeof(SqlServerNodaTimeDbFunctionsExtensions).GetRuntimeMethod(
                    nameof(SqlServerNodaTimeDbFunctionsExtensions.DateDiffHour),
                    new[] { typeof(DbFunctions), typeof(LocalTime), typeof(LocalTime) }),
                "HOUR"
            },
            {
                typeof(SqlServerNodaTimeDbFunctionsExtensions).GetRuntimeMethod(
                    nameof(SqlServerNodaTimeDbFunctionsExtensions.DateDiffHour),
                    new[] { typeof(DbFunctions), typeof(LocalTime?), typeof(LocalTime?) }),
                "HOUR"
            },
            {
                typeof(SqlServerNodaTimeDbFunctionsExtensions).GetRuntimeMethod(
                    nameof(SqlServerNodaTimeDbFunctionsExtensions.DateDiffMinute),
                    new[] { typeof(DbFunctions), typeof(LocalTime), typeof(LocalTime) }),
                "MINUTE"
            },
            {
                typeof(SqlServerNodaTimeDbFunctionsExtensions).GetRuntimeMethod(
                    nameof(SqlServerNodaTimeDbFunctionsExtensions.DateDiffMinute),
                    new[] { typeof(DbFunctions), typeof(LocalTime?), typeof(LocalTime?) }),
                "MINUTE"
            },
            {
                typeof(SqlServerNodaTimeDbFunctionsExtensions).GetRuntimeMethod(
                    nameof(SqlServerNodaTimeDbFunctionsExtensions.DateDiffSecond),
                    new[] { typeof(DbFunctions), typeof(LocalTime), typeof(LocalTime) }),
                "SECOND"
            },
            {
                typeof(SqlServerNodaTimeDbFunctionsExtensions).GetRuntimeMethod(
                    nameof(SqlServerNodaTimeDbFunctionsExtensions.DateDiffSecond),
                    new[] { typeof(DbFunctions), typeof(LocalTime?), typeof(LocalTime?) }),
                "SECOND"
            },
            {
                typeof(SqlServerNodaTimeDbFunctionsExtensions).GetRuntimeMethod(
                    nameof(SqlServerNodaTimeDbFunctionsExtensions.DateDiffMillisecond),
                    new[] { typeof(DbFunctions), typeof(LocalTime), typeof(LocalTime) }),
                "MILLISECOND"
            },
            {
                typeof(SqlServerNodaTimeDbFunctionsExtensions).GetRuntimeMethod(
                    nameof(SqlServerNodaTimeDbFunctionsExtensions.DateDiffMillisecond),
                    new[] { typeof(DbFunctions), typeof(LocalTime?), typeof(LocalTime?) }),
                "MILLISECOND"
            },
            {
                typeof(SqlServerNodaTimeDbFunctionsExtensions).GetRuntimeMethod(
                    nameof(SqlServerNodaTimeDbFunctionsExtensions.DateDiffMicrosecond),
                    new[] { typeof(DbFunctions), typeof(LocalTime), typeof(LocalTime) }),
                "MICROSECOND"
            },
            {
                typeof(SqlServerNodaTimeDbFunctionsExtensions).GetRuntimeMethod(
                    nameof(SqlServerNodaTimeDbFunctionsExtensions.DateDiffMicrosecond),
                    new[] { typeof(DbFunctions), typeof(LocalTime?), typeof(LocalTime?) }),
                "MICROSECOND"
            },
            {
                typeof(SqlServerNodaTimeDbFunctionsExtensions).GetRuntimeMethod(
                    nameof(SqlServerNodaTimeDbFunctionsExtensions.DateDiffNanosecond),
                    new[] { typeof(DbFunctions), typeof(LocalTime), typeof(LocalTime) }),
                "NANOSECOND"
            },
            {
                typeof(SqlServerNodaTimeDbFunctionsExtensions).GetRuntimeMethod(
                    nameof(SqlServerNodaTimeDbFunctionsExtensions.DateDiffNanosecond),
                    new[] { typeof(DbFunctions), typeof(LocalTime?), typeof(LocalTime?) }),
                "NANOSECOND"
            },
        };

        private static readonly Dictionary<MethodInfo, string> _methodInfoDateDiffBigMapping = new Dictionary<MethodInfo, string>
        {
            {
                typeof(SqlServerNodaTimeDbFunctionsExtensions).GetRuntimeMethod(
                    nameof(SqlServerNodaTimeDbFunctionsExtensions.DateDiffBigSecond),
                    new[] { typeof(DbFunctions), typeof(LocalTime), typeof(LocalTime) }),
                "SECOND"
            },
            {
                typeof(SqlServerNodaTimeDbFunctionsExtensions).GetRuntimeMethod(
                    nameof(SqlServerNodaTimeDbFunctionsExtensions.DateDiffBigSecond),
                    new[] { typeof(DbFunctions), typeof(LocalTime?), typeof(LocalTime?) }),
                "SECOND"
            },
            {
                typeof(SqlServerNodaTimeDbFunctionsExtensions).GetRuntimeMethod(
                    nameof(SqlServerNodaTimeDbFunctionsExtensions.DateDiffBigMillisecond),
                    new[] { typeof(DbFunctions), typeof(LocalTime), typeof(LocalTime) }),
                "MILLISECOND"
            },
            {
                typeof(SqlServerNodaTimeDbFunctionsExtensions).GetRuntimeMethod(
                    nameof(SqlServerNodaTimeDbFunctionsExtensions.DateDiffBigMillisecond),
                    new[] { typeof(DbFunctions), typeof(LocalTime?), typeof(LocalTime?) }),
                "MILLISECOND"
            },
            {
                typeof(SqlServerNodaTimeDbFunctionsExtensions).GetRuntimeMethod(
                    nameof(SqlServerNodaTimeDbFunctionsExtensions.DateDiffBigMicrosecond),
                    new[] { typeof(DbFunctions), typeof(LocalTime), typeof(LocalTime) }),
                "MICROSECOND"
            },
            {
                typeof(SqlServerNodaTimeDbFunctionsExtensions).GetRuntimeMethod(
                    nameof(SqlServerNodaTimeDbFunctionsExtensions.DateDiffBigMicrosecond),
                    new[] { typeof(DbFunctions), typeof(LocalTime?), typeof(LocalTime?) }),
                "MICROSECOND"
            },
            {
                typeof(SqlServerNodaTimeDbFunctionsExtensions).GetRuntimeMethod(
                    nameof(SqlServerNodaTimeDbFunctionsExtensions.DateDiffBigNanosecond),
                    new[] { typeof(DbFunctions), typeof(LocalTime), typeof(LocalTime) }),
                "NANOSECOND"
            },
            {
                typeof(SqlServerNodaTimeDbFunctionsExtensions).GetRuntimeMethod(
                    nameof(SqlServerNodaTimeDbFunctionsExtensions.DateDiffBigNanosecond),
                    new[] { typeof(DbFunctions), typeof(LocalTime?), typeof(LocalTime?) }),
                "NANOSECOND"
            },
        };

        public LocalTimeMethodTranslator(ISqlExpressionFactory sqlExpressionFactory)
            : base(sqlExpressionFactory, _methodInfoDateAddMapping, _methodInfoDateAddExtensionMapping, _methodInfoDatePartExtensionMapping, _methodInfoDateDiffMapping, _methodInfoDateDiffBigMapping)
        {
        }
    }
}
