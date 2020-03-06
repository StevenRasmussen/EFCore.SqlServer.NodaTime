using NodaTime;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.EntityFrameworkCore.SqlServer.NodaTime.Extensions
{
    public static class LocalTimeExtensions
    {
        public static LocalTime PlusMicroseconds(this LocalTime localTime, long microseconds)
        {
            return localTime.PlusNanoseconds(microseconds * 1000);
        }

        public static int Microsecond(this LocalTime localTime)
        {
            throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");
        }

        public static LocalTime FromParts(int hour, int minute, int second, int millisecond, int microsecond, int nanosecond)
        {
            return new LocalTime(hour, minute, second, millisecond)
                .PlusMicroseconds(microsecond)
                .PlusNanoseconds(nanosecond);
        }
    }
}
