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

        public static int Year(this Instant instant)
        {
            return instant.ToDateTimeUtc().Year;
        }

        public static int Quarter(this Instant instant)
        {
            throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");
        }

        public static int Month(this Instant instant)
        {
            return instant.ToDateTimeUtc().Month;
        }

        public static int DayOfYear(this Instant instant)
        {
            return instant.ToDateTimeUtc().DayOfYear;
        }

        public static int Day(this Instant instant)
        {
            return instant.ToDateTimeUtc().Day;
        }

        public static int Hour(this Instant instant)
        {
            return instant.ToDateTimeUtc().Hour;
        }

        public static int Minute(this Instant instant)
        {
            return instant.ToDateTimeUtc().Minute;
        }

        public static int Second(this Instant instant)
        {
            return instant.ToDateTimeUtc().Second;
        }

        public static int Millisecond(this Instant instant)
        {
            return instant.ToDateTimeUtc().Millisecond;
        }

        public static int Microsecond(this Instant instant)
        {
            throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");
        }

        public static int Nanosecond(this Instant instant)
        {
            throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");
        }

        public static int TzOffset(this Instant instant)
        {
            throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");
        }

        public static int Week(this Instant instant)
        {
            throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");
        }

        public static int WeekDay(this Instant instant)
        {
            throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");
        }

        public static int IsoWeek(this Instant instant)
        {
            throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");
        }
    }
}
