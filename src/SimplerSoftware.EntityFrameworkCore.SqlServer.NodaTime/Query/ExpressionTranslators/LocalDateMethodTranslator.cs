using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.SqlServer.NodaTime.Extensions;
using NodaTime;
using System.Collections.Generic;
using System.Reflection;

namespace Microsoft.EntityFrameworkCore.SqlServer.Query.ExpressionTranslators
{
    internal class LocalDateMethodTranslator : BaseNodaTimeMethodCallTranslator
    {
        private static readonly Dictionary<MethodInfo, string> _methodInfoDateAddMapping = new Dictionary<MethodInfo, string>
        {
            { typeof(LocalDate).GetRuntimeMethod(nameof(LocalDate.PlusYears), new[] { typeof(int) }), "year" },
            { typeof(LocalDate).GetRuntimeMethod(nameof(LocalDate.PlusMonths), new[] { typeof(int) }), "month" },
            { typeof(LocalDate).GetRuntimeMethod(nameof(LocalDate.PlusDays), new[] { typeof(int) }), "day" },
            { typeof(LocalDate).GetRuntimeMethod(nameof(LocalDate.PlusWeeks), new[] { typeof(int) }), "week" },
        };

        private static readonly Dictionary<MethodInfo, string> _methodInfoDateAddExtensionMapping = new Dictionary<MethodInfo, string>
        {
            { typeof(LocalDateExtensions).GetRuntimeMethod(nameof(LocalDateExtensions.PlusQuarters), new[] { typeof(LocalDate), typeof(int) }), "quarter" },
        };

        private static readonly Dictionary<MethodInfo, string> _methodInfoDatePartExtensionMapping = new Dictionary<MethodInfo, string>
        {
            { typeof(LocalDateExtensions).GetRuntimeMethod(nameof(LocalDateExtensions.Quarter), new[] { typeof(LocalDate) }), "quarter" },
            { typeof(LocalDateExtensions).GetRuntimeMethod(nameof(LocalDateExtensions.Week), new[] { typeof(LocalDate) }), "week" },
        };

        private static readonly Dictionary<MethodInfo, string> _methodInfoDateDiffMapping = new Dictionary<MethodInfo, string>
        {
            // Local Date
            {
                typeof(SqlServerNodaTimeDbFunctionsExtensions).GetRuntimeMethod(
                    nameof(SqlServerNodaTimeDbFunctionsExtensions.DateDiffYear),
                    new[] { typeof(DbFunctions), typeof(LocalDate), typeof(LocalDate) }),
                "YEAR"
            },
            {
                typeof(SqlServerNodaTimeDbFunctionsExtensions).GetRuntimeMethod(
                    nameof(SqlServerNodaTimeDbFunctionsExtensions.DateDiffYear),
                    new[] { typeof(DbFunctions), typeof(LocalDate?), typeof(LocalDate?) }),
                "YEAR"
            },
            {
                typeof(SqlServerNodaTimeDbFunctionsExtensions).GetRuntimeMethod(
                    nameof(SqlServerNodaTimeDbFunctionsExtensions.DateDiffMonth),
                    new[] { typeof(DbFunctions), typeof(LocalDate), typeof(LocalDate) }),
                "MONTH"
            },
            {
                typeof(SqlServerNodaTimeDbFunctionsExtensions).GetRuntimeMethod(
                    nameof(SqlServerNodaTimeDbFunctionsExtensions.DateDiffMonth),
                    new[] { typeof(DbFunctions), typeof(LocalDate?), typeof(LocalDate?) }),
                "MONTH"
            },
            {
                typeof(SqlServerNodaTimeDbFunctionsExtensions).GetRuntimeMethod(
                    nameof(SqlServerNodaTimeDbFunctionsExtensions.DateDiffDay),
                    new[] { typeof(DbFunctions), typeof(LocalDate), typeof(LocalDate) }),
                "DAY"
            },
            {
                typeof(SqlServerNodaTimeDbFunctionsExtensions).GetRuntimeMethod(
                    nameof(SqlServerNodaTimeDbFunctionsExtensions.DateDiffDay),
                    new[] { typeof(DbFunctions), typeof(LocalDate?), typeof(LocalDate?) }),
                "DAY"
            },
            {
                typeof(SqlServerNodaTimeDbFunctionsExtensions).GetRuntimeMethod(
                    nameof(SqlServerNodaTimeDbFunctionsExtensions.DateDiffWeek),
                    new[] { typeof(DbFunctions), typeof(LocalDate), typeof(LocalDate) }),
                "WEEK"
            },
            {
                typeof(SqlServerNodaTimeDbFunctionsExtensions).GetRuntimeMethod(
                    nameof(SqlServerNodaTimeDbFunctionsExtensions.DateDiffWeek),
                    new[] { typeof(DbFunctions), typeof(LocalDate?), typeof(LocalDate?) }),
                "WEEK"
            },
        };

        public LocalDateMethodTranslator(ISqlExpressionFactory sqlExpressionFactory)
            : base(sqlExpressionFactory, _methodInfoDateAddMapping, _methodInfoDateAddExtensionMapping, _methodInfoDatePartExtensionMapping, _methodInfoDateDiffMapping)
        {
        }
    }
}
