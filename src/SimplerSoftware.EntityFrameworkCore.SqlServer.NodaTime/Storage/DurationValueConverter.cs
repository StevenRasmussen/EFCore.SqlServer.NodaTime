using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NodaTime;
using System;
using System.Collections.Generic;
using System.Text;

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
