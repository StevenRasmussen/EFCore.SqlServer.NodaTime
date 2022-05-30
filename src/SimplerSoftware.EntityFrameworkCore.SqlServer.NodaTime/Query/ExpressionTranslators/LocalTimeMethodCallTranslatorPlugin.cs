using Microsoft.EntityFrameworkCore.Query;
using System.Collections.Generic;

namespace Microsoft.EntityFrameworkCore.SqlServer.Query.ExpressionTranslators
{
    internal class LocalTimeMethodCallTranslatorPlugin : IMethodCallTranslatorPlugin
    {
        public LocalTimeMethodCallTranslatorPlugin(
            ISqlExpressionFactory sqlExpressionFactory)
        {
            Translators = new IMethodCallTranslator[]
            {
                new LocalTimeMethodTranslator(sqlExpressionFactory)
            };
        }

        public virtual IEnumerable<IMethodCallTranslator> Translators { get; }
    }
}
