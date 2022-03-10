using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SimplerSoftware.EntityFrameworkCore.SqlServer.NodaTime.Tests.Models;
using Xunit;

namespace SimplerSoftware.EntityFrameworkCore.SqlServer.NodaTime.Tests;

public class ServiceCollectionTests
{
    [Fact]
    public void CustomServiceCollection_ShouldNotThrowError()
    {
        var serviceProvider = new ServiceCollection()
            .AddEntityFrameworkSqlServer()
            .AddNodaTime()
            .BuildServiceProvider(true);

        var builder = new DbContextOptionsBuilder<SimpleContext>()
            .UseInternalServiceProvider(serviceProvider)
            .UseSqlServer("Does not matter since we won't try to connect to the DB", opt => { opt.UseNodaTime(); });

        using var dbContext = new SimpleContext(builder.Options);

        dbContext.Add(new SupportedNodaTypes());
        Assert.Equal(1, dbContext.ChangeTracker.Entries().Count());
    }

    private class SimpleContext : DbContext
    {
        public SimpleContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<SupportedNodaTypes> TypeResults { get; set; }
    }
}
