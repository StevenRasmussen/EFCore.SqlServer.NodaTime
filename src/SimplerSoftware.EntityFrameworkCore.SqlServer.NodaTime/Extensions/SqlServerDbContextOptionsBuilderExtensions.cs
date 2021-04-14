using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.SqlServer.Infrastructure;
using System;

namespace Microsoft.EntityFrameworkCore
{
    public static class SqlServerDbContextOptionsBuilderExtensions
    {
        /// <summary>
        /// Enable NodaTime mappings.
        /// </summary>
        /// <param name="optionsBuilder">The build being used to configure SQL Server.</param>
        /// <returns>The options builder so that further configuration can be chained.</returns>
        public static SqlServerDbContextOptionsBuilder UseNodaTime(
            this SqlServerDbContextOptionsBuilder optionsBuilder)
        {
            var coreOptionsBuilder = ((IRelationalDbContextOptionsBuilderInfrastructure)optionsBuilder).OptionsBuilder;

            var extension = coreOptionsBuilder.Options.FindExtension<NodaTimeOptionsExtension>()
                ?? new NodaTimeOptionsExtension();

            ((IDbContextOptionsBuilderInfrastructure)coreOptionsBuilder).AddOrUpdateExtension(extension);

            return optionsBuilder;
        }
    }
}

namespace Microsoft.EntityFrameworkCore.SqlServer.NodaTime.Extensions
{
    public static class SqlServerDbContextOptionsBuilderExtensions
    {
        [Obsolete("Please use the 'UseNodaTime' extension method found in the 'Microsoft.EntityFrameworkCore.SqlServer' namespace.")]
        public static SqlServerDbContextOptionsBuilder UseNodaTime(
            this SqlServerDbContextOptionsBuilder optionsBuilder)
        {
            return EntityFrameworkCore.SqlServerDbContextOptionsBuilderExtensions.UseNodaTime(optionsBuilder);
        }
    }
}
