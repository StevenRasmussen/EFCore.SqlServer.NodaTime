using Microsoft.EntityFrameworkCore.Storage;
using NodaTime;

namespace Microsoft.EntityFrameworkCore.SqlServer.Storage
{
    internal class LocalDateTypeMappingSourcePlugin : IRelationalTypeMappingSourcePlugin
    {
        public const string SqlServerTypeName = "date";

        public RelationalTypeMapping FindMapping(in RelationalTypeMappingInfo mappingInfo)
        {
            var clrType = mappingInfo.ClrType ?? typeof(LocalDate);
            var storeTypeName = mappingInfo.StoreTypeName;

            return typeof(LocalDate).IsAssignableFrom(clrType) || storeTypeName == SqlServerTypeName
                ? new LocalDateTypeMapping(clrType)
                : null;
        }
    }
}
