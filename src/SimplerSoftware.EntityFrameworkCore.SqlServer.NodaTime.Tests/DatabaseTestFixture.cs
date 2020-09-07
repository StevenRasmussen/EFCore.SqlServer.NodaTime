using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.NodaTime.Extensions;
using SimplerSoftware.EntityFrameworkCore.SqlServer.NodaTime.Tests.Models;
using Xunit;

namespace SimplerSoftware.EntityFrameworkCore.SqlServer.NodaTime.Tests
{
    public class DatabaseTestFixture
    {
        public DatabaseTestFixture()
        {
            this.SqlConnection = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=NodaTimeTests");

            this.DbContextOptions = new DbContextOptionsBuilder<RacingContext>()
                .UseSqlServer(this.SqlConnection, x => x.UseNodaTime())
                .Options;

            this.DbContext = new RacingContext(this.DbContextOptions);
            //this.DbContext.Database.EnsureDeleted();
            //this.DbContext.Database.EnsureCreated();
        }

        public RacingContext DbContext { get; }

        public DbContextOptions<RacingContext> DbContextOptions { get; }

        public SqlConnection SqlConnection { get; }
    }

    [CollectionDefinition(nameof(DatabaseTestCollection))]
    public class DatabaseTestCollection : ICollectionFixture<DatabaseTestFixture>
    {
    }
}
