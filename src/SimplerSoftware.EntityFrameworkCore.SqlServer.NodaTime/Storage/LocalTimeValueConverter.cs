using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NodaTime;
using System;

namespace Microsoft.EntityFrameworkCore.SqlServer.Storage
{
    public class LocalTimeValueConverter : ValueConverter<LocalTime, TimeSpan>
    {
        public LocalTimeValueConverter()
             : base(i => toProvider(i), d => fromProvider(d))
        {
        }

        public static TimeSpan toProvider(LocalTime localTime)
        {
            return new TimeSpan(localTime.TickOfDay);
        }

        public static LocalTime fromProvider(TimeSpan dateTime)
        {
            return LocalTime.FromTicksSinceMidnight(dateTime.Ticks);
        }
    }
}
