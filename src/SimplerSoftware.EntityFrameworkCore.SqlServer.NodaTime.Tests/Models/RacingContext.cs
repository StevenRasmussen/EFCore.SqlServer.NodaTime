﻿using System;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.NodaTime.Extensions;

using NodaTime;

using SimplerSoftware.EntityFrameworkCore.SqlServer.NodaTime.Tests.Logging;

namespace SimplerSoftware.EntityFrameworkCore.SqlServer.NodaTime.Tests.Models
{
    public class RacingContext : DbContext
    {
        public RacingContext(DbContextOptions options)
            : base(options)
        {
        }

        private readonly TestLoggerFactory _loggerFactory
            = new TestLoggerFactory();

        public DbSet<SupportedNodaTypes> TypeResults { get; set; }

        public DbSet<LessPreciseRaceResult> LessPreciseRaceResults { get; set; }

        public DbSet<Race> Race { get; set; }

        public DbSet<RaceResult> RaceResult { get; set; }

        public DbSet<RaceSplit> RaceSplit { get; set; }

        public string Sql
            => _loggerFactory.Logger.Sql;

        protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseLoggerFactory(_loggerFactory);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Race>()
                .HasData(
                    new Race { Id = 1, Date = new LocalDate(2019, 1, 1), DateTimeDate = new DateTime(2019, 1, 1), ScheduledStartTime = new LocalTime(8, 0, 0, 0), ScheduledDuration = DurationExtensions.FromParts(1, 0, 0, 0), ScheduledStart = new LocalDateTime(2019, 1, 1, 8, 0, 0, 0) },
                    new Race { Id = 2, Date = new LocalDate(2019, 2, 1), DateTimeDate = new DateTime(2019, 2, 1), ScheduledStartTime = new LocalTime(9, 1, 1, 1), ScheduledDuration = DurationExtensions.FromParts(2, 1, 1, 1), ScheduledStart = new LocalDateTime(2019, 2, 1, 9, 1, 1, 1) },
                    new Race { Id = 3, Date = new LocalDate(2019, 3, 1), DateTimeDate = new DateTime(2019, 3, 1), ScheduledStartTime = new LocalTime(10, 2, 2, 2), ScheduledDuration = DurationExtensions.FromParts(3, 2, 2, 2), ScheduledStart = new LocalDateTime(2019, 3, 1, 10, 2, 2, 2) },
                    new Race { Id = 4, Date = new LocalDate(2019, 4, 1), DateTimeDate = new DateTime(2019, 4, 1), ScheduledStartTime = new LocalTime(11, 3, 3, 3), ScheduledDuration = DurationExtensions.FromParts(4, 3, 3, 3), ScheduledStart = new LocalDateTime(2019, 4, 1, 11, 3, 3, 3) },
                    new Race { Id = 5, Date = new LocalDate(2019, 5, 1), DateTimeDate = new DateTime(2019, 5, 1), ScheduledStartTime = new LocalTime(12, 4, 4, 4), ScheduledDuration = DurationExtensions.FromParts(5, 4, 4, 4), ScheduledStart = new LocalDateTime(2019, 5, 1, 12, 4, 4, 4) },
                    new Race { Id = 6, Date = new LocalDate(2019, 6, 1), DateTimeDate = new DateTime(2019, 6, 1), ScheduledStartTime = new LocalTime(13, 5, 5, 5), ScheduledDuration = DurationExtensions.FromParts(6, 5, 5, 5), ScheduledStart = new LocalDateTime(2019, 6, 1, 13, 5, 5, 5) },
                    new Race { Id = 7, Date = new LocalDate(2019, 7, 1), DateTimeDate = new DateTime(2019, 7, 1), ScheduledStartTime = new LocalTime(14, 6, 6, 6), ScheduledDuration = DurationExtensions.FromParts(7, 6, 6, 6), ScheduledStart = new LocalDateTime(2019, 7, 1, 14, 6, 6, 6) },
                    new Race { Id = 8, Date = new LocalDate(2019, 8, 1), DateTimeDate = new DateTime(2019, 8, 1), ScheduledStartTime = new LocalTime(15, 7, 7, 7), ScheduledDuration = DurationExtensions.FromParts(8, 7, 7, 7), ScheduledStart = new LocalDateTime(2019, 8, 1, 15, 7, 7, 7) },
                    new Race { Id = 9, Date = new LocalDate(2019, 9, 1), DateTimeDate = new DateTime(2019, 9, 1), ScheduledStartTime = new LocalTime(16, 8, 8, 8), ScheduledDuration = DurationExtensions.FromParts(9, 8, 8, 8), ScheduledStart = new LocalDateTime(2019, 9, 1, 16, 8, 8, 8) },
                    new Race { Id = 10, Date = new LocalDate(2019, 10, 1), DateTimeDate = new DateTime(2019, 10, 1), ScheduledStartTime = new LocalTime(17, 9, 9, 9), ScheduledDuration = DurationExtensions.FromParts(10, 9, 9, 9), ScheduledStart = new LocalDateTime(2019, 10, 1, 17, 9, 9, 9) },
                    new Race { Id = 11, Date = new LocalDate(2019, 11, 1), DateTimeDate = new DateTime(2019, 11, 1), ScheduledStartTime = new LocalTime(18, 10, 10, 10), ScheduledDuration = DurationExtensions.FromParts(11, 10, 10, 10), ScheduledStart = new LocalDateTime(2019, 11, 1, 18, 10, 10, 10) },
                    new Race { Id = 12, Date = new LocalDate(2019, 12, 1), DateTimeDate = new DateTime(2019, 12, 1), ScheduledStartTime = new LocalTime(19, 11, 11, 11), ScheduledDuration = DurationExtensions.FromParts(12, 11, 11, 11), ScheduledStart = new LocalDateTime(2019, 12, 1, 19, 11, 11, 11) }
                );


