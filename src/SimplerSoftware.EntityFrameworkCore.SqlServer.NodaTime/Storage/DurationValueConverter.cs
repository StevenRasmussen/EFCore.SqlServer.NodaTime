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

        private static TimeSpan toProvider(Duration duration)
        {
            return duration.ToTimeSpan();
        }

        private static Duration fromProvider(TimeSpan timeSpan)
        {
            return Duration.FromTimeSpan(timeSpan);
        }
    }
}
