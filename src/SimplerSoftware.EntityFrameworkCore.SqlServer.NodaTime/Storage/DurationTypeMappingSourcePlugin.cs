using Microsoft.EntityFrameworkCore.Storage;
using NodaTime;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.EntityFrameworkCore.SqlServer.Storage
{
    internal class DurationTypeMappingSourcePlugin : IRelationalTypeMappingSourcePlugin
    {
        public const string SqlServerTypeName = "time";

        public RelationalTypeMapping FindMapping(in RelationalTypeMappingInfo mappingInfo)
        {
            var clrType = mappingInfo.ClrType ?? typeof(Duration);
            var storeTypeName = mappingInfo.StoreTypeName;

            return typeof(Duration).IsAssignableFrom(clrType) || storeTypeName == SqlServerTypeName
                ? new DurationTypeMapping(clrType)
                : null;
        }
    }
}
