using Microsoft.EntityFrameworkCore;
using NodaTime;
using SimplerSoftware.EntityFrameworkCore.SqlServer.NodaTime.Tests.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SimplerSoftware.EntityFrameworkCore.SqlServer.NodaTime.Tests
{
    public class LocalDateQueryTests : QueryTestBase
    {
        public LocalDateQueryTests(DatabaseTestFixture databaseTestFixture)
            : base(databaseTestFixture) { }

        [Fact]
        public async Task LocalDate_can_be_used_in_query()
        {
            var raceResults = await this.Db.Race.Where(r => r.Date >= new LocalDate(2019, 7, 1)).ToListAsync();
            Assert.Equal(6, raceResults.Count);
        }
    }
}
