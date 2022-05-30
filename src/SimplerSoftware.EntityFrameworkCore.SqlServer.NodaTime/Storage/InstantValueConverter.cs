using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NodaTime;
using System;

namespace Microsoft.EntityFrameworkCore.SqlServer.Storage
{
    public class InstantValueConverter : ValueConverter<Instant, DateTime>
    {
        public InstantValueConverter()
            : base(i => toProvider(i), d => fromProvider(d))
        {
        }

        private static DateTime toProvider(Instant instant)
        {
            return instant.ToDateTimeUtc();
        }

        private static Instant fromProvider(DateTime dateTime)
        {
            return Instant.FromDateTimeUtc(DateTime.SpecifyKind(dateTime, DateTimeKind.Utc));
        }
    }
}
