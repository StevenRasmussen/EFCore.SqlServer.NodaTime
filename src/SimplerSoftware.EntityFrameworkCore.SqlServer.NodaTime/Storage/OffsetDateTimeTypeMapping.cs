using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace Microsoft.EntityFrameworkCore.SqlServer.Storage
{
    internal class OffsetDateTimeTypeMapping : RelationalTypeMapping
    {
        private const string DateTimeOffsetFormatConst = "{0:yyyy-MM-ddTHH:mm:ss.fffffffzzz}";

        public OffsetDateTimeTypeMapping(string storeType, Type clrType)
            : base(CreateRelationalTypeMappingParameters(storeType, clrType))
        {
        }

        protected OffsetDateTimeTypeMapping(RelationalTypeMappingParameters parameters)
            : base(parameters)
        {
        }

        protected override RelationalTypeMapping Clone(RelationalTypeMappingParameters parameters)
        {
            return new OffsetDateTimeTypeMapping(parameters);
        }

        private static RelationalTypeMappingParameters CreateRelationalTypeMappingParameters(string storeType, Type clrType)
        {
            return new RelationalTypeMappingParameters(
                new CoreTypeMappingParameters(
                    clrType,
                    new OffsetDateTimeValueConverter()
                    ),
                storeType);
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
        protected override string SqlLiteralFormatString => "'" + DateTimeOffsetFormatConst + "'";
    }
}
