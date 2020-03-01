using Microsoft.EntityFrameworkCore;
using NodaTime;
using SimplerSoftware.EntityFrameworkCore.SqlServer.NodaTime.Tests.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace SimplerSoftware.EntityFrameworkCore.SqlServer.NodaTime.Tests
{
    public class InstantQueryTests : QueryTestBase
    {
        public InstantQueryTests(DatabaseTestFixture databaseTestFixture)
            : base(databaseTestFixture) { }

        [Fact]
        public async Task Instant_can_be_used_in_query()
        {
            var raceResults = await this.Db.RaceResult.Where(r => r.StartTime >= Instant.FromUtc(2019, 7, 1, 1, 0)).ToListAsync();
            Assert.Equal(6, raceResults.Count);
        }
    }
}
