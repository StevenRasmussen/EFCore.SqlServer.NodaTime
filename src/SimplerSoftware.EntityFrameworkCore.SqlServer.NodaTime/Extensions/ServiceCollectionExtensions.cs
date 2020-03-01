using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.SqlServer.Query.ExpressionTranslators;
using Microsoft.EntityFrameworkCore.SqlServer.Storage;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Text;

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
                        // Instant
                        x.TryAddSingletonEnumerable<IMethodCallTranslatorPlugin, InstantMethodCallTranslatorPlugin>();
                        x.TryAddSingletonEnumerable<IRelationalTypeMappingSourcePlugin, InstantTypeMappingSourcePlugin>();

                        // LocalDate
                        x.TryAddSingletonEnumerable<IMethodCallTranslatorPlugin, LocalDateMethodCallTranslatorPlugin>();
                        x.TryAddSingletonEnumerable<IRelationalTypeMappingSourcePlugin, LocalDateTypeMappingSourcePlugin>();

                        // LocalTime
                        x.TryAddSingletonEnumerable<IMethodCallTranslatorPlugin, LocalTimeMethodCallTranslatorPlugin>();
                        x.TryAddSingletonEnumerable<IRelationalTypeMappingSourcePlugin, LocalTimeTypeMappingSourcePlugin>();

                        // Duration
                        x.TryAddSingletonEnumerable<IMethodCallTranslatorPlugin, DurationMethodCallTranslatorPlugin>();
                        x.TryAddSingletonEnumerable<IRelationalTypeMappingSourcePlugin, DurationTypeMappingSourcePlugin>();

                        // LocalDateTime
                        x.TryAddSingletonEnumerable<IMethodCallTranslatorPlugin, LocalDateTimeMethodCallTranslatorPlugin>();
                        x.TryAddSingletonEnumerable<IRelationalTypeMappingSourcePlugin, LocalDateTimeTypeMappingSourcePlugin>();

                        // OffsetDateTime
                        x.TryAddSingletonEnumerable<IMethodCallTranslatorPlugin, OffsetDateTimeMethodCallTranslatorPlugin>();
                        x.TryAddSingletonEnumerable<IRelationalTypeMappingSourcePlugin, OffsetDateTimeTypeMappingSourcePlugin>();
                    });

            return serviceCollection;
        }
    }
}
