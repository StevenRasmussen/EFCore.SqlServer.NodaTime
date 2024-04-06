using Microsoft.EntityFrameworkCore.SqlServer.Storage;
using NodaTime;
using System;
using Xunit;

namespace SimplerSoftware.EntityFrameworkCore.SqlServer.NodaTime.Tests
{
    public class MappingDefaultsTest
    {
        [Fact]
        public void Duration_Defaults()
        {
            var durationDefault = DurationTypeMapping.Default;

            Assert.NotNull(durationDefault);

            Assert.Equal("time", durationDefault.StoreType);
            Assert.Equal("'02:30:01'", durationDefault.GenerateSqlLiteral(Duration.FromTimeSpan(new TimeSpan(2, 30, 1))));
        }

        [Fact]
        public void Instant_Defaults()
        {
            var instantDefault = InstantTypeMapping.Default;

            Assert.NotNull(instantDefault);

            Assert.Equal("datetime2", instantDefault.StoreType);
            Assert.Equal("'2024-02-14T00:14:00.0000000Z'", instantDefault.GenerateSqlLiteral(Instant.FromUtc(2024, 02, 14, 0, 14)));
        }

        [Fact]
        public void LocalDateTime_Defaults()
        {
            var localDateTimeDefault = LocalDateTimeTypeMapping.Default;

            Assert.NotNull(localDateTimeDefault);

            Assert.Equal("datetime2", localDateTimeDefault.StoreType);
            Assert.Equal("'2024-04-01T13:15:00.0000000'", localDateTimeDefault.GenerateSqlLiteral(new LocalDateTime(2024, 04, 01, 13, 15)));
        }

        [Fact]
        public void LocalDate_Defaults()
        {
            var localDateDefault = LocalDateTypeMapping.Default;

            Assert.NotNull(localDateDefault);

            Assert.Equal("date", localDateDefault.StoreType);
            Assert.Equal("'2024-03-30'", localDateDefault.GenerateSqlLiteral(new LocalDate(2024, 03, 30)));
        }

        [Fact]
        public void LocalTime_Defaults()
        {
            var localTimeDefault = LocalTimeTypeMapping.Default;

            Assert.NotNull(localTimeDefault);

            Assert.Equal("time", localTimeDefault.StoreType);
            Assert.Equal("'19:40:00'", localTimeDefault.GenerateSqlLiteral(new LocalTime(19, 40)));
        }

        [Fact]
        public void OffsetDateTime_Defaults()
        {
            var offsetDateTimeDefault = OffsetDateTimeTypeMapping.Default;

            Assert.NotNull(offsetDateTimeDefault);

            Assert.Equal("datetimeoffset", offsetDateTimeDefault.StoreType);
            var localDateTime = new LocalDateTime(2024, 04, 01, 17, 15);
            Assert.Equal("'2024-04-01T17:15:00.0000000+02:00'", offsetDateTimeDefault.GenerateSqlLiteral(new OffsetDateTime(localDateTime, Offset.FromHours(2))));
        }
    }
}
