using Microsoft.EntityFrameworkCore.Storage;
using NodaTime;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.EntityFrameworkCore.SqlServer.Storage
{
    internal class LocalTimeTypeMappingSourcePlugin : IRelationalTypeMappingSourcePlugin
    {
        public const string SqlServerTypeName = "time";

        public RelationalTypeMapping FindMapping(in RelationalTypeMappingInfo mappingInfo)
        {
            var clrType = mappingInfo.ClrType ?? typeof(LocalTime);
            var storeTypeName = mappingInfo.StoreTypeName;

            return typeof(LocalTime).IsAssignableFrom(clrType) || storeTypeName == SqlServerTypeName
                ? new LocalTimeTypeMapping(clrType)
                : null;
        }
    }
}
