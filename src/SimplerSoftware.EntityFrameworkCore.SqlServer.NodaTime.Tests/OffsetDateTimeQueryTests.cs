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
    public class OffsetDateTimeQueryTests : QueryTestBase
    {
        public OffsetDateTimeQueryTests(DatabaseTestFixture databaseTestFixture)
            : base(databaseTestFixture) { }

        [Fact]
        public async Task OffsetDateTime_can_be_used_in_query()
        {
            var raceResults = await this.Db.RaceResult.Where(r => r.StartTimeOffset == GetOffsetDateTimeFromParts(2019, 7, 1, 14, 0, 0, 0, 5)).ToListAsync();

            Assert.Equal(
                condense(@$"{RaceResultSelectStatement} WHERE [r].[StartTimeOffset] = '2019-07-01T14:00:00.0000000+05:00'"),
                condense(this.Db.Sql));

            Assert.Single(raceResults);
        }

        [Fact]
        public async Task OffsetDateTime_PlusYears()
        {
            var raceResults = await this.Db.RaceResult.Where(r => r.StartTimeOffset.PlusYears(1) == GetOffsetDateTimeFromParts(2020, 7, 1, 14, 0, 0, 0, 5)).ToListAsync();

            Assert.Equal(
                condense(@$"{RaceResultSelectStatement} WHERE DATEADD(year, CAST(1 AS int), [r].[StartTimeOffset]) = '2020-07-01T14:00:00.0000000+05:00'"),
                condense(this.Db.Sql));

            Assert.Single(raceResults);
        }

        [Fact]
        public async Task OffsetDateTime_PlusMonths()
        {
            var raceResults = await this.Db.RaceResult.Where(r => r.StartTimeOffset.PlusMonths(1) == GetOffsetDateTimeFromParts(2019, 7, 1, 13, 0, 0, 0, 5)).ToListAsync();

            Assert.Equal(
                condense(@$"{RaceResultSelectStatement} WHERE DATEADD(month, CAST(1 AS int), [r].[StartTimeOffset]) = '2019-07-01T13:00:00.0000000+05:00'"),
                condense(this.Db.Sql));

            Assert.Single(raceResults);
        }

        [Fact]
        public async Task OffsetDateTime_PlusDays()
        {
            var raceResults = await this.Db.RaceResult.Where(r => r.StartTimeOffset.PlusDays(30) == GetOffsetDateTimeFromParts(2019, 7, 1, 13, 0, 0, 0, 5)).ToListAsync();

            Assert.Equal(
                condense(@$"{RaceResultSelectStatement} WHERE DATEADD(day, CAST(30.0E0 AS int), [r].[StartTimeOffset]) = '2019-07-01T13:00:00.0000000+05:00'"),
                condense(this.Db.Sql));

            Assert.Single(raceResults);
        }

        [Fact]
        public async Task OffsetDateTime_PlusHours()
        {
            var raceResults = await this.Db.RaceResult.Where(r => r.StartTimeOffset.PlusHours(1) == GetOffsetDateTimeFromParts(2019, 7, 1, 15, 0, 0, 0, 5)).ToListAsync();

            Assert.Equal(
                 condense(@$"{RaceResultSelectStatement} WHERE DATEADD(hour, CAST(1 AS int), [r].[StartTimeOffset]) = '2019-07-01T15:00:00.0000000+05:00'"),
                 condense(this.Db.Sql));

            Assert.Single(raceResults);
        }

        [Fact]
        public async Task OffsetDateTime_PlusMinutes()
        {
            var raceResults = await this.Db.RaceResult.Where(r => r.StartTimeOffset.PlusMinutes(60) == GetOffsetDateTimeFromParts(2019, 7, 1, 15, 0, 0, 0, 5)).ToListAsync();

            Assert.Equal(
                condense(@$"{RaceResultSelectStatement} WHERE DATEADD(minute, CAST(60 AS int), [r].[StartTimeOffset]) = '2019-07-01T15:00:00.0000000+05:00'"),
                condense(this.Db.Sql));

            Assert.Single(raceResults);
        }

        [Fact]
        public async Task OffsetDateTime_PlusSeconds()
        {
            var raceResults = await this.Db.RaceResult.Where(r => r.StartTimeOffset.PlusSeconds(3600) == GetOffsetDateTimeFromParts(2019, 7, 1, 15, 0, 0, 0, 5)).ToListAsync();

            Assert.Equal(
                condense(@$"{RaceResultSelectStatement} WHERE DATEADD(second, CAST(CAST(3600 AS bigint) AS int), [r].[StartTimeOffset]) = '2019-07-01T15:00:00.0000000+05:00'"),
                condense(this.Db.Sql));

            Assert.Single(raceResults);
        }

        [Fact]
        public async Task OffsetDateTime_PlusMilliseconds()
        {
            var raceResults = await this.Db.RaceResult.Where(r => r.StartTimeOffset.PlusMilliseconds(3600000) == GetOffsetDateTimeFromParts(2019, 7, 1, 15, 0, 0, 0, 5)).ToListAsync();

            Assert.Equal(
                condense(@$"{RaceResultSelectStatement} WHERE DATEADD(millisecond, CAST(CAST(3600000 AS bigint) AS int), [r].[StartTimeOffset]) = '2019-07-01T15:00:00.0000000+05:00'"),
                condense(this.Db.Sql));

            Assert.Single(raceResults);
        }

        [Fact]
        public async Task OffsetDateTime_DatePart_Year()
        {
            var raceResults = await this.Db.RaceResult.Where(r => r.StartTimeOffset.Year == 2019).ToListAsync();

            Assert.Equal(
                condense(@$"{RaceResultSelectStatement} WHERE DATEPART(year, [r].[StartTimeOffset]) = 2019"),
                condense(this.Db.Sql));

            Assert.Equal(12, raceResults.Count);
        }

        [Fact]
        public async Task OffsetDateTime_DatePart_Quarter()
        {
            var raceResults = await this.Db.RaceResult.Where(r => r.StartTimeOffset.Quarter() == 4).ToListAsync();

            Assert.Equal(
                condense(@$"{RaceResultSelectStatement} WHERE DATEPART(quarter, [r].[StartTimeOffset]) = 4"),
                condense(this.Db.Sql));

            Assert.Equal(3, raceResults.Count);
        }

        [Fact]
        public async Task OffsetDateTime_DatePart_Month()
        {
            var raceResults = await this.Db.RaceResult.Where(r => r.StartTimeOffset.Month == 12).ToListAsync();

            Assert.Equal(
                 condense(@$"{RaceResultSelectStatement} WHERE DATEPART(month, [r].[StartTimeOffset]) = 12"),
                 condense(this.Db.Sql));

            Assert.Single(raceResults);
        }

        [Fact]
        public async Task OffsetDateTime_DatePart_DayOfYear()
        {
            var raceResults = await this.Db.RaceResult.Where(r => r.StartTimeOffset.DayOfYear == 1).ToListAsync();

            Assert.Equal(
                condense(@$"{RaceResultSelectStatement} WHERE DATEPART(dayofyear, [r].[StartTimeOffset]) = 1"),
                condense(this.Db.Sql));

            Assert.Single(raceResults);
        }

        [Fact]
        public async Task OffsetDateTime_DatePart_Day()
        {
            var raceResults = await this.Db.RaceResult.Where(r => r.StartTimeOffset.Day == 1).ToListAsync();

            Assert.Equal(
                condense(@$"{RaceResultSelectStatement} WHERE DATEPART(day, [r].[StartTimeOffset]) = 1"),
                condense(this.Db.Sql));

            Assert.Equal(12, raceResults.Count);
        }

        [Fact]
        public async Task OffsetDateTime_DatePart_Week()
        {
            var raceResults = await this.Db.RaceResult.Where(r => r.StartTimeOffset.Week() == 1).ToListAsync();

            Assert.Equal(
                condense(@$"{RaceResultSelectStatement} WHERE DATEPART(week, [r].[StartTimeOffset]) = 1"),
                condense(this.Db.Sql));

            Assert.Single(raceResults);
        }

        [Fact]
        public async Task OffsetDateTime_DatePart_WeekDay()
        {
            var raceResults = await this.Db.RaceResult.Where(r => r.StartTimeOffset.WeekDay() == 1).ToListAsync();

            Assert.Equal(
                condense(@$"{RaceResultSelectStatement} WHERE DATEPART(weekday, [r].[StartTimeOffset]) = 1"),
                condense(this.Db.Sql));

            Assert.Equal(2, raceResults.Count);
        }

        [Fact]
        public async Task OffsetDateTime_DatePart_Hour()
        {
            var raceResults = await this.Db.RaceResult.Where(r => r.StartTimeOffset.Hour == 12).ToListAsync();

            Assert.Equal(
                condense(@$"{RaceResultSelectStatement} WHERE DATEPART(hour, [r].[StartTimeOffset]) = 12"),
                condense(this.Db.Sql));

            Assert.Single(raceResults);
        }

        [Fact]
        public async Task OffsetDateTime_DatePart_Minute()
        {
            var raceResults = await this.Db.RaceResult.Where(r => r.StartTimeOffset.Minute == 0).ToListAsync();

            Assert.Equal(
                 condense(@$"{RaceResultSelectStatement} WHERE DATEPART(minute, [r].[StartTimeOffset]) = 0"),
                 condense(this.Db.Sql));

            Assert.Equal(12, raceResults.Count);
        }

        [Fact]
        public async Task OffsetDateTime_DatePart_Second()
        {
            var raceResults = await this.Db.RaceResult.Where(r => r.StartTimeOffset.Second == 0).ToListAsync();

            Assert.Equal(
                condense(@$"{RaceResultSelectStatement} WHERE DATEPART(second, [r].[StartTimeOffset]) = 0"),
                condense(this.Db.Sql));

            Assert.Equal(12, raceResults.Count);
        }

        [Fact]
        public async Task OffsetDateTime_DatePart_Millisecond()
        {
            var raceResults = await this.Db.RaceResult.Where(r => r.StartTimeOffset.Millisecond == 0).ToListAsync();

            Assert.Equal(
                condense(@$"{RaceResultSelectStatement} WHERE DATEPART(millisecond, [r].[StartTimeOffset]) = 0"),
                condense(this.Db.Sql));

            Assert.Equal(12, raceResults.Count);
        }

        [Fact]
        public async Task OffsetDateTime_DatePart_Microsecond()
        {
            var raceResults = await this.Db.RaceResult.Where(r => r.StartTimeOffset.Microsecond() == 0).ToListAsync();

            Assert.Equal(
                condense(@$"{RaceResultSelectStatement} WHERE DATEPART(microsecond, [r].[StartTimeOffset]) = 0"),
                condense(this.Db.Sql));

            Assert.Equal(12, raceResults.Count);
        }

        [Fact]
        public async Task OffsetDateTime_DatePart_Nanosecond()
        {
            var raceResults = await this.Db.RaceResult.Where(r => r.StartTimeOffset.NanosecondOfSecond == 0).ToListAsync();

            Assert.Equal(
                condense(@$"{RaceResultSelectStatement} WHERE DATEPART(nanosecond, [r].[StartTimeOffset]) = 0"),
                condense(this.Db.Sql));

            Assert.Equal(12, raceResults.Count);
        }

        [Fact]
        public async Task OffsetDateTime_DatePart_TzOffset()
        {
            var raceResults = await this.Db.RaceResult.Where(r => r.StartTimeOffset.TzOffset() == 300).ToListAsync();

            Assert.Equal(
                condense(@$"{RaceResultSelectStatement} WHERE DATEPART(tzoffset, [r].[StartTimeOffset]) = 300"),
                condense(this.Db.Sql));

            Assert.Equal(12, raceResults.Count);
        }

        [Fact]
        public async Task OffsetDateTime_DatePart_IsoWeek()
        {
            var raceResults = await this.Db.RaceResult.Where(r => r.StartTimeOffset.IsoWeek() == 1).ToListAsync();

            Assert.Equal(
                condense(@$"{RaceResultSelectStatement} WHERE DATEPART(iso_week, [r].[StartTimeOffset]) = 1"),
                condense(this.Db.Sql));

            Assert.Single(raceResults);
        }

        private OffsetDateTime GetOffsetDateTimeFromParts(int year, int month, int day, int hour, int minute, int second, int millisecond, int OffsetInHours)
        {
            return OffsetDateTime.FromDateTimeOffset(new DateTimeOffset(new DateTime(year, month, day, hour, minute, second, millisecond), TimeSpan.FromHours(OffsetInHours)));
        }
    }
}
