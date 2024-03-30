using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NodaTime;
using System;

namespace Microsoft.EntityFrameworkCore.SqlServer.Storage
{
    public class DurationValueConverter : ValueConverter<Duration, TimeSpan>
    {
        public DurationValueConverter()
            : base(i => toProvider(i), d => fromProvider(d))
        {
        }

        public static TimeSpan toProvider(Duration duration)
        {
            return duration.ToTimeSpan();
        }

        public static Duration fromProvider(TimeSpan timeSpan)
        {
            return Duration.FromTimeSpan(timeSpan);
        }
    }
}
