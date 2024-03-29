﻿using Microsoft.EntityFrameworkCore.Storage;
using NodaTime;
using SimplerSoftware.EntityFrameworkCore.SqlServer.NodaTime.Storage;
using DateTimeTypeMapping = SimplerSoftware.EntityFrameworkCore.SqlServer.NodaTime.Storage.DateTimeTypeMapping;

namespace Microsoft.EntityFrameworkCore.SqlServer.Storage
{
    public class LocalDateTimeTypeMapping : DateTimeTypeMapping
    {
        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        public static LocalDateTimeTypeMapping Default { get; } = new(SqlServerDateTimeTypes.DateTime2);

        public LocalDateTimeTypeMapping(string storeType)
            : base(storeType, typeof(LocalDateTime), new LocalDateTimeValueConverter())
        {
        }

        protected LocalDateTimeTypeMapping(RelationalTypeMappingParameters parameters)
            : base(parameters)
        {
        }

        protected override RelationalTypeMapping Clone(RelationalTypeMappingParameters parameters)
        {
            return new LocalDateTimeTypeMapping(parameters);
        }
    }
}
