using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Scaffolding;
using Microsoft.EntityFrameworkCore.SqlServer.Storage;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;

namespace SimplerSoftware.EntityFrameworkCore.SqlServer.NodaTime.Design
{
    [UsedImplicitly]
    public class SqlServerNodaTimeDesignTimeServices : IDesignTimeServices
    {
        public void ConfigureDesignTimeServices(IServiceCollection serviceCollection)
            => serviceCollection
                .AddSingleton<IRelationalTypeMappingSourcePlugin, InstantTypeMappingSourcePlugin>()
                .AddSingleton<IRelationalTypeMappingSourcePlugin, LocalDateTypeMappingSourcePlugin>()
                .AddSingleton<IRelationalTypeMappingSourcePlugin, LocalTimeTypeMappingSourcePlugin>()
                .AddSingleton<IRelationalTypeMappingSourcePlugin, DurationTypeMappingSourcePlugin>()
                .AddSingleton<IRelationalTypeMappingSourcePlugin, LocalDateTimeTypeMappingSourcePlugin>()
                .AddSingleton<IRelationalTypeMappingSourcePlugin, OffsetDateTimeTypeMappingSourcePlugin>()
                .AddSingleton<IProviderCodeGeneratorPlugin, SqlServerNodaTimeCodeGeneratorPlugin>();
    }
}
