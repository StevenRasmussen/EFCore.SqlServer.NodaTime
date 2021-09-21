using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.SqlServer.Query.ExpressionTranslators;
using Microsoft.EntityFrameworkCore.Storage;
using SimplerSoftware.EntityFrameworkCore.SqlServer.NodaTime.Storage;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddNodaTime(this IServiceCollection serviceCollection)
        {
            new EntityFrameworkRelationalServicesBuilder(serviceCollection)
                .TryAdd<IRelationalTypeMappingSourcePlugin, SqlServerNodaTimeTypeMappingSourcePlugin>()
                .TryAdd<IMethodCallTranslatorPlugin, InstantMethodCallTranslatorPlugin>()
                .TryAdd<IMethodCallTranslatorPlugin, LocalDateMethodCallTranslatorPlugin>()
                .TryAdd<IMethodCallTranslatorPlugin, LocalTimeMethodCallTranslatorPlugin>()
                .TryAdd<IMethodCallTranslatorPlugin, DurationMethodCallTranslatorPlugin>()
                .TryAdd<IMethodCallTranslatorPlugin, LocalDateTimeMethodCallTranslatorPlugin>()
                .TryAdd<IMethodCallTranslatorPlugin, OffsetDateTimeMethodCallTranslatorPlugin>()
                .TryAddProviderSpecificServices(x => x.TryAddScopedEnumerable<IMemberTranslatorProvider, NodaTimeTypesMemberTranslatorPlugin>());

            return serviceCollection;
        }
    }
}
