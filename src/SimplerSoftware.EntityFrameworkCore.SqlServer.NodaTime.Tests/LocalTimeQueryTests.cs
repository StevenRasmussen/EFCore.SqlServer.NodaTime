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
    public class LocalTimeQueryTests : QueryTestBase
    {
        [Fact]
        public async Task LocalTime_can_be_used_in_query()
        {
            var raceResults = await this.Db.Race.Where(r => r.ScheduledStart >= new LocalTime(14, 0, 0)).ToListAsync();
            Assert.Equal(6, raceResults.Count);
        }
    }
}
