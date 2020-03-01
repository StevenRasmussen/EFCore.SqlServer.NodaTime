using Microsoft.EntityFrameworkCore.Storage;
using NodaTime;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.EntityFrameworkCore.SqlServer.Storage
{
    internal class InstantTypeMappingSourcePlugin : IRelationalTypeMappingSourcePlugin
    {
        public const string SqlServerTypeName = "datetime2";

        public RelationalTypeMapping FindMapping(in RelationalTypeMappingInfo mappingInfo)
        {
            var clrType = mappingInfo.ClrType ?? typeof(Instant);
            var storeTypeName = mappingInfo.StoreTypeName;

            return typeof(Instant).IsAssignableFrom(clrType) || storeTypeName == SqlServerTypeName
                ? new InstantTypeMapping(SqlServerTypeName, clrType)
                : null;
        }
    }
}
