using NodaTime;
using System;

namespace Microsoft.EntityFrameworkCore.SqlServer.NodaTime.Extensions
{
    public static class InstantExtensions
    {
        public static Instant PlusYears(this Instant instant, int years)
        {
            throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");
        }

        public static Instant PlusMonths(this Instant instant, int months)
        {
            throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");
        }

        public static Instant PlusDays(this Instant instant, double days)
        {
            return instant.Plus(Duration.FromDays(days));
        }

        public static Instant PlusHours(this Instant instant, double hours)
        {
            return instant.Plus(Duration.FromHours(hours));
        }

        public static Instant PlusMinutes(this Instant instant, double minutes)
        {
            return instant.Plus(Duration.FromMinutes(minutes));
        }

        public static Instant PlusSeconds(this Instant instant, double seconds)
        {
            return instant.Plus(Duration.FromSeconds(seconds));
        }

        public static Instant PlusMilliseconds(this Instant instant, double milliseconds)
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

        public static Instant FromParts(int year, int month, int day, int hour, int minute, int second, int millisecond, int microsecond, int nanosecond)
        {
            return Instant.FromUtc(year, month, day, hour, minute)
                .PlusMilliseconds(millisecond)
                .PlusNanoseconds(microsecond * 1000)
                .PlusNanoseconds(nanosecond);
        }
    }
}