            modelBuilder.Entity<RaceResult>()
                .HasData(
                    new RaceResult { Id = 1, StartTime = Instant.FromUtc(2019, 1, 1, 8, 0), EndTime = Instant.FromUtc(2019, 1, 1, 9, 0), StartTimeOffset = OffsetDateTime.FromDateTimeOffset(new DateTimeOffset(new DateTime(2019, 1, 1, 8, 0, 0), TimeSpan.FromHours(5))), OffsetFromWinner = DurationExtensions.FromParts(0, 0, 0, 0, 0, 0) },
                    new RaceResult { Id = 2, StartTime = Instant.FromUtc(2019, 2, 1, 9, 0), EndTime = Instant.FromUtc(2019, 2, 1, 10, 0), StartTimeOffset = OffsetDateTime.FromDateTimeOffset(new DateTimeOffset(new DateTime(2019, 2, 1, 9, 0, 0), TimeSpan.FromHours(5))), OffsetFromWinner = DurationExtensions.FromParts(0, 0, 0, 1, 0, 0) },
                    new RaceResult { Id = 3, StartTime = Instant.FromUtc(2019, 3, 1, 10, 0), EndTime = Instant.FromUtc(2019, 3, 1, 11, 0), StartTimeOffset = OffsetDateTime.FromDateTimeOffset(new DateTimeOffset(new DateTime(2019, 3, 1, 10, 0, 0), TimeSpan.FromHours(5))), OffsetFromWinner = DurationExtensions.FromParts(0, 0, 0, 2, 0, 0) },
                    new RaceResult { Id = 4, StartTime = Instant.FromUtc(2019, 4, 1, 11, 0), EndTime = Instant.FromUtc(2019, 4, 1, 12, 0), StartTimeOffset = OffsetDateTime.FromDateTimeOffset(new DateTimeOffset(new DateTime(2019, 4, 1, 11, 0, 0), TimeSpan.FromHours(5))), OffsetFromWinner = DurationExtensions.FromParts(0, 0, 0, 3, 0, 0) },
                    new RaceResult { Id = 5, StartTime = Instant.FromUtc(2019, 5, 1, 12, 0), EndTime = Instant.FromUtc(2019, 5, 1, 13, 0), StartTimeOffset = OffsetDateTime.FromDateTimeOffset(new DateTimeOffset(new DateTime(2019, 5, 1, 12, 0, 0), TimeSpan.FromHours(5))), OffsetFromWinner = DurationExtensions.FromParts(0, 0, 0, 4, 0, 0) },
                    new RaceResult { Id = 6, StartTime = Instant.FromUtc(2019, 6, 1, 13, 0), EndTime = Instant.FromUtc(2019, 6, 1, 14, 0), StartTimeOffset = OffsetDateTime.FromDateTimeOffset(new DateTimeOffset(new DateTime(2019, 6, 1, 13, 0, 0), TimeSpan.FromHours(5))), OffsetFromWinner = DurationExtensions.FromParts(0, 0, 0, 5, 0, 0) },
                    new RaceResult { Id = 7, StartTime = Instant.FromUtc(2019, 7, 1, 14, 0), EndTime = Instant.FromUtc(2019, 7, 1, 15, 0), StartTimeOffset = OffsetDateTime.FromDateTimeOffset(new DateTimeOffset(new DateTime(2019, 7, 1, 14, 0, 0), TimeSpan.FromHours(5))), OffsetFromWinner = DurationExtensions.FromParts(0, 0, 0, 6, 0, 0) },
                    new RaceResult { Id = 8, StartTime = Instant.FromUtc(2019, 8, 1, 15, 0), EndTime = Instant.FromUtc(2019, 8, 1, 16, 0), StartTimeOffset = OffsetDateTime.FromDateTimeOffset(new DateTimeOffset(new DateTime(2019, 8, 1, 15, 0, 0), TimeSpan.FromHours(5))), OffsetFromWinner = DurationExtensions.FromParts(0, 0, 0, 7, 0, 0) },
                    new RaceResult { Id = 9, StartTime = Instant.FromUtc(2019, 9, 1, 16, 0), EndTime = Instant.FromUtc(2019, 9, 1, 17, 0), StartTimeOffset = OffsetDateTime.FromDateTimeOffset(new DateTimeOffset(new DateTime(2019, 9, 1, 16, 0, 0), TimeSpan.FromHours(5))), OffsetFromWinner = DurationExtensions.FromParts(0, 0, 0, 8, 0, 0) },
                    new RaceResult { Id = 10, StartTime = Instant.FromUtc(2019, 10, 1, 17, 0), EndTime = Instant.FromUtc(2019, 10, 1, 18, 0), StartTimeOffset = OffsetDateTime.FromDateTimeOffset(new DateTimeOffset(new DateTime(2019, 10, 1, 17, 0, 0), TimeSpan.FromHours(5))), OffsetFromWinner = DurationExtensions.FromParts(0, 0, 0, 9, 0, 0) },
                    new RaceResult { Id = 11, StartTime = Instant.FromUtc(2019, 11, 1, 18, 0), EndTime = Instant.FromUtc(2019, 11, 1, 19, 0), StartTimeOffset = OffsetDateTime.FromDateTimeOffset(new DateTimeOffset(new DateTime(2019, 11, 1, 18, 0, 0), TimeSpan.FromHours(5))), OffsetFromWinner = DurationExtensions.FromParts(0, 0, 0, 10, 0, 0) },
                    new RaceResult { Id = 12, StartTime = Instant.FromUtc(2019, 12, 1, 19, 0), EndTime = Instant.FromUtc(2019, 12, 1, 20, 0), StartTimeOffset = OffsetDateTime.FromDateTimeOffset(new DateTimeOffset(new DateTime(2019, 12, 1, 19, 0, 0), TimeSpan.FromHours(5))), OffsetFromWinner = DurationExtensions.FromParts(0, 0, 0, 11, 0, 0) });

