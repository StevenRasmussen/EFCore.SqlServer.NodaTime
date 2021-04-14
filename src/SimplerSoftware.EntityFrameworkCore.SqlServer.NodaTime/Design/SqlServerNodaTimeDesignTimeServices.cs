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
            // We are replacing the default implementation of the 'SqlServerTypeMappingSource' due to a bug. See here: https://github.com/dotnet/efcore/issues/24660
            var insertIndex = serviceCollection.Count - 1;
#pragma warning disable EF1001 // Internal EF Core API usage.
            var sqlServerTypeMappingSource = serviceCollection.FirstOrDefault(x => x.ServiceType == typeof(IRelationalTypeMappingSource) && x.ImplementationType == typeof(SqlServerTypeMappingSource));
#pragma warning restore EF1001 // Internal EF Core API usage.
            if (sqlServerTypeMappingSource != null)
            {
                insertIndex = serviceCollection.IndexOf(sqlServerTypeMappingSource);
                serviceCollection.Remove(sqlServerTypeMappingSource);
            }

            serviceCollection.Insert(insertIndex, new ServiceDescriptor(typeof(IRelationalTypeMappingSource), typeof(SqlServerNodaTimeTypeMappingSource), ServiceLifetime.Singleton));

            serviceCollection
                .AddSingleton<IRelationalTypeMappingSourcePlugin, SqlServerNodaTimeTypeMappingSourcePlugin>()
                .AddSingleton<IProviderCodeGeneratorPlugin, SqlServerNodaTimeCodeGeneratorPlugin>();
        }
    }
}
