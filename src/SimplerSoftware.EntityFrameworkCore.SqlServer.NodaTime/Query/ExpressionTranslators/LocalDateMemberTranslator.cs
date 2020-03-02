using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using NodaTime;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

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
