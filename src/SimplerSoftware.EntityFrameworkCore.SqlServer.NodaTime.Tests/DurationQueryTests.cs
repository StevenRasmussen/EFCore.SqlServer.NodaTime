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
    public class DurationQueryTests : QueryTestBase
    {
        public DurationQueryTests(DatabaseTestFixture databaseTestFixture)
            : base(databaseTestFixture) { }

        [Fact]
        public async Task Duration_can_be_used_in_query()
        {
            var raceResults = await this.Db.Race.Where(r => r.ScheduledDuration >= Duration.FromHours(7)).ToListAsync();
            Assert.Equal(6, raceResults.Count);
        }
    }
}
