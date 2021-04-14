using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.SqlServer.Storage.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using SimplerSoftware.EntityFrameworkCore.SqlServer.NodaTime.Storage;
using System;
using System.Linq;

namespace SimplerSoftware.EntityFrameworkCore.SqlServer.NodaTime.Design
{
    // This class is only used due to a bug in the 'SqlServerTypeMappingSource'. See here for more info: https://github.com/dotnet/efcore/issues/24660
#pragma warning disable EF1001 // Internal EF Core API usage.
    internal sealed class SqlServerNodaTimeTypeMappingSource : SqlServerTypeMappingSource
    {
        private readonly SqlServerNodaTimeTypeMappingSourcePlugin _sqlServerNodaTimeTypeMappingSourcePlugin;
        public SqlServerNodaTimeTypeMappingSource(
            [NotNull] TypeMappingSourceDependencies dependencies,
            [NotNull] RelationalTypeMappingSourceDependencies relationalDependencies)
            : base(dependencies, relationalDependencies)
        {
            this._sqlServerNodaTimeTypeMappingSourcePlugin = relationalDependencies.Plugins.OfType<SqlServerNodaTimeTypeMappingSourcePlugin>().FirstOrDefault();

            if (this._sqlServerNodaTimeTypeMappingSourcePlugin == null)
                throw new NullReferenceException($"The '{nameof(SqlServerNodaTimeTypeMappingSourcePlugin)}' was not found. Please ensure that it is registered in the DI container.");
        }

        protected override RelationalTypeMapping FindMapping(in RelationalTypeMappingInfo mappingInfo)
        {
            return this._sqlServerNodaTimeTypeMappingSourcePlugin.FindMapping(mappingInfo) ?? base.FindMapping(mappingInfo);
        }
    }
}
