using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.EntityFrameworkCore.SqlServer.NodaTime.Extensions;
using Microsoft.EntityFrameworkCore.SqlServer.Storage;
using Microsoft.EntityFrameworkCore.Storage;
using NodaTime;
using System.Collections.Generic;
using System.Reflection;

namespace Microsoft.EntityFrameworkCore.SqlServer.Query.ExpressionTranslators
{
    internal class InstantMethodTranslator : IMethodCallTranslator
    {
        private readonly Dictionary<MethodInfo, string> _methodInfoDatePartMapping = new Dictionary<MethodInfo, string>
        {
            { typeof(InstantExtensions).GetRuntimeMethod(nameof(InstantExtensions.AddYears), new[] { typeof(Instant), typeof(int) }), "year" },
            { typeof(InstantExtensions).GetRuntimeMethod(nameof(InstantExtensions.AddMonths), new[] { typeof(Instant), typeof(int) }), "month" },
            { typeof(InstantExtensions).GetRuntimeMethod(nameof(InstantExtensions.AddDays), new[] { typeof(Instant), typeof(double) }), "day" },
            { typeof(InstantExtensions).GetRuntimeMethod(nameof(InstantExtensions.AddHours), new[] { typeof(Instant), typeof(double) }), "hour" },
            { typeof(InstantExtensions).GetRuntimeMethod(nameof(InstantExtensions.AddMinutes), new[] { typeof(Instant), typeof(double) }), "minute" },
            { typeof(InstantExtensions).GetRuntimeMethod(nameof(InstantExtensions.AddSeconds), new[] { typeof(Instant), typeof(double) }), "second" },
            { typeof(InstantExtensions).GetRuntimeMethod(nameof(InstantExtensions.AddMilliseconds), new[] { typeof(Instant), typeof(double) }), "millisecond" },
        };

        private readonly IRelationalTypeMappingSource _typeMappingSource;
        private readonly ISqlExpressionFactory _sqlExpressionFactory;

        public InstantMethodTranslator(
            IRelationalTypeMappingSource typeMappingSource,
            ISqlExpressionFactory sqlExpressionFactory)
        {
            _typeMappingSource = typeMappingSource;
            _sqlExpressionFactory = sqlExpressionFactory;
        }

        public virtual SqlExpression Translate(SqlExpression instance, MethodInfo method, IReadOnlyList<SqlExpression> arguments)
        {
            if (_methodInfoDatePartMapping.TryGetValue(method, out var datePart))
            {
                return !datePart.Equals("year")
                    && !datePart.Equals("month")
                    && arguments[0] is SqlConstantExpression sqlConstant
                    && ((double)sqlConstant.Value >= int.MaxValue
                        || (double)sqlConstant.Value <= int.MinValue)
                        ? null
                        : _sqlExpressionFactory.Function(
                            "DATEADD",
                            new[]
                            {
                                _sqlExpressionFactory.Fragment(datePart),
                                _sqlExpressionFactory.Convert(arguments[1], typeof(int)),
                                arguments[0]
                            },
                            arguments[0].Type,
                            arguments[0].TypeMapping);
            }

            return null;
        }
    }
}
