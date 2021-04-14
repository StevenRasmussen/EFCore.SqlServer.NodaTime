using Microsoft.EntityFrameworkCore.Storage;
using NodaTime;
using DateTimeTypeMapping = SimplerSoftware.EntityFrameworkCore.SqlServer.NodaTime.Storage.DateTimeTypeMapping;

namespace Microsoft.EntityFrameworkCore.SqlServer.Storage
{
    internal class LocalDateTimeTypeMapping : DateTimeTypeMapping
    {
        public LocalDateTimeTypeMapping(string storeType)
            : base(storeType, typeof(LocalDateTime), new LocalDateTimeValueConverter())
        {
        }

        protected LocalDateTimeTypeMapping(RelationalTypeMappingParameters parameters)
            : base(parameters)
        {
        }

        protected override RelationalTypeMapping Clone(RelationalTypeMappingParameters parameters)
        {
            return new LocalDateTimeTypeMapping(parameters);
        }
    }
}
