using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NodaTime;
using System;

namespace Microsoft.EntityFrameworkCore.SqlServer.Storage
{
    public class LocalDateValueConverter : ValueConverter<LocalDate, DateTime>
    {
        public LocalDateValueConverter()
             : base(i => toProvider(i), d => fromProvider(d))
        {
        }

        private static DateTime toProvider(LocalDate localDate)
        {
            return localDate.ToDateTimeUnspecified();
        }

        private static LocalDate fromProvider(DateTime dateTime)
        {
            return LocalDate.FromDateTime(dateTime);
        }
    }
}
