﻿using NodaTime;
using System;

namespace Microsoft.EntityFrameworkCore.SqlServer.NodaTime.Extensions
{
    public static class OffsetDateTimeExtensions
    {
        public static OffsetDateTime PlusYears(this OffsetDateTime offsetDateTime, int years)
        {
            throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");
        }

        public static OffsetDateTime PlusMonths(this OffsetDateTime offsetDateTime, int months)
        {
            throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");
        }

        public static OffsetDateTime PlusQuarters(this OffsetDateTime offsetDateTime, int months)
        {
            throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");
        }

        public static OffsetDateTime PlusDays(this OffsetDateTime offsetDateTime, double days)
        {
            return offsetDateTime.Plus(Duration.FromDays(days));
        }

        public static OffsetDateTime PlusWeeks(this OffsetDateTime offsetDateTime, double weeks)
        {
            throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");
        }

        public static OffsetDateTime PlusMicroseconds(this OffsetDateTime offsetDateTime, double microseconds)
        {
            var result =  offsetDateTime.Plus(Duration.FromNanoseconds(microseconds * 1000));
            return result;
        }

        public static int Quarter(this OffsetDateTime offsetDateTime)
        {
            throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");
        }

        public static int Microsecond(this OffsetDateTime offsetDateTime)
        {
            throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");
        }

        public static int TzOffset(this OffsetDateTime offsetDateTime)
        {
            throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");
        }

        public static int Week(this OffsetDateTime offsetDateTime)
        {
            throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");
        }

        public static int WeekDay(this OffsetDateTime offsetDateTime)
        {
            throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");
        }

        public static int IsoWeek(this OffsetDateTime offsetDateTime)
        {
            throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");
        }

        public static OffsetDateTime FromParts(int year, int month, int day, int hour, int minute, int second, int millisecond, int microsecond, int nanosecond, int offsetInMinutes)
        {
            var result = OffsetDateTime.FromDateTimeOffset(new DateTimeOffset(new DateTime(year, month, day, hour, minute, second, millisecond), TimeSpan.FromMinutes(offsetInMinutes)))
                .PlusMicroseconds(microsecond)
                .PlusNanoseconds(nanosecond);

            return result;
        }
    }
}
