﻿using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NodaTime;
using System;

namespace Microsoft.EntityFrameworkCore.SqlServer.Storage
{
    public class LocalDateTimeValueConverter : ValueConverter<LocalDateTime, DateTime>
    {
        public LocalDateTimeValueConverter()
             : base(i => toProvider(i), d => fromProvider(d))
        {
        }

        public static DateTime toProvider(LocalDateTime localDateTime)
        {
            return localDateTime.ToDateTimeUnspecified();
        }

        public static LocalDateTime fromProvider(DateTime dateTime)
        {
            return LocalDateTime.FromDateTime(dateTime);
        }
    }
}
