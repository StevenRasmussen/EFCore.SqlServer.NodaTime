using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace SimplerSoftware.EntityFrameworkCore.SqlServer.NodaTime.Tests
{
    public class DateTimeQueryTests : QueryTestBase
    {
        public DateTimeQueryTests(DatabaseTestFixture databaseTestFixture)
            : base(databaseTestFixture) { }

        [Fact]
        public async Task DateTime_Date_Test()
        {
            var raceResults = await this.Db.Race.Where(r => r.DateTimeDate.Date >= new DateTime(2019, 7, 1)).ToListAsync();

            Assert.Equal(
                condense(@$"{RaceSelectStatement} WHERE CONVERT(date, [r].[DateTimeDate]) >= '2019-07-01T00:00:00.0000000'"),
                condense(this.Db.Sql));

            Assert.Equal(6, raceResults.Count);
        }
    }
}