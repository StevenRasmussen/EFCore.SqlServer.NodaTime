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
    public class DurationQueryTests : QueryTestBase
    {
        public DurationQueryTests(DatabaseTestFixture databaseTestFixture)
            : base(databaseTestFixture) { }

        [Fact]
        public async Task Duration_can_be_used_in_query()
        {
            var raceResults = await this.Db.Race.Where(r => r.ScheduledDuration >= Duration.FromHours(7)).ToListAsync();

            Assert.Equal(
                condense(@"SELECT [r].[Id], [r].[Date], [r].[ScheduledDuration], [r].[ScheduledStart], [r].[ScheduledStartTime] FROM [Race] AS [r] WHERE [r].[ScheduledDuration] >= '07:00:00'"),
                condense(this.Db.Sql));

            Assert.Equal(6, raceResults.Count);
        }

        [Fact]
        public async Task Duration_PlusHours()
        {
            var raceResults = await this.Db.Race.Where(r => r.ScheduledDuration.PlusHours(2) >= Duration.FromHours(6)).ToListAsync();

            Assert.Equal(
                condense(@"SELECT [r].[Id], [r].[Date], [r].[ScheduledDuration], [r].[ScheduledStart], [r].[ScheduledStartTime] FROM [Race] AS [r] WHERE DATEADD(hour, CAST(2.0E0 AS int), [r].[ScheduledDuration]) >= '06:00:00'"),
                condense(this.Db.Sql));

            Assert.Equal(9, raceResults.Count);
        }

        [Fact]
        public async Task Duration_PlusMinutes()
        {
            var raceResults = await this.Db.Race.Where(r => r.ScheduledDuration.PlusMinutes(120) >= Duration.FromHours(6)).ToListAsync();

            Assert.Equal(
                 condense(@"SELECT [r].[Id], [r].[Date], [r].[ScheduledDuration], [r].[ScheduledStart], [r].[ScheduledStartTime] FROM [Race] AS [r] WHERE DATEADD(minute, CAST(120.0E0 AS int), [r].[ScheduledDuration]) >= '06:00:00'"),
                 condense(this.Db.Sql));

            Assert.Equal(9, raceResults.Count);
        }

        [Fact]
        public async Task Duration_PlusSeconds()
        {
            var raceResults = await this.Db.Race.Where(r => r.ScheduledDuration.PlusSeconds(7200) >= Duration.FromHours(6)).ToListAsync();

            Assert.Equal(
                 condense(@"SELECT [r].[Id], [r].[Date], [r].[ScheduledDuration], [r].[ScheduledStart], [r].[ScheduledStartTime] FROM [Race] AS [r] WHERE DATEADD(second, CAST(7200.0E0 AS int), [r].[ScheduledDuration]) >= '06:00:00'"),
                 condense(this.Db.Sql));

            Assert.Equal(9, raceResults.Count);
        }

        [Fact]
        public async Task Duration_PlusMilliseconds()
        {
            var raceResults = await this.Db.Race.Where(r => r.ScheduledDuration.PlusMilliseconds(7200000) >= Duration.FromHours(6)).ToListAsync();

            Assert.Equal(
                condense(@"SELECT [r].[Id], [r].[Date], [r].[ScheduledDuration], [r].[ScheduledStart], [r].[ScheduledStartTime] FROM [Race] AS [r] WHERE DATEADD(millisecond, CAST(7200000.0E0 AS int), [r].[ScheduledDuration]) >= '06:00:00'"),
                condense(this.Db.Sql));

            Assert.Equal(9, raceResults.Count);
        }

        [Fact]
        public async Task Duration_PlusMicroseconds()
        {
            var raceResults = await this.Db.Race.Where(r => r.ScheduledDuration.PlusMicroseconds(7200000) >= Duration.FromHours(6)).ToListAsync();

            Assert.Equal(
                 condense(@"SELECT [r].[Id], [r].[Date], [r].[ScheduledDuration], [r].[ScheduledStart], [r].[ScheduledStartTime] FROM [Race] AS [r] WHERE DATEADD(microsecond, CAST(7200000.0E0 AS int), [r].[ScheduledDuration]) >= '06:00:00'"),
                 condense(this.Db.Sql));

            Assert.Equal(7, raceResults.Count);
        }

        [Fact]
        public async Task Duration_PlusNanoseconds()
        {
            var raceResults = await this.Db.Race.Where(r => r.ScheduledDuration.PlusNanoseconds(7200000) >= Duration.FromHours(6)).ToListAsync();

            Assert.Equal(
                condense(@"SELECT [r].[Id], [r].[Date], [r].[ScheduledDuration], [r].[ScheduledStart], [r].[ScheduledStartTime] FROM [Race] AS [r] WHERE DATEADD(nanosecond, CAST(7200000.0E0 AS int), [r].[ScheduledDuration]) >= '06:00:00'"),
                condense(this.Db.Sql));

            Assert.Equal(7, raceResults.Count);
        }

        [Fact]
        public async Task Duration_DatePart_Hour()
        {
            var raceResults = await this.Db.Race.Where(r => r.ScheduledDuration.Hours == 12).ToListAsync();

            Assert.Equal(
                condense(@"SELECT [r].[Id], [r].[Date], [r].[ScheduledDuration], [r].[ScheduledStart], [r].[ScheduledStartTime] FROM [Race] AS [r] WHERE DATEPART(hour, [r].[ScheduledDuration]) = 12"),
                condense(this.Db.Sql));

            Assert.Single(raceResults);
        }

        [Fact]
        public async Task Duration_DatePart_Minute()
        {
            var raceResults = await this.Db.Race.Where(r => r.ScheduledDuration.Minutes == 6).ToListAsync();
            Assert.Equal(
                 condense(@"SELECT [r].[Id], [r].[Date], [r].[ScheduledDuration], [r].[ScheduledStart], [r].[ScheduledStartTime] FROM [Race] AS [r] WHERE DATEPART(minute, [r].[ScheduledDuration]) = 6"),
                 condense(this.Db.Sql));

            Assert.Single(raceResults);
        }

        [Fact]
        public async Task Duration_DatePart_Second()
        {
            var raceResults = await this.Db.Race.Where(r => r.ScheduledDuration.Seconds == 6).ToListAsync();
            Assert.Equal(
                condense(@"SELECT [r].[Id], [r].[Date], [r].[ScheduledDuration], [r].[ScheduledStart], [r].[ScheduledStartTime] FROM [Race] AS [r] WHERE DATEPART(second, [r].[ScheduledDuration]) = 6"),
                condense(this.Db.Sql));

            Assert.Single(raceResults);
        }

        [Fact]
        public async Task Duration_DatePart_Millisecond()
        {
            var raceResults = await this.Db.Race.Where(r => r.ScheduledDuration.Milliseconds == 6).ToListAsync();
            Assert.Equal(
                condense(@"SELECT [r].[Id], [r].[Date], [r].[ScheduledDuration], [r].[ScheduledStart], [r].[ScheduledStartTime] FROM [Race] AS [r] WHERE DATEPART(millisecond, [r].[ScheduledDuration]) = 6"),
                condense(this.Db.Sql));

            Assert.Single(raceResults);
        }

        [Fact]
        public async Task Duration_DatePart_Microsecond()
        {
            var raceResults = await this.Db.Race.Where(r => r.ScheduledDuration.Microseconds() == 1000).ToListAsync();
            Assert.Equal(
                condense(@"SELECT [r].[Id], [r].[Date], [r].[ScheduledDuration], [r].[ScheduledStart], [r].[ScheduledStartTime] FROM [Race] AS [r] WHERE DATEPART(microsecond, [r].[ScheduledDuration]) = 1000"),
                condense(this.Db.Sql));

            Assert.Single(raceResults);
        }

        [Fact]
        public async Task Duration_DatePart_Nanosecond()
        {
            var raceResults = await this.Db.Race.Where(r => r.ScheduledDuration.SubsecondNanoseconds == 1000000).ToListAsync();
            Assert.Equal(
                condense(@"SELECT [r].[Id], [r].[Date], [r].[ScheduledDuration], [r].[ScheduledStart], [r].[ScheduledStartTime] FROM [Race] AS [r] WHERE DATEPART(nanosecond, [r].[ScheduledDuration]) = 1000000"),
                condense(this.Db.Sql));

            Assert.Single(raceResults);
        }
    }
}
