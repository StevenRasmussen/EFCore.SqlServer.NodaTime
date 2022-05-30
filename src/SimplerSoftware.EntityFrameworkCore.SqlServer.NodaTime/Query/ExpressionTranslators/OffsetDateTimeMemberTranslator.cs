using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Query;
using NodaTime;
using System.Collections.Generic;

namespace Microsoft.EntityFrameworkCore.SqlServer.Query.ExpressionTranslators
{
    public class OffsetDateTimeMemberTranslator : BaseNodaTimeMemberTranslator
    {
        private static readonly Dictionary<string, string> _datePartMapping
            = new Dictionary<string, string>
            {
                { nameof(OffsetDateTime.Year), "year" },
                { nameof(OffsetDateTime.Month), "month" },
                { nameof(OffsetDateTime.Day), "day" },
                { nameof(OffsetDateTime.DayOfYear), "dayofyear" },
                { nameof(OffsetDateTime.Hour), "hour" },
                { nameof(OffsetDateTime.Minute), "minute" },
                { nameof(OffsetDateTime.Second), "second" },
                { nameof(OffsetDateTime.Millisecond), "millisecond" },
                { nameof(OffsetDateTime.NanosecondOfSecond), "nanosecond" },
            };

        public OffsetDateTimeMemberTranslator([NotNull] ISqlExpressionFactory sqlExpressionFactory) 
            : base(sqlExpressionFactory, typeof(OffsetDateTime), _datePartMapping)
        {
        }
    }
}
