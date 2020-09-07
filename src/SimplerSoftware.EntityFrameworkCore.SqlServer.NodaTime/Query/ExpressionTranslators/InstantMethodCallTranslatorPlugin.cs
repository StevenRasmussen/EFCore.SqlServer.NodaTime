using Microsoft.EntityFrameworkCore.Query;
using System.Collections.Generic;

namespace Microsoft.EntityFrameworkCore.SqlServer.Query.ExpressionTranslators
{
    internal class InstantMethodCallTranslatorPlugin : IMethodCallTranslatorPlugin
    {
        public InstantMethodCallTranslatorPlugin(
            ISqlExpressionFactory sqlExpressionFactory)
        {
            Translators = new IMethodCallTranslator[]
            {
                new InstantMethodTranslator(sqlExpressionFactory)
            };
        }

        public virtual IEnumerable<IMethodCallTranslator> Translators { get; }
    }
}
