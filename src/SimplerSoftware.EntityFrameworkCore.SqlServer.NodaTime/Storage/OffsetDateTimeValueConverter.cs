using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NodaTime;
using System;

namespace Microsoft.EntityFrameworkCore.SqlServer.Storage
{
    public class OffsetDateTimeValueConverter : ValueConverter<OffsetDateTime, DateTimeOffset>
    {
        public OffsetDateTimeValueConverter()
             : base(i => toProvider(i), d => fromProvider(d))
        {
        }

        public static DateTimeOffset toProvider(OffsetDateTime offsetDateTime)
        {
            return offsetDateTime.ToDateTimeOffset();
        }

        public static OffsetDateTime fromProvider(DateTimeOffset dateTimeOffset)
        {
            return OffsetDateTime.FromDateTimeOffset(dateTimeOffset);
        }
    }
}
