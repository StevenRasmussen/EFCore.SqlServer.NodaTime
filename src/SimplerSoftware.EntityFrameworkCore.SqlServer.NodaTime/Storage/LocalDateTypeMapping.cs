﻿using System.Data;
using System.Data.Common;

using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Storage;
using NodaTime;
using SimplerSoftware.EntityFrameworkCore.SqlServer.NodaTime.Storage;

namespace Microsoft.EntityFrameworkCore.SqlServer.Storage
{
    public class LocalDateTypeMapping : RelationalTypeMapping
    {
        private const string DateFormatConst = "'{0:yyyy-MM-dd}'";

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        public static LocalDateTypeMapping Default { get; } = new();

        public LocalDateTypeMapping()
            : base(CreateRelationalTypeMappingParameters())
        {
        }

        protected LocalDateTypeMapping(RelationalTypeMappingParameters parameters)
            : base(parameters)
        {
        }

        protected override RelationalTypeMapping Clone(RelationalTypeMappingParameters parameters)
        {
            return new LocalDateTypeMapping(parameters);
        }

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        protected override string SqlLiteralFormatString => DateFormatConst;

        private static RelationalTypeMappingParameters CreateRelationalTypeMappingParameters()
        {
            return new RelationalTypeMappingParameters(
                new CoreTypeMappingParameters(
                    typeof(LocalDate),
                    new LocalDateValueConverter()),
                SqlServerDateTimeTypes.Date,
                StoreTypePostfix.None,
                System.Data.DbType.Date);
        }
        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        protected override void ConfigureParameter(DbParameter parameter)
        {
            base.ConfigureParameter(parameter);

            // Workaround for a SQLClient bug
            if (DbType == System.Data.DbType.Date)
            {
                ((SqlParameter)parameter).SqlDbType = SqlDbType.Date;
            }

            if (Size.HasValue
                && Size.Value != -1)
            {
                parameter.Size = Size.Value;
            }
        }
    }
}
