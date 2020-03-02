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
    public class LocalTimeMemberTranslator : IMemberTranslator
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

        private readonly ISqlExpressionFactory _sqlExpressionFactory;

        public LocalTimeMemberTranslator([NotNull] ISqlExpressionFactory sqlExpressionFactory)
        {
            _sqlExpressionFactory = sqlExpressionFactory;
        }

        public SqlExpression Translate(SqlExpression instance, MemberInfo member, Type returnType)
        {
            var declaringType = member.DeclaringType;
            if (declaringType == typeof(LocalTime))
            {
                var memberName = member.Name;

                if (_datePartMapping.TryGetValue(memberName, out var datePart))
                {
                    return _sqlExpressionFactory.Function(
                        "DATEPART",
                        new[] { _sqlExpressionFactory.Fragment(datePart), instance },
                        returnType);
                }
            }

            return null;
        }
    }
}