            modelBuilder.Entity<RaceSplit>()
                .HasData(
                    new RaceSplit { Id = 1, TimeStampInstant = InstantExtensions.FromParts(2020, 1, 1, 1, 1, 1, 1, 1, 1), TimeStampLocalDateTime = LocalDateTimeExtensions.FromParts(2020, 1, 1, 1, 1, 1, 1, 1, 1), TimeStampLocalTime = LocalTimeExtensions.FromParts(1, 1, 1, 1, 1, 100), TimeStampOffsetDateTime = OffsetDateTimeExtensions.FromParts(2020, 1, 1, 1, 1, 1, 1, 1, 100, 300) },
                    new RaceSplit { Id = 2, TimeStampInstant = InstantExtensions.FromParts(2020, 1, 1, 2, 2, 2, 2, 2, 2), TimeStampLocalDateTime = LocalDateTimeExtensions.FromParts(2020, 1, 1, 2, 2, 2, 2, 2, 2), TimeStampLocalTime = LocalTimeExtensions.FromParts(2, 2, 2, 2, 1, 100), TimeStampOffsetDateTime = OffsetDateTimeExtensions.FromParts(2020, 1, 1, 2, 2, 2, 2, 2, 200, 300) },
                    new RaceSplit { Id = 3, TimeStampInstant = InstantExtensions.FromParts(2020, 1, 1, 3, 3, 3, 3, 3, 3), TimeStampLocalDateTime = LocalDateTimeExtensions.FromParts(2020, 1, 1, 3, 3, 3, 3, 3, 3), TimeStampLocalTime = LocalTimeExtensions.FromParts(3, 3, 3, 3, 3, 300), TimeStampOffsetDateTime = OffsetDateTimeExtensions.FromParts(2020, 1, 1, 3, 3, 3, 3, 3, 300, 300) },
                    new RaceSplit { Id = 4, TimeStampInstant = InstantExtensions.FromParts(2020, 1, 1, 4, 4, 4, 4, 4, 4), TimeStampLocalDateTime = LocalDateTimeExtensions.FromParts(2020, 1, 1, 4, 4, 4, 4, 4, 4), TimeStampLocalTime = LocalTimeExtensions.FromParts(4, 4, 4, 4, 4, 400), TimeStampOffsetDateTime = OffsetDateTimeExtensions.FromParts(2020, 1, 1, 4, 4, 4, 4, 4, 400, 300) },
                    new RaceSplit { Id = 5, TimeStampInstant = InstantExtensions.FromParts(2020, 1, 1, 5, 5, 5, 5, 5, 5), TimeStampLocalDateTime = LocalDateTimeExtensions.FromParts(2020, 1, 1, 5, 5, 5, 5, 5, 5), TimeStampLocalTime = LocalTimeExtensions.FromParts(5, 5, 5, 5, 5, 500), TimeStampOffsetDateTime = OffsetDateTimeExtensions.FromParts(2020, 1, 1, 5, 5, 5, 5, 5, 500, 300) },
                    new RaceSplit { Id = 6, TimeStampInstant = InstantExtensions.FromParts(2020, 1, 1, 6, 6, 6, 6, 6, 6), TimeStampLocalDateTime = LocalDateTimeExtensions.FromParts(2020, 1, 1, 6, 6, 6, 6, 6, 6), TimeStampLocalTime = LocalTimeExtensions.FromParts(6, 6, 6, 6, 6, 600), TimeStampOffsetDateTime = OffsetDateTimeExtensions.FromParts(2020, 1, 1, 6, 6, 6, 6, 6, 600, 300) },
                    new RaceSplit { Id = 7, TimeStampInstant = InstantExtensions.FromParts(2020, 1, 1, 7, 7, 7, 7, 7, 7), TimeStampLocalDateTime = LocalDateTimeExtensions.FromParts(2020, 1, 1, 7, 7, 7, 7, 7, 7), TimeStampLocalTime = LocalTimeExtensions.FromParts(7, 7, 7, 7, 7, 700), TimeStampOffsetDateTime = OffsetDateTimeExtensions.FromParts(2020, 1, 1, 7, 7, 7, 7, 7, 700, 300) },
                    new RaceSplit { Id = 8, TimeStampInstant = InstantExtensions.FromParts(2020, 1, 1, 8, 8, 8, 8, 8, 8), TimeStampLocalDateTime = LocalDateTimeExtensions.FromParts(2020, 1, 1, 8, 8, 8, 8, 8, 8), TimeStampLocalTime = LocalTimeExtensions.FromParts(8, 8, 8, 8, 8, 800), TimeStampOffsetDateTime = OffsetDateTimeExtensions.FromParts(2020, 1, 1, 8, 8, 8, 8, 8, 800, 300) },
                    new RaceSplit { Id = 9, TimeStampInstant = InstantExtensions.FromParts(2020, 1, 1, 9, 9, 9, 9, 9, 9), TimeStampLocalDateTime = LocalDateTimeExtensions.FromParts(2020, 1, 1, 9, 9, 9, 9, 9, 9), TimeStampLocalTime = LocalTimeExtensions.FromParts(9, 9, 9, 9, 9, 900), TimeStampOffsetDateTime = OffsetDateTimeExtensions.FromParts(2020, 1, 1, 9, 9, 9, 9, 9, 900, 300) }
                );
        }
    }
}
