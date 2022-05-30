using Microsoft.EntityFrameworkCore.Query;
using System.Collections.Generic;

namespace Microsoft.EntityFrameworkCore.SqlServer.Query.ExpressionTranslators
{
    internal class OffsetDateTimeMethodCallTranslatorPlugin : IMethodCallTranslatorPlugin
    {
        public OffsetDateTimeMethodCallTranslatorPlugin(
            ISqlExpressionFactory sqlExpressionFactory)
        {
            Translators = new IMethodCallTranslator[]
            {
                new OffsetDateTimeMethodTranslator(sqlExpressionFactory)
            };
        }

        public virtual IEnumerable<IMethodCallTranslator> Translators { get; }
    }
}
