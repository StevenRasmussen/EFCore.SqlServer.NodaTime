using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.EntityFrameworkCore.SqlServer.Query.ExpressionTranslators
{
    public class LocalDateMemberTranslatorPlugin : RelationalMemberTranslatorProvider
    {
        public LocalDateMemberTranslatorPlugin([NotNull] RelationalMemberTranslatorProviderDependencies dependencies) 
            : base(dependencies)
        {
            var sqlExpressionFactory = dependencies.SqlExpressionFactory;

            AddTranslators(
                new IMemberTranslator[]
                {
                    new LocalDateMemberTranslator(sqlExpressionFactory),
                });
        }
    }
}
