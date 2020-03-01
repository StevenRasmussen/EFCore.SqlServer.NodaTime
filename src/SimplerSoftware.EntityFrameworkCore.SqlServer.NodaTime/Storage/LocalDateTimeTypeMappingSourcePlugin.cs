using Microsoft.EntityFrameworkCore.Storage;
using NodaTime;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.EntityFrameworkCore.SqlServer.Storage
{
    internal class LocalDateTimeTypeMappingSourcePlugin : IRelationalTypeMappingSourcePlugin
    {
        public const string SqlServerTypeName = "datetime2";

        public RelationalTypeMapping FindMapping(in RelationalTypeMappingInfo mappingInfo)
        {
            var clrType = mappingInfo.ClrType ?? typeof(LocalDateTime);
            var storeTypeName = mappingInfo.StoreTypeName;

            return typeof(LocalDateTime).IsAssignableFrom(clrType) || storeTypeName == SqlServerTypeName
                ? new LocalDateTimeTypeMapping(SqlServerTypeName, clrType)
                : null;
        }
    }
}
