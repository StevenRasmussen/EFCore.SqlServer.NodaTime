using System;
using System.Data;
using System.Data.Common;

using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Storage;

namespace Microsoft.EntityFrameworkCore.SqlServer.Storage
{
    internal class LocalDateTimeTypeMapping : RelationalTypeMapping
    {
        private const string SmallDateTimeFormatConst = "'{0:yyyy-MM-ddTHH:mm:ss}'";
        private const string DateTimeFormatConst = "'{0:yyyy-MM-ddTHH:mm:ss.fff}'";

        private readonly string[] _dateTime2Formats =
        {
            "'{0:yyyy-MM-ddTHH:mm:ss}'",
            "'{0:yyyy-MM-ddTHH:mm:ss.fK}'",
            "'{0:yyyy-MM-ddTHH:mm:ss.ffK}'",
            "'{0:yyyy-MM-ddTHH:mm:ss.fffK}'",
            "'{0:yyyy-MM-ddTHH:mm:ss.ffffK}'",
            "'{0:yyyy-MM-ddTHH:mm:ss.fffffK}'",
            "'{0:yyyy-MM-ddTHH:mm:ss.ffffffK}'",
            "'{0:yyyy-MM-ddTHH:mm:ss.fffffffK}'"
        };

        public LocalDateTimeTypeMapping(string storeType, Type clrType)
            : base(CreateRelationalTypeMappingParameters(storeType, clrType))
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

        private static RelationalTypeMappingParameters CreateRelationalTypeMappingParameters(string storeType, Type clrType)
        {
            return new RelationalTypeMappingParameters(
                new CoreTypeMappingParameters(
                    clrType,
                    new LocalDateTimeValueConverter()
                    ),
                storeType,
                StoreTypePostfix.Precision,
                storeType == "datetime2" ? System.Data.DbType.DateTime2 : System.Data.DbType.DateTime);
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

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        protected override string SqlLiteralFormatString
        {
            get
            {
                switch (StoreType)
                {
                    case "datetime":
                        return DateTimeFormatConst;
                    case "smalldatetime":
                        return SmallDateTimeFormatConst;
                    default:
                        if (Size.HasValue)
                        {
                            var size = Size.Value;
                            if (size <= 7
                                && size >= 0)
                            {
                                return _dateTime2Formats[size];
                            }
                        }

                        return _dateTime2Formats[7];
                }
            }
        }
    }
}
