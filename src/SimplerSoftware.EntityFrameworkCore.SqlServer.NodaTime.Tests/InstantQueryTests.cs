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
               condense(@"SELECT [r].[Id], [r].[EndTime], [r].[OffsetFromWinner], [r].[StartTime], [r].[StartTimeOffset] FROM [RaceResult] AS [r] WHERE [r].[StartTime] >= '2019-07-01T01:00:00.0000000Z'"),
               condense(this.Db.Sql));

            Assert.Equal(6, raceResults.Count);
        }

        [Fact]
        public async Task Instant_PlusYears()
        {
            var raceResults = await this.Db.RaceResult.Where(r => r.StartTime.PlusYears(1) >= Instant.FromUtc(2019, 7, 1, 1, 0)).ToListAsync();

            Assert.Equal(
                condense(@"SELECT [r].[Id], [r].[EndTime], [r].[OffsetFromWinner], [r].[StartTime], [r].[StartTimeOffset] FROM [RaceResult] AS [r] WHERE DATEADD(year, CAST(1 AS int), [r].[StartTime]) >= '2019-07-01T01:00:00.0000000Z'"),
                condense(this.Db.Sql));

            Assert.Equal(12, raceResults.Count);
        }

        [Fact]
        public async Task Instant_PlusMonths()
        {
            var raceResults = await this.Db.RaceResult.Where(r => r.StartTime.PlusMonths(1) >= Instant.FromUtc(2019, 7, 1, 1, 0)).ToListAsync();

            Assert.Equal(
                condense(@"SELECT [r].[Id], [r].[EndTime], [r].[OffsetFromWinner], [r].[StartTime], [r].[StartTimeOffset] FROM [RaceResult] AS [r] WHERE DATEADD(month, CAST(1 AS int), [r].[StartTime]) >= '2019-07-01T01:00:00.0000000Z'"),
                condense(this.Db.Sql));

            Assert.Equal(7, raceResults.Count);
        }

        [Fact]
        public async Task Instant_PlusDays()
        {
            var raceResults = await this.Db.RaceResult.Where(r => r.StartTime.PlusDays(45) >= Instant.FromUtc(2019, 7, 1, 1, 0)).ToListAsync();

            Assert.Equal(
                condense(@"SELECT [r].[Id], [r].[EndTime], [r].[OffsetFromWinner], [r].[StartTime], [r].[StartTimeOffset] FROM [RaceResult] AS [r] WHERE DATEADD(day, CAST(45.0E0 AS int), [r].[StartTime]) >= '2019-07-01T01:00:00.0000000Z'"),
                condense(this.Db.Sql));

            Assert.Equal(7, raceResults.Count);
        }

        [Fact]
        public async Task Instant_PlusHours()
        {
            var raceResults = await this.Db.RaceResult.Where(r => r.StartTime.PlusHours(45) >= Instant.FromUtc(2019, 7, 1, 1, 0)).ToListAsync();

            Assert.Equal(
                condense(@"SELECT [r].[Id], [r].[EndTime], [r].[OffsetFromWinner], [r].[StartTime], [r].[StartTimeOffset] FROM [RaceResult] AS [r] WHERE DATEADD(hour, CAST(45.0E0 AS int), [r].[StartTime]) >= '2019-07-01T01:00:00.0000000Z'"),
                condense(this.Db.Sql));

            Assert.Equal(6, raceResults.Count);
        }

        [Fact]
        public async Task Instant_PlusMinutes()
        {
            var raceResults = await this.Db.RaceResult.Where(r => r.StartTime.PlusMinutes(45) >= Instant.FromUtc(2019, 7, 1, 1, 0)).ToListAsync();

            Assert.Equal(
                condense(@"SELECT [r].[Id], [r].[EndTime], [r].[OffsetFromWinner], [r].[StartTime], [r].[StartTimeOffset] FROM [RaceResult] AS [r] WHERE DATEADD(minute, CAST(45.0E0 AS int), [r].[StartTime]) >= '2019-07-01T01:00:00.0000000Z'"),
                condense(this.Db.Sql));

            Assert.Equal(6, raceResults.Count);
        }

        [Fact]
        public async Task Instant_PlusSeconds()
        {
            var raceResults = await this.Db.RaceResult.Where(r => r.StartTime.PlusSeconds(45) >= Instant.FromUtc(2019, 7, 1, 1, 0)).ToListAsync();

            Assert.Equal(
                condense(@"SELECT [r].[Id], [r].[EndTime], [r].[OffsetFromWinner], [r].[StartTime], [r].[StartTimeOffset] FROM [RaceResult] AS [r] WHERE DATEADD(second, CAST(45.0E0 AS int), [r].[StartTime]) >= '2019-07-01T01:00:00.0000000Z'"),
                condense(this.Db.Sql));

            Assert.Equal(6, raceResults.Count);
        }

        [Fact]
        public async Task Instant_PlusMilliseconds()
        {
            var raceResults = await this.Db.RaceResult.Where(r => r.StartTime.PlusMilliseconds(45) >= Instant.FromUtc(2019, 7, 1, 1, 0)).ToListAsync();

            Assert.Equal(
                condense(@"SELECT [r].[Id], [r].[EndTime], [r].[OffsetFromWinner], [r].[StartTime], [r].[StartTimeOffset] FROM [RaceResult] AS [r] WHERE DATEADD(millisecond, CAST(45.0E0 AS int), [r].[StartTime]) >= '2019-07-01T01:00:00.0000000Z'"),
                condense(this.Db.Sql));

            Assert.Equal(6, raceResults.Count);
        }

        [Fact]
        public async Task Instant_DatePart_Year()
        {
            var raceResults = await this.Db.RaceResult.Where(r => r.StartTime.Year() == 2019).ToListAsync();
            Assert.Equal(
                condense(@"SELECT [r].[Id], [r].[EndTime], [r].[OffsetFromWinner], [r].[StartTime], [r].[StartTimeOffset] FROM [RaceResult] AS [r] WHERE DATEPART(year, [r].[StartTime]) = 2019"),
                condense(this.Db.Sql));

            Assert.Equal(12, raceResults.Count);
        }

        [Fact]
        public async Task Instant_DatePart_Quarter()
        {
            var raceResults = await this.Db.RaceResult.Where(r => r.StartTime.Quarter() == 4).ToListAsync();
            Assert.Equal(
                condense(@"SELECT [r].[Id], [r].[EndTime], [r].[OffsetFromWinner], [r].[StartTime], [r].[StartTimeOffset] FROM [RaceResult] AS [r] WHERE DATEPART(quarter, [r].[StartTime]) = 4"),
                condense(this.Db.Sql));

            Assert.Equal(3, raceResults.Count);
        }

        [Fact]
        public async Task Instant_DatePart_Month()
        {
            var raceResults = await this.Db.RaceResult.Where(r => r.StartTime.Month() == 12).ToListAsync();
            Assert.Equal(
                condense(@"SELECT [r].[Id], [r].[EndTime], [r].[OffsetFromWinner], [r].[StartTime], [r].[StartTimeOffset] FROM [RaceResult] AS [r] WHERE DATEPART(month, [r].[StartTime]) = 12"),
                condense(this.Db.Sql));

            Assert.Single(raceResults);
        }

        [Fact]
        public async Task Instant_DatePart_DayOfYear()
        {
            var raceResults = await this.Db.RaceResult.Where(r => r.StartTime.DayOfYear() == 1).ToListAsync();
            Assert.Equal(
                condense(@"SELECT [r].[Id], [r].[EndTime], [r].[OffsetFromWinner], [r].[StartTime], [r].[StartTimeOffset] FROM [RaceResult] AS [r] WHERE DATEPART(dayofyear, [r].[StartTime]) = 1"),
                condense(this.Db.Sql));

            Assert.Single(raceResults);
        }

        [Fact]
        public async Task Instant_DatePart_Day()
        {
            var raceResults = await this.Db.RaceResult.Where(r => r.StartTime.Day() == 1).ToListAsync();
            Assert.Equal(
                condense(@"SELECT [r].[Id], [r].[EndTime], [r].[OffsetFromWinner], [r].[StartTime], [r].[StartTimeOffset] FROM [RaceResult] AS [r] WHERE DATEPART(day, [r].[StartTime]) = 1"),
                condense(this.Db.Sql));

            Assert.Equal(12, raceResults.Count);
        }

        [Fact]
        public async Task Instant_DatePart_Week()
        {
            var raceResults = await this.Db.RaceResult.Where(r => r.StartTime.Week() == 1).ToListAsync();
            Assert.Equal(
                condense(@"SELECT [r].[Id], [r].[EndTime], [r].[OffsetFromWinner], [r].[StartTime], [r].[StartTimeOffset] FROM [RaceResult] AS [r] WHERE DATEPART(week, [r].[StartTime]) = 1"),
                condense(this.Db.Sql));

            Assert.Single(raceResults);
        }

        [Fact]
        public async Task Instant_DatePart_WeekDay()
        {
            var raceResults = await this.Db.RaceResult.Where(r => r.StartTime.WeekDay() == 1).ToListAsync();
            Assert.Equal(
                condense(@"SELECT [r].[Id], [r].[EndTime], [r].[OffsetFromWinner], [r].[StartTime], [r].[StartTimeOffset] FROM [RaceResult] AS [r] WHERE DATEPART(weekday, [r].[StartTime]) = 1"),
                condense(this.Db.Sql));

            Assert.Equal(2, raceResults.Count);
        }

        [Fact]
        public async Task Instant_DatePart_Hour()
        {
            var raceResults = await this.Db.RaceResult.Where(r => r.StartTime.Hour() == 12).ToListAsync();
            Assert.Equal(
                condense(@"SELECT [r].[Id], [r].[EndTime], [r].[OffsetFromWinner], [r].[StartTime], [r].[StartTimeOffset] FROM [RaceResult] AS [r] WHERE DATEPART(hour, [r].[StartTime]) = 12"),
                condense(this.Db.Sql));

            Assert.Single(raceResults);
        }

        [Fact]
        public async Task Instant_DatePart_Minute()
        {
            var raceResults = await this.Db.RaceResult.Where(r => r.StartTime.Minute() == 0).ToListAsync();
            Assert.Equal(
                condense(@"SELECT [r].[Id], [r].[EndTime], [r].[OffsetFromWinner], [r].[StartTime], [r].[StartTimeOffset] FROM [RaceResult] AS [r] WHERE DATEPART(minute, [r].[StartTime]) = 0"),
                condense(this.Db.Sql));

            Assert.Equal(12, raceResults.Count);
        }

        [Fact]
        public async Task Instant_DatePart_Second()
        {
            var raceResults = await this.Db.RaceResult.Where(r => r.StartTime.Second() == 0).ToListAsync();
            Assert.Equal(
                condense(@"SELECT [r].[Id], [r].[EndTime], [r].[OffsetFromWinner], [r].[StartTime], [r].[StartTimeOffset] FROM [RaceResult] AS [r] WHERE DATEPART(second, [r].[StartTime]) = 0"),
                condense(this.Db.Sql));

            Assert.Equal(12, raceResults.Count);
        }

        [Fact]
        public async Task Instant_DatePart_Millisecond()
        {
            var raceResults = await this.Db.RaceResult.Where(r => r.StartTime.Millisecond() == 0).ToListAsync();
            Assert.Equal(
                condense(@"SELECT [r].[Id], [r].[EndTime], [r].[OffsetFromWinner], [r].[StartTime], [r].[StartTimeOffset] FROM [RaceResult] AS [r] WHERE DATEPART(millisecond, [r].[StartTime]) = 0"),
                condense(this.Db.Sql));

            Assert.Equal(12, raceResults.Count);
        }

        [Fact]
        public async Task Instant_DatePart_Microsecond()
        {
            var raceResults = await this.Db.RaceResult.Where(r => r.StartTime.Microsecond() == 0).ToListAsync();
            Assert.Equal(
                condense(@"SELECT [r].[Id], [r].[EndTime], [r].[OffsetFromWinner], [r].[StartTime], [r].[StartTimeOffset] FROM [RaceResult] AS [r] WHERE DATEPART(microsecond, [r].[StartTime]) = 0"),
                condense(this.Db.Sql));

            Assert.Equal(12, raceResults.Count);
        }

        [Fact]
        public async Task Instant_DatePart_Nanosecond()
        {
            var raceResults = await this.Db.RaceResult.Where(r => r.StartTime.Nanosecond() == 0).ToListAsync();
            Assert.Equal(
                condense(@"SELECT [r].[Id], [r].[EndTime], [r].[OffsetFromWinner], [r].[StartTime], [r].[StartTimeOffset] FROM [RaceResult] AS [r] WHERE DATEPART(nanosecond, [r].[StartTime]) = 0"),
                condense(this.Db.Sql));

            Assert.Equal(12, raceResults.Count);
        }

        [Fact]
        public async Task Instant_DatePart_TzOffset()
        {
            var raceResults = await this.Db.RaceResult.Where(r => r.StartTime.TzOffset() == 0).ToListAsync();
            Assert.Equal(
                condense(@"SELECT [r].[Id], [r].[EndTime], [r].[OffsetFromWinner], [r].[StartTime], [r].[StartTimeOffset] FROM [RaceResult] AS [r] WHERE DATEPART(tzoffset, [r].[StartTime]) = 0"),
                condense(this.Db.Sql));

            Assert.Equal(12, raceResults.Count);
        }

        [Fact]
        public async Task Instant_DatePart_IsoWeek()
        {
            var raceResults = await this.Db.RaceResult.Where(r => r.StartTime.IsoWeek() == 1).ToListAsync();
            Assert.Equal(
                condense(@"SELECT [r].[Id], [r].[EndTime], [r].[OffsetFromWinner], [r].[StartTime], [r].[StartTimeOffset] FROM [RaceResult] AS [r] WHERE DATEPART(iso_week, [r].[StartTime]) = 1"),
                condense(this.Db.Sql));

            Assert.Single(raceResults);
        }

        [Fact]
        public async Task Instant_DateDiff_Year()
        {
            var raceResults = await this.Db.RaceResult.Where(r => this.Functions.DateDiffYear(r.StartTime, Instant.FromUtc(2020, 1, 1, 0, 0)) >= 1).ToListAsync();
            Assert.Equal(
                condense(@"SELECT [r].[Id], [r].[EndTime], [r].[OffsetFromWinner], [r].[StartTime], [r].[StartTimeOffset] FROM [RaceResult] AS [r] WHERE DATEDIFF(YEAR, [r].[StartTime], '2020-01-01T00:00:00.0000000Z') >= 1"),
                condense(this.Db.Sql));

            Assert.Equal(12, raceResults.Count);
        }

        [Fact]
        public async Task Instant_DateDiff_Month()
        {
            var raceResults = await this.Db.RaceResult.Where(r => this.Functions.DateDiffMonth(r.StartTime, Instant.FromUtc(2020, 1, 1, 0, 0)) >= 1).ToListAsync();
            Assert.Equal(
                condense(@"SELECT [r].[Id], [r].[EndTime], [r].[OffsetFromWinner], [r].[StartTime], [r].[StartTimeOffset] FROM [RaceResult] AS [r] WHERE DATEDIFF(MONTH, [r].[StartTime], '2020-01-01T00:00:00.0000000Z') >= 1"),
                condense(this.Db.Sql));

            Assert.Equal(12, raceResults.Count);
        }

        [Fact]
        public async Task Instant_DateDiff_Week()
        {
            var raceResults = await this.Db.RaceResult.Where(r => this.Functions.DateDiffWeek(r.StartTime, Instant.FromUtc(2019, 7, 1, 0, 0)) >= 10).ToListAsync();
            Assert.Equal(
                condense(@"SELECT [r].[Id], [r].[EndTime], [r].[OffsetFromWinner], [r].[StartTime], [r].[StartTimeOffset] FROM [RaceResult] AS [r] WHERE DATEDIFF(WEEK, [r].[StartTime], '2019-07-01T00:00:00.0000000Z') >= 10"),
                condense(this.Db.Sql));

            Assert.Equal(4, raceResults.Count);
        }

        [Fact]
        public async Task Instant_DateDiff_Day()
        {
            var raceResults = await this.Db.RaceResult.Where(r => this.Functions.DateDiffDay(r.StartTime, Instant.FromUtc(2019, 7, 1, 0, 0)) >= 100).ToListAsync();
            Assert.Equal(
                condense(@"SELECT [r].[Id], [r].[EndTime], [r].[OffsetFromWinner], [r].[StartTime], [r].[StartTimeOffset] FROM [RaceResult] AS [r] WHERE DATEDIFF(DAY, [r].[StartTime], '2019-07-01T00:00:00.0000000Z') >= 100"),
                condense(this.Db.Sql));

            Assert.Equal(3, raceResults.Count);
        }

        [Fact]
        public async Task Instant_DateDiff_Hour()
        {
            var raceResults = await this.Db.RaceResult.Where(r => this.Functions.DateDiffHour(r.StartTime, Instant.FromUtc(2019, 7, 1, 0, 0)) >= 1000).ToListAsync();
            Assert.Equal(
                condense(@"SELECT [r].[Id], [r].[EndTime], [r].[OffsetFromWinner], [r].[StartTime], [r].[StartTimeOffset] FROM [RaceResult] AS [r] WHERE DATEDIFF(HOUR, [r].[StartTime], '2019-07-01T00:00:00.0000000Z') >= 1000"),
                condense(this.Db.Sql));

            Assert.Equal(5, raceResults.Count);
        }

        [Fact]
        public async Task Instant_DateDiff_Minute()
        {
            var raceResults = await this.Db.RaceResult.Where(r => this.Functions.DateDiffMinute(r.StartTime, Instant.FromUtc(2019, 7, 1, 0, 0)) <= 100000).ToListAsync();
            Assert.Equal(
                condense(@"SELECT [r].[Id], [r].[EndTime], [r].[OffsetFromWinner], [r].[StartTime], [r].[StartTimeOffset] FROM [RaceResult] AS [r] WHERE DATEDIFF(MINUTE, [r].[StartTime], '2019-07-01T00:00:00.0000000Z') <= 100000"),
                condense(this.Db.Sql));

            Assert.Equal(8, raceResults.Count);
        }

        [Fact]
        public async Task Instant_DateDiff_Second()
        {
            var raceResults = await this.Db.RaceResult.Where(r => this.Functions.DateDiffSecond(r.StartTime, Instant.FromUtc(2019, 7, 1, 0, 0)) >= 100000).ToListAsync();
            Assert.Equal(
                condense(@"SELECT [r].[Id], [r].[EndTime], [r].[OffsetFromWinner], [r].[StartTime], [r].[StartTimeOffset] FROM [RaceResult] AS [r] WHERE DATEDIFF(SECOND, [r].[StartTime], '2019-07-01T00:00:00.0000000Z') >= 100000"),
                condense(this.Db.Sql));

            Assert.Equal(6, raceResults.Count);
        }

        [Fact]
        public async Task Instant_DateDiff_Millisecond()
        {
            var raceResults = await this.Db.RaceSplit.Where(r => this.Functions.DateDiffMillisecond(Instant.FromUtc(2020, 1, 1, 0, 0), r.TimeStampInstant) <= 15000000).ToListAsync();
            Assert.Equal(
                condense(@"SELECT [r].[Id], [r].[TimeStampInstant], [r].[TimeStampLocalDateTime], [r].[TimeStampLocalTime], [r].[TimeStampOffsetDateTime] FROM [RaceSplit] AS [r] WHERE DATEDIFF(MILLISECOND, '2020-01-01T00:00:00.0000000Z', [r].[TimeStampInstant]) <= 15000000"),
                condense(this.Db.Sql));

            Assert.Equal(4, raceResults.Count);
        }

        [Fact]
        public async Task Instant_DateDiff_Big_Second()
        {
            var raceResults = await this.Db.RaceResult.Where(r => this.Functions.DateDiffBigSecond(r.StartTime, Instant.FromUtc(2019, 7, 1, 0, 0)) >= 100000).ToListAsync();
            Assert.Equal(
                condense(@"SELECT [r].[Id], [r].[EndTime], [r].[OffsetFromWinner], [r].[StartTime], [r].[StartTimeOffset] FROM [RaceResult] AS [r] WHERE DATEDIFF_BIG(SECOND, [r].[StartTime], '2019-07-01T00:00:00.0000000Z') >= CAST(100000 AS bigint)"),
                condense(this.Db.Sql));

            Assert.Equal(6, raceResults.Count);
        }

        [Fact]
        public async Task Instant_DateDiff_Big_Millisecond()
        {
            var raceResults = await this.Db.RaceSplit.Where(r => this.Functions.DateDiffBigMillisecond(Instant.FromUtc(2020, 1, 1, 0, 0), r.TimeStampInstant) <= 15000000).ToListAsync();
            Assert.Equal(
                condense(@"SELECT [r].[Id], [r].[TimeStampInstant], [r].[TimeStampLocalDateTime], [r].[TimeStampLocalTime], [r].[TimeStampOffsetDateTime] FROM [RaceSplit] AS [r] WHERE DATEDIFF_BIG(MILLISECOND, '2020-01-01T00:00:00.0000000Z', [r].[TimeStampInstant]) <= CAST(15000000 AS bigint)"),
                condense(this.Db.Sql));

            Assert.Equal(4, raceResults.Count);
        }

        [Fact]
        public async Task Instant_DateDiff_Big_Microsecond()
        {
            var raceResults = await this.Db.RaceSplit.Where(r => this.Functions.DateDiffBigMicrosecond(Instant.FromUtc(2020, 1, 1, 0, 0), r.TimeStampInstant) <= 15000000000).ToListAsync();
            Assert.Equal(
                condense(@"SELECT [r].[Id], [r].[TimeStampInstant], [r].[TimeStampLocalDateTime], [r].[TimeStampLocalTime], [r].[TimeStampOffsetDateTime] FROM [RaceSplit] AS [r] WHERE DATEDIFF_BIG(MICROSECOND, '2020-01-01T00:00:00.0000000Z', [r].[TimeStampInstant]) <= CAST(15000000000 AS bigint)"),
                condense(this.Db.Sql));

            Assert.Equal(4, raceResults.Count);
        }

        [Fact]
        public async Task Instant_DateDiff_Big_Nanosecond()
        {
            var raceResults = await this.Db.RaceSplit.Where(r => this.Functions.DateDiffBigNanosecond(Instant.FromUtc(2020, 1, 1, 0, 0), r.TimeStampInstant) <= 15000000000000).ToListAsync();
            Assert.Equal(
                condense(@"SELECT [r].[Id], [r].[TimeStampInstant], [r].[TimeStampLocalDateTime], [r].[TimeStampLocalTime], [r].[TimeStampOffsetDateTime] FROM [RaceSplit] AS [r] WHERE DATEDIFF_BIG(NANOSECOND, '2020-01-01T00:00:00.0000000Z', [r].[TimeStampInstant]) <= CAST(15000000000000 AS bigint)"),
                condense(this.Db.Sql));

            Assert.Equal(4, raceResults.Count);
        }
    }
}