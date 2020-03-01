using Microsoft.EntityFrameworkCore.Storage;
using NodaTime;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.EntityFrameworkCore.SqlServer.Storage
{
    internal class OffsetDateTimeTypeMappingSourcePlugin : IRelationalTypeMappingSourcePlugin
    {
        public const string SqlServerTypeName = "datetimeoffset";

        public RelationalTypeMapping FindMapping(in RelationalTypeMappingInfo mappingInfo)
        {
            var clrType = mappingInfo.ClrType ?? typeof(OffsetDateTime);
            var storeTypeName = mappingInfo.StoreTypeName;

            return typeof(OffsetDateTime).IsAssignableFrom(clrType) || storeTypeName == SqlServerTypeName
                ? new OffsetDateTimeTypeMapping(SqlServerTypeName, clrType)
                : null;
        }
    }
}
