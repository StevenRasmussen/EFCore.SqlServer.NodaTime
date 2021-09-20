using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.SqlServer.NodaTime.Extensions;
using Microsoft.EntityFrameworkCore.SqlServer.Query.ExpressionTranslators;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using SimplerSoftware.EntityFrameworkCore.SqlServer.NodaTime.Properties;
using SimplerSoftware.EntityFrameworkCore.SqlServer.NodaTime.Storage;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.EntityFrameworkCore.SqlServer.Infrastructure
{
    internal class NodaTimeOptionsExtension : IDbContextOptionsExtension
    {
        public DbContextOptionsExtensionInfo _info;

        public void ApplyServices(IServiceCollection services)
        {
            services.AddNodaTime();
        }

        public DbContextOptionsExtensionInfo Info => _info ??= new ExtensionInfo(this);

        public void Validate(IDbContextOptions options)
        {
            var internalServiceProvider = options.FindExtension<CoreOptionsExtension>()?.InternalServiceProvider;
            if (internalServiceProvider != null)
            {
                using (var scope = internalServiceProvider.CreateScope())
                {
                    // Instant
                    if (scope.ServiceProvider.GetService<IEnumerable<IMethodCallTranslatorPlugin>>()
                            ?.Any(s => s is InstantMethodCallTranslatorPlugin) != true ||
                        scope.ServiceProvider.GetService<IEnumerable<IRelationalTypeMappingSourcePlugin>>()
                           ?.Any(s => s is SqlServerNodaTimeTypeMappingSourcePlugin) != true)
                    {
                        throw new InvalidOperationException(Resources.ServicesMissing);
                    }
                }
            }
        }

        private sealed class ExtensionInfo : DbContextOptionsExtensionInfo
        {
            public ExtensionInfo(IDbContextOptionsExtension extension)
                : base(extension)
            {
            }

            private new NodaTimeOptionsExtension Extension => (NodaTimeOptionsExtension)base.Extension;

            public override bool IsDatabaseProvider => false;

            public override int GetServiceProviderHashCode() => 0;

            public override void PopulateDebugInfo(IDictionary<string, string> debugInfo) => debugInfo["SqlServer:" + nameof(SqlServerDbContextOptionsBuilderExtensions.UseNodaTime)] = "1";

            public override bool ShouldUseSameServiceProvider(DbContextOptionsExtensionInfo other) => true;

            public override string LogFragment => "using NodaTime ";
        }
    }
}
