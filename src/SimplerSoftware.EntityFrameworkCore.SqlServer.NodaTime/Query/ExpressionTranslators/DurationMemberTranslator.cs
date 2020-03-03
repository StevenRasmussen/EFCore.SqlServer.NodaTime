using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Query;
using NodaTime;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.EntityFrameworkCore.SqlServer.Query.ExpressionTranslators
{
    public class DurationMemberTranslator : BaseNodaTimeMemberTranslator
    {
        private static readonly Dictionary<string, string> _datePartMapping
            = new Dictionary<string, string>
            {
                { nameof(Duration.Days), "day" },
                { nameof(Duration.Hours), "hour" },
                { nameof(Duration.Minutes), "minute" },
                { nameof(Duration.Seconds), "second" },
                { nameof(Duration.Milliseconds), "millisecond" },
                { nameof(Duration.SubsecondNanoseconds), "nanosecond" },
            };

        public DurationMemberTranslator([NotNull] ISqlExpressionFactory sqlExpressionFactory)
            : base(sqlExpressionFactory, typeof(Duration), _datePartMapping)
        {
        }
    }
}
