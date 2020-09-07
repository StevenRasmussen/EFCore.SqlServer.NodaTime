using Microsoft.EntityFrameworkCore.Storage;
using NodaTime;

namespace Microsoft.EntityFrameworkCore.SqlServer.Storage
{
    internal class InstantTypeMappingSourcePlugin : IRelationalTypeMappingSourcePlugin
    {
        public RelationalTypeMapping FindMapping(in RelationalTypeMappingInfo mappingInfo)
        {
            var clrType = mappingInfo.ClrType ?? typeof(Instant);
            var storeTypeName = mappingInfo.StoreTypeName;

            return typeof(Instant).IsAssignableFrom(clrType)
                ? new InstantTypeMapping(storeTypeName ?? "datetime2", clrType)
                : null;
        }
    }
}
