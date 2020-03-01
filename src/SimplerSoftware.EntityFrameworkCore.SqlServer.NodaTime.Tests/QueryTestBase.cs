using SimplerSoftware.EntityFrameworkCore.SqlServer.NodaTime.Tests.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SimplerSoftware.EntityFrameworkCore.SqlServer.NodaTime.Tests
{
    [Collection(nameof(DatabaseTestCollection))]
    public abstract class QueryTestBase
    {
        public QueryTestBase(DatabaseTestFixture databaseTestFixture)
        {
            this.Db = databaseTestFixture.DbContext;
        }

        protected RacingContext Db { get; }
    }
}
