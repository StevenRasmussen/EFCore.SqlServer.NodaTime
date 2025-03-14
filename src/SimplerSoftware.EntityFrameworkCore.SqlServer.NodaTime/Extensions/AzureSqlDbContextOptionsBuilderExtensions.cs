using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.SqlServer.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.EntityFrameworkCore;

public static class AzureSqlDbContextOptionsBuilderExtensions
{
    public static AzureSqlDbContextOptionsBuilder UseNodaTime(
        this AzureSqlDbContextOptionsBuilder azureBuilder)
    {
        // Access the underlying OptionsBuilder
        var coreOptionsBuilder = ((IRelationalDbContextOptionsBuilderInfrastructure)azureBuilder).OptionsBuilder;

        // Look for the NodaTime extension or create a new instance if not found
        var extension = coreOptionsBuilder.Options.FindExtension<NodaTimeOptionsExtension>()
            ?? new NodaTimeOptionsExtension();

        // Add or update the extension in the options
        ((IDbContextOptionsBuilderInfrastructure)coreOptionsBuilder).AddOrUpdateExtension(extension);

        return azureBuilder;
    }
}