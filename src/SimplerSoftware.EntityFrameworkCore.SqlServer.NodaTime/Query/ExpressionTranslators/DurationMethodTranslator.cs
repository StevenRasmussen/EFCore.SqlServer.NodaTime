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

        public DurationMethodTranslator(ISqlExpressionFactory sqlExpressionFactory)
            :base(sqlExpressionFactory, null, _methodInfoDateAddExtensionMapping, _methodInfoDatePartExtensionMapping)
        {
        }
    }
}
