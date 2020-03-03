using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.EntityFrameworkCore.SqlServer.Query.ExpressionTranslators
{
    public class NodaTimeTypesMemberTranslatorPlugin : RelationalMemberTranslatorProvider
    {
        public NodaTimeTypesMemberTranslatorPlugin([NotNull] RelationalMemberTranslatorProviderDependencies dependencies) 
            : base(dependencies)
        {
            var sqlExpressionFactory = dependencies.SqlExpressionFactory;

            AddTranslators(
                new IMemberTranslator[]
                {
                    new LocalDateMemberTranslator(sqlExpressionFactory),
                    new LocalTimeMemberTranslator(sqlExpressionFactory),
                    new LocalDateTimeMemberTranslator(sqlExpressionFactory),
                    new DurationMemberTranslator(sqlExpressionFactory),
                });
        }
    }
}
