using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.SqlServer.NodaTime.Extensions;
using NodaTime;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Microsoft.EntityFrameworkCore.SqlServer.Query.ExpressionTranslators
{
    public class DurationMethodTranslator : BaseNodaTimeMethodCallTranslator
    {
        private static readonly Dictionary<MethodInfo, string> _methodInfoDateAddExtensionMapping = new Dictionary<MethodInfo, string>
        {
            { typeof(DurationExtensions).GetRuntimeMethod(nameof(DurationExtensions.PlusHours), new[] { typeof(Duration), typeof(double) }), "hour" },
            { typeof(DurationExtensions).GetRuntimeMethod(nameof(DurationExtensions.PlusMinutes), new[] { typeof(Duration), typeof(double) }), "minute" },
            { typeof(DurationExtensions).GetRuntimeMethod(nameof(DurationExtensions.PlusSeconds), new[] { typeof(Duration), typeof(double) }), "second" },
            { typeof(DurationExtensions).GetRuntimeMethod(nameof(DurationExtensions.PlusMilliseconds), new[] { typeof(Duration), typeof(double) }), "millisecond" },
            { typeof(DurationExtensions).GetRuntimeMethod(nameof(DurationExtensions.PlusMicroseconds), new[] { typeof(Duration), typeof(double) }), "microsecond" },
            { typeof(DurationExtensions).GetRuntimeMethod(nameof(DurationExtensions.PlusNanoseconds), new[] { typeof(Duration), typeof(double) }), "nanosecond" },
        };

        private static readonly Dictionary<MethodInfo, string> _methodInfoDatePartExtensionMapping = new Dictionary<MethodInfo, string>
        {
            { typeof(DurationExtensions).GetRuntimeMethod(nameof(DurationExtensions.Microseconds), new[] { typeof(Duration) }), "microsecond" },
        };

        private static readonly Dictionary<MethodInfo, string> _methodInfoContainsMapping = new Dictionary<MethodInfo, string>
        {
            { BaseNodaTimeMethodCallTranslator.ContainsMethod.MakeGenericMethod(typeof(Duration)) , "contains" },
        };

        private static readonly Dictionary<MethodInfo, string> _methodInfoDateDiffMapping = new Dictionary<MethodInfo, string>
        {
            {
                typeof(SqlServerNodaTimeDbFunctionsExtensions).GetRuntimeMethod(
                    nameof(SqlServerNodaTimeDbFunctionsExtensions.DateDiffHour),
                    new[] { typeof(DbFunctions), typeof(Duration), typeof(Duration) }),
                "HOUR"
            },
            {
                typeof(SqlServerNodaTimeDbFunctionsExtensions).GetRuntimeMethod(
                    nameof(SqlServerNodaTimeDbFunctionsExtensions.DateDiffHour),
                    new[] { typeof(DbFunctions), typeof(Duration?), typeof(Duration?) }),
                "HOUR"
            },
            {
                typeof(SqlServerNodaTimeDbFunctionsExtensions).GetRuntimeMethod(
                    nameof(SqlServerNodaTimeDbFunctionsExtensions.DateDiffMinute),
                    new[] { typeof(DbFunctions), typeof(Duration), typeof(Duration) }),
                "MINUTE"
            },
            {
                typeof(SqlServerNodaTimeDbFunctionsExtensions).GetRuntimeMethod(
                    nameof(SqlServerNodaTimeDbFunctionsExtensions.DateDiffMinute),
                    new[] { typeof(DbFunctions), typeof(Duration?), typeof(Duration?) }),
                "MINUTE"
            },
            {
                typeof(SqlServerNodaTimeDbFunctionsExtensions).GetRuntimeMethod(
                    nameof(SqlServerNodaTimeDbFunctionsExtensions.DateDiffSecond),
                    new[] { typeof(DbFunctions), typeof(Duration), typeof(Duration) }),
                "SECOND"
            },
            {
                typeof(SqlServerNodaTimeDbFunctionsExtensions).GetRuntimeMethod(
                    nameof(SqlServerNodaTimeDbFunctionsExtensions.DateDiffSecond),
                    new[] { typeof(DbFunctions), typeof(Duration?), typeof(Duration?) }),
                "SECOND"
            },
            {
                typeof(SqlServerNodaTimeDbFunctionsExtensions).GetRuntimeMethod(
                    nameof(SqlServerNodaTimeDbFunctionsExtensions.DateDiffMillisecond),
                    new[] { typeof(DbFunctions), typeof(Duration), typeof(Duration) }),
                "MILLISECOND"
            },
            {
                typeof(SqlServerNodaTimeDbFunctionsExtensions).GetRuntimeMethod(
                    nameof(SqlServerNodaTimeDbFunctionsExtensions.DateDiffMillisecond),
                    new[] { typeof(DbFunctions), typeof(Duration?), typeof(Duration?) }),
                "MILLISECOND"
            },
            {
                typeof(SqlServerNodaTimeDbFunctionsExtensions).GetRuntimeMethod(
                    nameof(SqlServerNodaTimeDbFunctionsExtensions.DateDiffMicrosecond),
                    new[] { typeof(DbFunctions), typeof(Duration), typeof(Duration) }),
                "MICROSECOND"
            },
            {
                typeof(SqlServerNodaTimeDbFunctionsExtensions).GetRuntimeMethod(
                    nameof(SqlServerNodaTimeDbFunctionsExtensions.DateDiffMicrosecond),
                    new[] { typeof(DbFunctions), typeof(Duration?), typeof(Duration?) }),
                "MICROSECOND"
            },
            {
                typeof(SqlServerNodaTimeDbFunctionsExtensions).GetRuntimeMethod(
                    nameof(SqlServerNodaTimeDbFunctionsExtensions.DateDiffNanosecond),
                    new[] { typeof(DbFunctions), typeof(Duration), typeof(Duration) }),
                "NANOSECOND"
            },
            {
                typeof(SqlServerNodaTimeDbFunctionsExtensions).GetRuntimeMethod(
                    nameof(SqlServerNodaTimeDbFunctionsExtensions.DateDiffNanosecond),
                    new[] { typeof(DbFunctions), typeof(Duration?), typeof(Duration?) }),
                "NANOSECOND"
            },
        };

        private static readonly Dictionary<MethodInfo, string> _methodInfoDateDiffBigMapping = new Dictionary<MethodInfo, string>
        {
            {
                typeof(SqlServerNodaTimeDbFunctionsExtensions).GetRuntimeMethod(
                    nameof(SqlServerNodaTimeDbFunctionsExtensions.DateDiffBigSecond),
                    new[] { typeof(DbFunctions), typeof(Duration), typeof(Duration) }),
                "SECOND"
            },
            {
                typeof(SqlServerNodaTimeDbFunctionsExtensions).GetRuntimeMethod(
                    nameof(SqlServerNodaTimeDbFunctionsExtensions.DateDiffBigSecond),
                    new[] { typeof(DbFunctions), typeof(Duration?), typeof(Duration?) }),
                "SECOND"
            },
            {
                typeof(SqlServerNodaTimeDbFunctionsExtensions).GetRuntimeMethod(
                    nameof(SqlServerNodaTimeDbFunctionsExtensions.DateDiffBigMillisecond),
                    new[] { typeof(DbFunctions), typeof(Duration), typeof(Duration) }),
                "MILLISECOND"
            },
            {
                typeof(SqlServerNodaTimeDbFunctionsExtensions).GetRuntimeMethod(
                    nameof(SqlServerNodaTimeDbFunctionsExtensions.DateDiffBigMillisecond),
                    new[] { typeof(DbFunctions), typeof(Duration?), typeof(Duration?) }),
                "MILLISECOND"
            },
            {
                typeof(SqlServerNodaTimeDbFunctionsExtensions).GetRuntimeMethod(
                    nameof(SqlServerNodaTimeDbFunctionsExtensions.DateDiffBigMicrosecond),
                    new[] { typeof(DbFunctions), typeof(Duration), typeof(Duration) }),
                "MICROSECOND"
            },
            {
                typeof(SqlServerNodaTimeDbFunctionsExtensions).GetRuntimeMethod(
                    nameof(SqlServerNodaTimeDbFunctionsExtensions.DateDiffBigMicrosecond),
                    new[] { typeof(DbFunctions), typeof(Duration?), typeof(Duration?) }),
                "MICROSECOND"
            },
            {
                typeof(SqlServerNodaTimeDbFunctionsExtensions).GetRuntimeMethod(
                    nameof(SqlServerNodaTimeDbFunctionsExtensions.DateDiffBigNanosecond),
                    new[] { typeof(DbFunctions), typeof(Duration), typeof(Duration) }),
                "NANOSECOND"
            },
            {
                typeof(SqlServerNodaTimeDbFunctionsExtensions).GetRuntimeMethod(
                    nameof(SqlServerNodaTimeDbFunctionsExtensions.DateDiffBigNanosecond),
                    new[] { typeof(DbFunctions), typeof(Duration?), typeof(Duration?) }),
                "NANOSECOND"
            },
        };

        public DurationMethodTranslator(ISqlExpressionFactory sqlExpressionFactory)
            : base(sqlExpressionFactory, null, _methodInfoDateAddExtensionMapping, _methodInfoDatePartExtensionMapping, _methodInfoDateDiffMapping, _methodInfoDateDiffBigMapping, _methodInfoContainsMapping)
        {
        }
    }
}
