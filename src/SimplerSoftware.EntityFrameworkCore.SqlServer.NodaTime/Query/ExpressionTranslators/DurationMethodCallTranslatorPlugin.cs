using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.EntityFrameworkCore.SqlServer.Query.ExpressionTranslators
{
    internal class DurationMethodCallTranslatorPlugin : IMethodCallTranslatorPlugin
    {
        public DurationMethodCallTranslatorPlugin(
            IRelationalTypeMappingSource typeMappingSource,
            ISqlExpressionFactory sqlExpressionFactory)
        {
            Translators = new IMethodCallTranslator[]
            {
                new DurationMethodTranslator(typeMappingSource, sqlExpressionFactory)
            };
        }

        public virtual IEnumerable<IMethodCallTranslator> Translators { get; }
    }
}
