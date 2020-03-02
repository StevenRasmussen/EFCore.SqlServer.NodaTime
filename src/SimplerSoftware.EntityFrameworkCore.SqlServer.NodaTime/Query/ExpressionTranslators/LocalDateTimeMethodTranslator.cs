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

        public LocalDateTimeMethodTranslator(ISqlExpressionFactory sqlExpressionFactory)
            : base(sqlExpressionFactory, _methodInfoDateAddMapping, _methodInfoDateAddExtensionMapping, _methodInfoDatePartExtensionMapping)
        {
        }
    }
}
