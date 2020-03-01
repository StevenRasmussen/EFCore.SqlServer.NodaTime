using Microsoft.EntityFrameworkCore;
using NodaTime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SimplerSoftware.EntityFrameworkCore.SqlServer.NodaTime.Tests
{
    public class OffsetDateTimeQueryTests : QueryTestBase
    {
        public OffsetDateTimeQueryTests(DatabaseTestFixture databaseTestFixture)
            : base(databaseTestFixture) { }

        [Fact]
        public async Task OffsetDateTime_can_be_used_in_query()
        {
            var raceResults = await this.Db.RaceResult.Where(r => r.StartTimeOffset == OffsetDateTime.FromDateTimeOffset(new DateTimeOffset(new DateTime(2019, 7, 1, 0, 0, 0), TimeSpan.FromHours(5)))).ToListAsync();
            Assert.Single(raceResults);
        }
    }
}
