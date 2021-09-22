using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Scaffolding;
using Microsoft.EntityFrameworkCore.SqlServer.Storage.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using SimplerSoftware.EntityFrameworkCore.SqlServer.NodaTime.Storage;
using System.Linq;

namespace SimplerSoftware.EntityFrameworkCore.SqlServer.NodaTime.Design
{
    [UsedImplicitly]
    public class SqlServerNodaTimeDesignTimeServices : IDesignTimeServices
    {
        public void ConfigureDesignTimeServices(IServiceCollection serviceCollection)
        {
            serviceCollection
                .AddSingleton<IRelationalTypeMappingSourcePlugin, SqlServerNodaTimeTypeMappingSourcePlugin>()
                .AddSingleton<IProviderCodeGeneratorPlugin, SqlServerNodaTimeCodeGeneratorPlugin>();
        }
    }
}
