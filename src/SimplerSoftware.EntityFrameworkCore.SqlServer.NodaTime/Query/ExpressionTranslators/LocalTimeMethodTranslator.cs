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


        public LocalTimeMethodTranslator(ISqlExpressionFactory sqlExpressionFactory)
            : base(sqlExpressionFactory, _methodInfoDateAddMapping, _methodInfoDateAddExtensionMapping, _methodInfoDatePartExtensionMapping)
        {
        }
    }
}
