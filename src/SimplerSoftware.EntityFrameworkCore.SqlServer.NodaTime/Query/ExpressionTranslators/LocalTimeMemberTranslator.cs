using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Query;
using NodaTime;
using System.Collections.Generic;

namespace Microsoft.EntityFrameworkCore.SqlServer.Query.ExpressionTranslators
{
    public class LocalTimeMemberTranslator : BaseNodaTimeMemberTranslator
    {
        private static readonly Dictionary<string, string> _datePartMapping
            = new Dictionary<string, string>
            {
                { nameof(LocalTime.Hour), "hour" },
                { nameof(LocalTime.Minute), "minute" },
                { nameof(LocalTime.Second), "second" },
                { nameof(LocalTime.Millisecond), "millisecond" },
                { nameof(LocalTime.NanosecondOfSecond), "nanosecond" },
            };

        public LocalTimeMemberTranslator([NotNull] ISqlExpressionFactory sqlExpressionFactory)
            : base(sqlExpressionFactory, typeof(LocalTime), _datePartMapping)
        {
        }
    }
}
