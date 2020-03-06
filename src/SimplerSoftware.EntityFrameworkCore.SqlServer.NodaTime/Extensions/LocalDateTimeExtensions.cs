using NodaTime;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.EntityFrameworkCore.SqlServer.NodaTime.Extensions
{
    public static class LocalDateTimeExtensions
    {
        public static LocalDateTime PlusQuarters(this LocalDateTime localDateTime, int quarters)
        {
            throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");
        }

        public static int Quarter(this LocalDateTime localDateTime)
        {
            throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");
        }

        public static int Week(this LocalDateTime localDateTime)
        {
            throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");
        }

        public static LocalDateTime PlusMicroseconds(this LocalDateTime localDateTime, long microseconds)
        {
            throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");
        }

        public static int Microsecond(this LocalDateTime localDateTime)
        {
            throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");
        }

        public static LocalDateTime FromParts(int year, int month, int day, int hour, int minute, int second, int millisecond, int microsecond, int nanosecond)
        {
            return new LocalDateTime(year, month, day, hour, minute)
                .PlusMilliseconds(millisecond)
                .PlusNanoseconds(microsecond * 1000)
                .PlusNanoseconds(nanosecond);
        }
    }
}
