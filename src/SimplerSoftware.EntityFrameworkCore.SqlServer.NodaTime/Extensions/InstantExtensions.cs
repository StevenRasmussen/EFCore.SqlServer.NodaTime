using NodaTime;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.EntityFrameworkCore.SqlServer.NodaTime.Extensions
{
    public static class InstantExtensions
    {
        public static Instant AddYears(this Instant instant, int years)
        {
            // No native support from NodaTime so we need to convert to a DateTime and then back to an Instant
            return Instant.FromDateTimeUtc(DateTime.SpecifyKind(instant.ToDateTimeUtc().AddYears(years), DateTimeKind.Utc));
        }

        public static Instant AddMonths(this Instant instant, int months)
        {
            // No native support from NodaTime so we need to convert to a DateTime and then back to an Instant
            return Instant.FromDateTimeUtc(DateTime.SpecifyKind(instant.ToDateTimeUtc().AddMonths(months), DateTimeKind.Utc));
        }

        public static Instant AddDays(this Instant instant, double days)
        {
            return instant.Plus(Duration.FromDays(days));
        }

        public static Instant AddHours(this Instant instant, double hours)
        {
            return instant.Plus(Duration.FromHours(hours));
        }

        public static Instant AddMinutes(this Instant instant, double minutes)
        {
            return instant.Plus(Duration.FromMinutes(minutes));
        }

        public static Instant AddSeconds(this Instant instant, double seconds)
        {
            return instant.Plus(Duration.FromSeconds(seconds));
        }

        public static Instant AddMilliseconds(this Instant instant, double milliseconds)
        {
            return instant.Plus(Duration.FromMilliseconds(milliseconds));
        }
    }
}
