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
                .TryAddProviderSpecificServices(
                    x =>
                    {
                        // All type mappings
                        x.TryAddSingletonEnumerable<IRelationalTypeMappingSourcePlugin, SqlServerNodaTimeTypeMappingSourcePlugin>();

                        // Instant
                        x.TryAddSingletonEnumerable<IMethodCallTranslatorPlugin, InstantMethodCallTranslatorPlugin>();

                        // LocalDate
                        x.TryAddSingletonEnumerable<IMethodCallTranslatorPlugin, LocalDateMethodCallTranslatorPlugin>();

                        // LocalTime
                        x.TryAddSingletonEnumerable<IMethodCallTranslatorPlugin, LocalTimeMethodCallTranslatorPlugin>();
                        
                        // Duration
                        x.TryAddSingletonEnumerable<IMethodCallTranslatorPlugin, DurationMethodCallTranslatorPlugin>();

                        // LocalDateTime
                        x.TryAddSingletonEnumerable<IMethodCallTranslatorPlugin, LocalDateTimeMethodCallTranslatorPlugin>();

                        // OffsetDateTime
                        x.TryAddSingletonEnumerable<IMethodCallTranslatorPlugin, OffsetDateTimeMethodCallTranslatorPlugin>();

                        // All MemberTranslators
                        x.TryAddSingletonEnumerable<IMemberTranslatorProvider, NodaTimeTypesMemberTranslatorPlugin>();
                    });

            return serviceCollection;
        }
    }
}
