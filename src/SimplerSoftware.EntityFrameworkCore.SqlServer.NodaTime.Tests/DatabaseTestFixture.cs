using SimplerSoftware.EntityFrameworkCore.SqlServer.NodaTime.Tests.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SimplerSoftware.EntityFrameworkCore.SqlServer.NodaTime.Tests
{
    public class DatabaseTestFixture
    {
        public DatabaseTestFixture()
        {
            this.DbContext = new RacingContext();
            //this.DbContext.Database.EnsureDeleted();
            //this.DbContext.Database.EnsureCreated();
        }

        public RacingContext DbContext { get; }
    }

    [CollectionDefinition(nameof(DatabaseTestCollection))]
    public class DatabaseTestCollection: ICollectionFixture<DatabaseTestFixture>
    {
    }
}
