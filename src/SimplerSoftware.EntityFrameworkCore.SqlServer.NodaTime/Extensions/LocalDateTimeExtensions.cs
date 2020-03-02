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
    }
}
