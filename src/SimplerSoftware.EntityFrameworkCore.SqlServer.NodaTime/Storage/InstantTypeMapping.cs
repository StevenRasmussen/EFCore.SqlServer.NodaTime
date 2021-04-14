using Microsoft.EntityFrameworkCore.Storage;
using NodaTime;
using DateTimeTypeMapping = SimplerSoftware.EntityFrameworkCore.SqlServer.NodaTime.Storage.DateTimeTypeMapping;

namespace Microsoft.EntityFrameworkCore.SqlServer.Storage
{
    internal class InstantTypeMapping : DateTimeTypeMapping
    {
        public InstantTypeMapping(string storeType)
            : base(storeType, typeof(Instant), new InstantValueConverter())
        {
        }

        protected InstantTypeMapping(RelationalTypeMappingParameters parameters)
            : base(parameters)
        {
        }

        protected override RelationalTypeMapping Clone(RelationalTypeMappingParameters parameters)
        {
            return new InstantTypeMapping(parameters);
        }
    }
}
