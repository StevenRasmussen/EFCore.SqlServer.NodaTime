using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.NodaTime.Extensions;
using NodaTime;
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

            Assert.Equal(
               condense(@"SELECT [r].[Id], [r].[EndTime], [r].[StartTime], [r].[StartTimeOffset] FROM [RaceResult] AS [r] WHERE [r].[StartTime] >= '2019-07-01T01:00:00.0000000Z'"),
               condense(this.Db.Sql));

            Assert.Equal(6, raceResults.Count);
        }

        [Fact]
        public async Task Instant_AddYears()
        {
            var raceResults = await this.Db.RaceResult.Where(r => r.StartTime.AddYears(1) >= Instant.FromUtc(2019, 7, 1, 1, 0)).ToListAsync();

            Assert.Equal(
              condense(@"SELECT [r].[Id], [r].[EndTime], [r].[StartTime], [r].[StartTimeOffset] FROM [RaceResult] AS [r] WHERE DATEADD(year, CAST(1 AS int), [r].[StartTime]) >= '2019-07-01T01:00:00.0000000Z'"),
              condense(this.Db.Sql));

            Assert.Equal(12, raceResults.Count);
        }

        [Fact]
        public async Task Instant_AddMonths()
        {
            var raceResults = await this.Db.RaceResult.Where(r => r.StartTime.AddMonths(1) >= Instant.FromUtc(2019, 7, 1, 1, 0)).ToListAsync();

            Assert.Equal(
              condense(@"SELECT [r].[Id], [r].[EndTime], [r].[StartTime], [r].[StartTimeOffset] FROM [RaceResult] AS [r] WHERE DATEADD(month, CAST(1 AS int), [r].[StartTime]) >= '2019-07-01T01:00:00.0000000Z'"),
              condense(this.Db.Sql));

            Assert.Equal(7, raceResults.Count);
        }

        [Fact]
        public async Task Instant_AddDays()
        {
            var raceResults = await this.Db.RaceResult.Where(r => r.StartTime.AddDays(45) >= Instant.FromUtc(2019, 7, 1, 1, 0)).ToListAsync();

            Assert.Equal(
              condense(@"SELECT [r].[Id], [r].[EndTime], [r].[StartTime], [r].[StartTimeOffset] FROM [RaceResult] AS [r] WHERE DATEADD(day, CAST(45.0E0 AS int), [r].[StartTime]) >= '2019-07-01T01:00:00.0000000Z'"),
              condense(this.Db.Sql));

            Assert.Equal(7, raceResults.Count);
        }

        [Fact]
        public async Task Instant_AddHours()
        {
            var raceResults = await this.Db.RaceResult.Where(r => r.StartTime.AddHours(45) >= Instant.FromUtc(2019, 7, 1, 1, 0)).ToListAsync();

            Assert.Equal(
              condense(@"SELECT [r].[Id], [r].[EndTime], [r].[StartTime], [r].[StartTimeOffset] FROM [RaceResult] AS [r] WHERE DATEADD(hour, CAST(45.0E0 AS int), [r].[StartTime]) >= '2019-07-01T01:00:00.0000000Z'"),
              condense(this.Db.Sql));

            Assert.Equal(6, raceResults.Count);
        }

        [Fact]
        public async Task Instant_AddMinutes()
        {
            var raceResults = await this.Db.RaceResult.Where(r => r.StartTime.AddMinutes(45) >= Instant.FromUtc(2019, 7, 1, 1, 0)).ToListAsync();

            Assert.Equal(
              condense(@"SELECT [r].[Id], [r].[EndTime], [r].[StartTime], [r].[StartTimeOffset] FROM [RaceResult] AS [r] WHERE DATEADD(minute, CAST(45.0E0 AS int), [r].[StartTime]) >= '2019-07-01T01:00:00.0000000Z'"),
              condense(this.Db.Sql));

            Assert.Equal(6, raceResults.Count);
        }

        [Fact]
        public async Task Instant_AddSeconds()
        {
            var raceResults = await this.Db.RaceResult.Where(r => r.StartTime.AddSeconds(45) >= Instant.FromUtc(2019, 7, 1, 1, 0)).ToListAsync();

            Assert.Equal(
              condense(@"SELECT [r].[Id], [r].[EndTime], [r].[StartTime], [r].[StartTimeOffset] FROM [RaceResult] AS [r] WHERE DATEADD(second, CAST(45.0E0 AS int), [r].[StartTime]) >= '2019-07-01T01:00:00.0000000Z'"),
              condense(this.Db.Sql));

            Assert.Equal(6, raceResults.Count);
        }

        [Fact]
        public async Task Instant_AddMilliseconds()
        {
            var raceResults = await this.Db.RaceResult.Where(r => r.StartTime.AddMilliseconds(45) >= Instant.FromUtc(2019, 7, 1, 1, 0)).ToListAsync();

            Assert.Equal(
              condense(@"SELECT [r].[Id], [r].[EndTime], [r].[StartTime], [r].[StartTimeOffset] FROM [RaceResult] AS [r] WHERE DATEADD(millisecond, CAST(45.0E0 AS int), [r].[StartTime]) >= '2019-07-01T01:00:00.0000000Z'"),
              condense(this.Db.Sql));

            Assert.Equal(6, raceResults.Count);
        }
    }
}
