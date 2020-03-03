using NodaTime;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.EntityFrameworkCore.SqlServer.NodaTime.Extensions
{
    public static class DurationExtensions
    {
        public static Duration PlusHours(this Duration duration, double hours)
        {
            return duration.Plus(Duration.FromHours(hours));
        }

        public static Duration PlusMinutes(this Duration duration, double minutes)
        {
            return duration.Plus(Duration.FromMinutes(minutes));
        }

        public static Duration PlusSeconds(this Duration duration, double seconds)
        {
            return duration.Plus(Duration.FromSeconds(seconds));
        }

        public static Duration PlusMilliseconds(this Duration duration, double milliseconds)
        {
            return duration.Plus(Duration.FromMilliseconds(milliseconds));
        }

        public static Duration PlusMicroseconds(this Duration duration, double microseconds)
        {
            return duration.Plus(Duration.FromNanoseconds(microseconds * 1000));
        }

        public static Duration PlusNanoseconds(this Duration duration, double nanoseconds)
        {
            return duration.Plus(Duration.FromNanoseconds(nanoseconds));
        }

        public static int Microseconds(this Duration duration)
        {
            throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");
        }

        public static Duration FromParts(int hours, int minutes, int seconds, int milliseconds = 0, int microseconds = 0, int nanoseconds = 0)
        {
            return Duration.FromHours(hours)
                .PlusMinutes(minutes)
                .PlusSeconds(seconds)
                .PlusMilliseconds(milliseconds)
                .PlusMicroseconds(microseconds)
                .PlusNanoseconds(nanoseconds);
        }
    }
}
