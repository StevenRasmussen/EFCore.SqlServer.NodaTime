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

        public LocalDateMethodTranslator(ISqlExpressionFactory sqlExpressionFactory)
            : base(sqlExpressionFactory, _methodInfoDateAddMapping, _methodInfoDateAddExtensionMapping, _methodInfoDatePartExtensionMapping)
        {
        }
    }
}
