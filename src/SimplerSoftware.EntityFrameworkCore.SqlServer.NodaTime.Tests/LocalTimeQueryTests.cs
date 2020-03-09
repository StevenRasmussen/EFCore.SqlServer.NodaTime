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
    public class LocalTimeQueryTests : QueryTestBase
    {
        public LocalTimeQueryTests(DatabaseTestFixture databaseTestFixture)
            : base(databaseTestFixture) { }

        [Fact]
        public async Task LocalTime_can_be_used_in_query()
        {
            var raceResults = await this.Db.Race.Where(r => r.ScheduledStartTime >= new LocalTime(14, 0, 0)).ToListAsync();

            Assert.Equal(
                condense(@$"{RaceSelectStatement} WHERE [r].[ScheduledStartTime] >= '14:00:00'"),
                condense(this.Db.Sql));

            Assert.Equal(6, raceResults.Count);
        }

        [Fact]
        public async Task LocalTime_PlusHours()
        {
            var raceResults = await this.Db.Race.Where(r => r.ScheduledStartTime.PlusHours(2) >= new LocalTime(12, 0, 0)).ToListAsync();

            Assert.Equal(
                condense(@$"{RaceSelectStatement} WHERE DATEADD(hour, CAST(CAST(2 AS bigint) AS int), [r].[ScheduledStartTime]) >= '12:00:00'"),
                condense(this.Db.Sql));

            Assert.Equal(10, raceResults.Count);
        }

        [Fact]
        public async Task LocalTime_PlusMinutes()
        {
            var raceResults = await this.Db.Race.Where(r => r.ScheduledStartTime.PlusMinutes(120) >= new LocalTime(12, 0, 0)).ToListAsync();

            Assert.Equal(
                condense(@$"{RaceSelectStatement} WHERE DATEADD(minute, CAST(CAST(120 AS bigint) AS int), [r].[ScheduledStartTime]) >= '12:00:00'"),
                condense(this.Db.Sql));

            Assert.Equal(10, raceResults.Count);
        }

        [Fact]
        public async Task LocalTime_PlusSeconds()
        {
            var raceResults = await this.Db.Race.Where(r => r.ScheduledStartTime.PlusSeconds(7200) >= new LocalTime(12, 0, 0)).ToListAsync();

            Assert.Equal(
                condense(@$"{RaceSelectStatement} WHERE DATEADD(second, CAST(CAST(7200 AS bigint) AS int), [r].[ScheduledStartTime]) >= '12:00:00'"),
                condense(this.Db.Sql));

            Assert.Equal(10, raceResults.Count);
        }

        [Fact]
        public async Task LocalTime_PlusMilliseconds()
        {
            var raceResults = await this.Db.Race.Where(r => r.ScheduledStartTime.PlusMilliseconds(7200000) >= new LocalTime(12, 0, 0)).ToListAsync();

            Assert.Equal(
                condense(@$"{RaceSelectStatement} WHERE DATEADD(millisecond, CAST(CAST(7200000 AS bigint) AS int), [r].[ScheduledStartTime]) >= '12:00:00'"),
                condense(this.Db.Sql));

            Assert.Equal(10, raceResults.Count);
        }

        [Fact]
        public async Task LocalTime_PlusMicroseconds()
        {
            var raceResults = await this.Db.Race.Where(r => r.ScheduledStartTime.PlusMicroseconds(7200000) >= new LocalTime(12, 0, 0)).ToListAsync();

            Assert.Equal(
                condense(@$"{RaceSelectStatement} WHERE DATEADD(microsecond, CAST(CAST(7200000 AS bigint) AS int), [r].[ScheduledStartTime]) >= '12:00:00'"),
                condense(this.Db.Sql));

            Assert.Equal(8, raceResults.Count);
        }

        [Fact]
        public async Task LocalTime_PlusNanoseconds()
        {
            var raceResults = await this.Db.Race.Where(r => r.ScheduledStartTime.PlusNanoseconds(7200000) >= new LocalTime(12, 0, 0)).ToListAsync();

            Assert.Equal(
                condense(@$"{RaceSelectStatement} WHERE DATEADD(nanosecond, CAST(CAST(7200000 AS bigint) AS int), [r].[ScheduledStartTime]) >= '12:00:00'"),
                condense(this.Db.Sql));

            Assert.Equal(8, raceResults.Count);
        }

        [Fact]
        public async Task LocalTime_DatePart_Hour()
        {
            var raceResults = await this.Db.Race.Where(r => r.ScheduledStartTime.Hour == 12).ToListAsync();

            Assert.Equal(
                condense(@$"{RaceSelectStatement} WHERE DATEPART(hour, [r].[ScheduledStartTime]) = 12"),
                condense(this.Db.Sql));

            Assert.Single(raceResults);
        }

        [Fact]
        public async Task LocalTime_DatePart_Minute()
        {
            var raceResults = await this.Db.Race.Where(r => r.ScheduledStartTime.Minute == 6).ToListAsync();
            Assert.Equal(
                condense(@$"{RaceSelectStatement} WHERE DATEPART(minute, [r].[ScheduledStartTime]) = 6"),
                condense(this.Db.Sql));

            Assert.Single(raceResults);
        }

        [Fact]
        public async Task LocalTime_DatePart_Second()
        {
            var raceResults = await this.Db.Race.Where(r => r.ScheduledStartTime.Second == 6).ToListAsync();
            Assert.Equal(
                 condense(@$"{RaceSelectStatement} WHERE DATEPART(second, [r].[ScheduledStartTime]) = 6"),
                 condense(this.Db.Sql));

            Assert.Single(raceResults);
        }

        [Fact]
        public async Task LocalTime_DatePart_Millisecond()
        {
            var raceResults = await this.Db.Race.Where(r => r.ScheduledStartTime.Millisecond == 6).ToListAsync();
            Assert.Equal(
                condense(@$"{RaceSelectStatement} WHERE DATEPART(millisecond, [r].[ScheduledStartTime]) = 6"),
                condense(this.Db.Sql));

            Assert.Single(raceResults);
        }

        [Fact]
        public async Task LocalTime_DatePart_Microsecond()
        {
            var raceResults = await this.Db.Race.Where(r => r.ScheduledStartTime.Microsecond() == 1000).ToListAsync();
            Assert.Equal(
                 condense(@$"{RaceSelectStatement} WHERE DATEPART(microsecond, [r].[ScheduledStartTime]) = 1000"),
                 condense(this.Db.Sql));

            Assert.Single(raceResults);
        }

        [Fact]
        public async Task LocalTime_DatePart_Nanosecond()
        {
            var raceResults = await this.Db.Race.Where(r => r.ScheduledStartTime.NanosecondOfSecond == 1000000).ToListAsync();
            Assert.Equal(
                condense(@$"{RaceSelectStatement} WHERE DATEPART(nanosecond, [r].[ScheduledStartTime]) = 1000000"),
                condense(this.Db.Sql));

            Assert.Single(raceResults);
        }
    }
}
