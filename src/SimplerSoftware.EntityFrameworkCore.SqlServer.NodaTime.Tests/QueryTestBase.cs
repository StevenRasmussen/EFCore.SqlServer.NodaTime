using SimplerSoftware.EntityFrameworkCore.SqlServer.NodaTime.Tests.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimplerSoftware.EntityFrameworkCore.SqlServer.NodaTime.Tests
{
    public abstract class QueryTestBase
    {
        public QueryTestBase()
        {
            this.Db = new RacingContext();
            this.Db.Database.EnsureDeleted();
            this.Db.Database.EnsureCreated();
        }

        protected RacingContext Db { get; }
    }
}
