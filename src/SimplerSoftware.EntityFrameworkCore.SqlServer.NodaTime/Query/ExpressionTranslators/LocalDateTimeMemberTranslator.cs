using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Query;
using NodaTime;
using System.Collections.Generic;

namespace Microsoft.EntityFrameworkCore.SqlServer.Query.ExpressionTranslators
{
    public class LocalDateTimeMemberTranslator : BaseNodaTimeMemberTranslator
    {
        private static readonly Dictionary<string, string> _datePartMapping
            = new Dictionary<string, string>
            {
                { nameof(LocalDateTime.Year), "year" },
                { nameof(LocalDateTime.Month), "month" },
                { nameof(LocalDateTime.DayOfYear), "dayofyear" },
                { nameof(LocalDateTime.Day), "day" },
                { nameof(LocalDateTime.Hour), "hour" },
                { nameof(LocalDateTime.Minute), "minute" },
                { nameof(LocalDateTime.Second), "second" },
                { nameof(LocalDateTime.Millisecond), "millisecond" },
                { nameof(LocalDateTime.NanosecondOfSecond), "nanosecond" },
            };

        public LocalDateTimeMemberTranslator([NotNull] ISqlExpressionFactory sqlExpressionFactory)
            : base(sqlExpressionFactory, typeof(LocalDateTime), _datePartMapping)
        {
        }
    }
}
