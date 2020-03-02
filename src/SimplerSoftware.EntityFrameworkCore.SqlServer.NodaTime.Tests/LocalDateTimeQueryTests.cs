using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.NodaTime.Extensions;
using NodaTime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SimplerSoftware.EntityFrameworkCore.SqlServer.NodaTime.Tests
{
    public class LocalDateTimeQueryTests : QueryTestBase
    {
        public LocalDateTimeQueryTests(DatabaseTestFixture databaseTestFixture)
            : base(databaseTestFixture) { }

        [Fact]
        public async Task LocalDateTime_can_be_used_in_query()
        {
            var raceResults = await this.Db.Race.Where(r => r.ScheduledStart >= new LocalDateTime(2019, 7, 1, 0, 0)).ToListAsync();

            Assert.Equal(
               condense(@"SELECT [r].[Id], [r].[Date], [r].[ScheduledDuration], [r].[ScheduledStart], [r].[ScheduledStartTime] FROM [Race] AS [r] WHERE [r].[ScheduledStart] >= '2019-07-01T00:00:00.0000000'"),
               condense(this.Db.Sql));

            Assert.Equal(6, raceResults.Count);
        }

        [Fact]
        public async Task LocalDateTime_PlusYears()
        {
            var raceResults = await this.Db.Race.Where(r => r.ScheduledStart.PlusYears(1) >= new LocalDateTime(2019, 7, 1, 0, 0)).ToListAsync();

            Assert.Equal(
               condense(@"SELECT [r].[Id], [r].[Date], [r].[ScheduledDuration], [r].[ScheduledStart], [r].[ScheduledStartTime] FROM [Race] AS [r] WHERE DATEADD(year, CAST(1 AS int), [r].[ScheduledStart]) >= '2019-07-01T00:00:00.0000000'"),
               condense(this.Db.Sql));

            Assert.Equal(12, raceResults.Count);
        }

        [Fact]
        public async Task LocalDateTime_PlusMonths()
        {
            var raceResults = await this.Db.Race.Where(r => r.ScheduledStart.PlusMonths(1) >= new LocalDateTime(2019, 7, 1, 0, 0)).ToListAsync();

            Assert.Equal(
               condense(@"SELECT [r].[Id], [r].[Date], [r].[ScheduledDuration], [r].[ScheduledStart], [r].[ScheduledStartTime] FROM [Race] AS [r] WHERE DATEADD(month, CAST(1 AS int), [r].[ScheduledStart]) >= '2019-07-01T00:00:00.0000000'"),
               condense(this.Db.Sql));

            Assert.Equal(7, raceResults.Count);
        }

        [Fact]
        public async Task LocalDateTime_PlusDays()
        {
            var raceResults = await this.Db.Race.Where(r => r.ScheduledStart.PlusDays(45) >= new LocalDateTime(2019, 7, 1, 0, 0)).ToListAsync();

            Assert.Equal(
               condense(@"SELECT [r].[Id], [r].[Date], [r].[ScheduledDuration], [r].[ScheduledStart], [r].[ScheduledStartTime] FROM [Race] AS [r] WHERE DATEADD(day, CAST(45 AS int), [r].[ScheduledStart]) >= '2019-07-01T00:00:00.0000000'"),
               condense(this.Db.Sql));

            Assert.Equal(7, raceResults.Count);
        }

        [Fact]
        public async Task LocalDateTime_PlusWeeks()
        {
            var raceResults = await this.Db.Race.Where(r => r.ScheduledStart.PlusWeeks(5) >= new LocalDateTime(2019, 7, 1, 0, 0)).ToListAsync();

            Assert.Equal(
               condense(@"SELECT [r].[Id], [r].[Date], [r].[ScheduledDuration], [r].[ScheduledStart], [r].[ScheduledStartTime] FROM [Race] AS [r] WHERE DATEADD(week, CAST(5 AS int), [r].[ScheduledStart]) >= '2019-07-01T00:00:00.0000000'"),
               condense(this.Db.Sql));

            Assert.Equal(7, raceResults.Count);
        }

        [Fact]
        public async Task LocalDateTime_PlusQuarters()
        {
            var raceResults = await this.Db.Race.Where(r => r.ScheduledStart.PlusQuarters(1) >= new LocalDateTime(2019, 7, 1, 0, 0)).ToListAsync();

            Assert.Equal(
               condense(@"SELECT [r].[Id], [r].[Date], [r].[ScheduledDuration], [r].[ScheduledStart], [r].[ScheduledStartTime] FROM [Race] AS [r] WHERE DATEADD(quarter, CAST(1 AS int), [r].[ScheduledStart]) >= '2019-07-01T00:00:00.0000000'"),
               condense(this.Db.Sql));

            Assert.Equal(9, raceResults.Count);
        }

        [Fact]
        public async Task LocalDateTime_PlusHours()
        {
            var raceResults = await this.Db.Race.Where(r => r.ScheduledStart.PlusHours(2) >= new LocalDateTime(2019, 7, 1, 12, 0, 0)).ToListAsync();

            Assert.Equal(
                condense(@"SELECT [r].[Id], [r].[Date], [r].[ScheduledDuration], [r].[ScheduledStart], [r].[ScheduledStartTime] FROM [Race] AS [r] WHERE DATEADD(hour, CAST(CAST(2 AS bigint) AS int), [r].[ScheduledStart]) >= '2019-07-01T12:00:00.0000000'"),
                condense(this.Db.Sql));

            Assert.Equal(6, raceResults.Count);
        }

        [Fact]
        public async Task LocalDateTime_PlusMinutes()
        {
            var raceResults = await this.Db.Race.Where(r => r.ScheduledStart.PlusMinutes(120) >= new LocalDateTime(2019, 7, 1, 12, 0, 0)).ToListAsync();

            Assert.Equal(
                condense(@"SELECT [r].[Id], [r].[Date], [r].[ScheduledDuration], [r].[ScheduledStart], [r].[ScheduledStartTime] FROM [Race] AS [r] WHERE DATEADD(minute, CAST(CAST(120 AS bigint) AS int), [r].[ScheduledStart]) >= '2019-07-01T12:00:00.0000000'"),
                condense(this.Db.Sql));

            Assert.Equal(6, raceResults.Count);
        }

        [Fact]
        public async Task LocalDateTime_PlusSeconds()
        {
            var raceResults = await this.Db.Race.Where(r => r.ScheduledStart.PlusSeconds(7200) >= new LocalDateTime(2019, 7, 1, 12, 0, 0)).ToListAsync();

            Assert.Equal(
                condense(@"SELECT [r].[Id], [r].[Date], [r].[ScheduledDuration], [r].[ScheduledStart], [r].[ScheduledStartTime] FROM [Race] AS [r] WHERE DATEADD(second, CAST(CAST(7200 AS bigint) AS int), [r].[ScheduledStart]) >= '2019-07-01T12:00:00.0000000'"),
                condense(this.Db.Sql));

            Assert.Equal(6, raceResults.Count);
        }

        [Fact]
        public async Task LocalDateTime_PlusMilliseconds()
        {
            var raceResults = await this.Db.Race.Where(r => r.ScheduledStart.PlusMilliseconds(7200000) >= new LocalDateTime(2019, 7, 1, 12, 0, 0)).ToListAsync();

            Assert.Equal(
                condense(@"SELECT [r].[Id], [r].[Date], [r].[ScheduledDuration], [r].[ScheduledStart], [r].[ScheduledStartTime] FROM [Race] AS [r] WHERE DATEADD(millisecond, CAST(CAST(7200000 AS bigint) AS int), [r].[ScheduledStart]) >= '2019-07-01T12:00:00.0000000'"),
                condense(this.Db.Sql));

            Assert.Equal(6, raceResults.Count);
        }

        [Fact]
        public async Task LocalDateTime_PlusMicroseconds()
        {
            var raceResults = await this.Db.Race.Where(r => r.ScheduledStart.PlusMicroseconds(7200000) >= new LocalDateTime(2019, 7, 1, 12, 0, 0)).ToListAsync();

            Assert.Equal(
                condense(@"SELECT [r].[Id], [r].[Date], [r].[ScheduledDuration], [r].[ScheduledStart], [r].[ScheduledStartTime] FROM [Race] AS [r] WHERE DATEADD(microsecond, CAST(CAST(7200000 AS bigint) AS int), [r].[ScheduledStart]) >= '2019-07-01T12:00:00.0000000'"),
                condense(this.Db.Sql));

            Assert.Equal(6, raceResults.Count);
        }

        [Fact]
        public async Task LocalDateTime_PlusNanoseconds()
        {
            var raceResults = await this.Db.Race.Where(r => r.ScheduledStart.PlusNanoseconds(7200000) >= new LocalDateTime(2019, 7, 1, 12, 0, 0)).ToListAsync();

            Assert.Equal(
                condense(@"SELECT [r].[Id], [r].[Date], [r].[ScheduledDuration], [r].[ScheduledStart], [r].[ScheduledStartTime] FROM [Race] AS [r] WHERE DATEADD(nanosecond, CAST(CAST(7200000 AS bigint) AS int), [r].[ScheduledStart]) >= '2019-07-01T12:00:00.0000000'"),
                condense(this.Db.Sql));

            Assert.Equal(6, raceResults.Count);
        }

        [Fact]
        public async Task LocalDateTime_DatePart_Year()
        {
            var raceResults = await this.Db.Race.Where(r => r.ScheduledStart.Year == 2019).ToListAsync();

            Assert.Equal(
               condense(@"SELECT [r].[Id], [r].[Date], [r].[ScheduledDuration], [r].[ScheduledStart], [r].[ScheduledStartTime] FROM [Race] AS [r] WHERE DATEPART(year, [r].[ScheduledStart]) = 2019"),
               condense(this.Db.Sql));

            Assert.Equal(12, raceResults.Count);
        }

        [Fact]
        public async Task LocalDateTime_DatePart_Quarter()
        {
            var raceResults = await this.Db.Race.Where(r => r.ScheduledStart.Quarter() == 4).ToListAsync();

            Assert.Equal(
               condense(@"SELECT [r].[Id], [r].[Date], [r].[ScheduledDuration], [r].[ScheduledStart], [r].[ScheduledStartTime] FROM [Race] AS [r] WHERE DATEPART(quarter, [r].[ScheduledStart]) = 4"),
               condense(this.Db.Sql));

            Assert.Equal(3, raceResults.Count);
        }

        [Fact]
        public async Task LocalDateTime_DatePart_Month()
        {
            var raceResults = await this.Db.Race.Where(r => r.ScheduledStart.Month == 12).ToListAsync();

            Assert.Equal(
               condense(@"SELECT [r].[Id], [r].[Date], [r].[ScheduledDuration], [r].[ScheduledStart], [r].[ScheduledStartTime] FROM [Race] AS [r] WHERE DATEPART(month, [r].[ScheduledStart]) = 12"),
               condense(this.Db.Sql));

            Assert.Single(raceResults);
        }

        [Fact]
        public async Task LocalDateTime_DatePart_DayOfYear()
        {
            var raceResults = await this.Db.Race.Where(r => r.ScheduledStart.DayOfYear == 1).ToListAsync();

            Assert.Equal(
               condense(@"SELECT [r].[Id], [r].[Date], [r].[ScheduledDuration], [r].[ScheduledStart], [r].[ScheduledStartTime] FROM [Race] AS [r] WHERE DATEPART(dayofyear, [r].[ScheduledStart]) = 1"),
               condense(this.Db.Sql));

            Assert.Single(raceResults);
        }

        [Fact]
        public async Task LocalDateTime_DatePart_Day()
        {
            var raceResults = await this.Db.Race.Where(r => r.ScheduledStart.Day == 1).ToListAsync();

            Assert.Equal(
               condense(@"SELECT [r].[Id], [r].[Date], [r].[ScheduledDuration], [r].[ScheduledStart], [r].[ScheduledStartTime] FROM [Race] AS [r] WHERE DATEPART(day, [r].[ScheduledStart]) = 1"),
               condense(this.Db.Sql));

            Assert.Equal(12, raceResults.Count);
        }

        [Fact]
        public async Task LocalDateTime_DatePart_Week()
        {
            var raceResults = await this.Db.Race.Where(r => r.ScheduledStart.Week() == 1).ToListAsync();

            Assert.Equal(
               condense(@"SELECT [r].[Id], [r].[Date], [r].[ScheduledDuration], [r].[ScheduledStart], [r].[ScheduledStartTime] FROM [Race] AS [r] WHERE DATEPART(week, [r].[ScheduledStart]) = 1"),
               condense(this.Db.Sql));

            Assert.Single(raceResults);
        }

        [Fact]
        public async Task LocalDateTime_DatePart_Hour()
        {
            var raceResults = await this.Db.Race.Where(r => r.ScheduledStart.Hour == 12).ToListAsync();

            Assert.Equal(
                condense(@"SELECT [r].[Id], [r].[Date], [r].[ScheduledDuration], [r].[ScheduledStart], [r].[ScheduledStartTime] FROM [Race] AS [r] WHERE DATEPART(hour, [r].[ScheduledStart]) = 12"),
                condense(this.Db.Sql));

            Assert.Single(raceResults);
        }

        [Fact]
        public async Task LocalDateTime_DatePart_Minute()
        {
            var raceResults = await this.Db.Race.Where(r => r.ScheduledStart.Minute == 6).ToListAsync();
            Assert.Equal(
                condense(@"SELECT [r].[Id], [r].[Date], [r].[ScheduledDuration], [r].[ScheduledStart], [r].[ScheduledStartTime] FROM [Race] AS [r] WHERE DATEPART(minute, [r].[ScheduledStart]) = 6"),
                condense(this.Db.Sql));

            Assert.Single(raceResults);
        }

        [Fact]
        public async Task LocalDateTime_DatePart_Second()
        {
            var raceResults = await this.Db.Race.Where(r => r.ScheduledStart.Second == 6).ToListAsync();
            Assert.Equal(
                 condense(@"SELECT [r].[Id], [r].[Date], [r].[ScheduledDuration], [r].[ScheduledStart], [r].[ScheduledStartTime] FROM [Race] AS [r] WHERE DATEPART(second, [r].[ScheduledStart]) = 6"),
                 condense(this.Db.Sql));

            Assert.Single(raceResults);
        }

        [Fact]
        public async Task LocalDateTime_DatePart_Millisecond()
        {
            var raceResults = await this.Db.Race.Where(r => r.ScheduledStart.Millisecond == 6).ToListAsync();
            Assert.Equal(
                condense(@"SELECT [r].[Id], [r].[Date], [r].[ScheduledDuration], [r].[ScheduledStart], [r].[ScheduledStartTime] FROM [Race] AS [r] WHERE DATEPART(millisecond, [r].[ScheduledStart]) = 6"),
                condense(this.Db.Sql));

            Assert.Single(raceResults);
        }

        [Fact]
        public async Task LocalDateTime_DatePart_Microsecond()
        {
            var raceResults = await this.Db.Race.Where(r => r.ScheduledStart.Microsecond() == 1000).ToListAsync();
            Assert.Equal(
                 condense(@"SELECT [r].[Id], [r].[Date], [r].[ScheduledDuration], [r].[ScheduledStart], [r].[ScheduledStartTime] FROM [Race] AS [r] WHERE DATEPART(microsecond, [r].[ScheduledStart]) = 1000"),
                 condense(this.Db.Sql));

            Assert.Single(raceResults);
        }

        [Fact]
        public async Task LocalDateTime_DatePart_Nanosecond()
        {
            var raceResults = await this.Db.Race.Where(r => r.ScheduledStart.NanosecondOfSecond == 1000000).ToListAsync();
            Assert.Equal(
                condense(@"SELECT [r].[Id], [r].[Date], [r].[ScheduledDuration], [r].[ScheduledStart], [r].[ScheduledStartTime] FROM [Race] AS [r] WHERE DATEPART(nanosecond, [r].[ScheduledStart]) = 1000000"),
                condense(this.Db.Sql));

            Assert.Single(raceResults);
        }
    }
}
