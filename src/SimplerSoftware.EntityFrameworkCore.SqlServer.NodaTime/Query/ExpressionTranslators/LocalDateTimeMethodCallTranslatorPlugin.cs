using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.EntityFrameworkCore.SqlServer.Query.ExpressionTranslators
{
    internal class LocalDateTimeMethodCallTranslatorPlugin : IMethodCallTranslatorPlugin
    {
        public LocalDateTimeMethodCallTranslatorPlugin(
            IRelationalTypeMappingSource typeMappingSource,
            ISqlExpressionFactory sqlExpressionFactory)
        {
            Translators = new IMethodCallTranslator[]
            {
                new LocalDateTimeMethodTranslator(typeMappingSource, sqlExpressionFactory)
            };
        }

        public virtual IEnumerable<IMethodCallTranslator> Translators { get; }
    }
}
