using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Query;
using NodaTime;
using System.Collections.Generic;

namespace Microsoft.EntityFrameworkCore.SqlServer.Query.ExpressionTranslators
{
    public class LocalDateMemberTranslator : BaseNodaTimeMemberTranslator
    {
        private static readonly Dictionary<string, string> _datePartMapping
            = new Dictionary<string, string>
            {
                { nameof(LocalDate.Year), "year" },
                { nameof(LocalDate.Month), "month" },
                { nameof(LocalDate.DayOfYear), "dayofyear" },
                { nameof(LocalDate.Day), "day" },
            };

        public LocalDateMemberTranslator([NotNull] ISqlExpressionFactory sqlExpressionFactory)
            : base(sqlExpressionFactory, typeof(LocalDate), _datePartMapping)
        {
        }
    }
}
