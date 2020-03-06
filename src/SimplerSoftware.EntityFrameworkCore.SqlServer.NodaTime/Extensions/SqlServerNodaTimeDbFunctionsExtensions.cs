using Microsoft.EntityFrameworkCore;
using NodaTime;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.EntityFrameworkCore.SqlServer.NodaTime.Extensions
{
    public static class SqlServerNodaTimeDbFunctionsExtensions
    {
        // Instant Functions
        public static int DateDiffYear(this DbFunctions _, Instant startDate, Instant endDate)
            => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");

        public static int? DateDiffYear(this DbFunctions _, Instant? startDate, Instant? endDate)
            => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");

        public static int DateDiffMonth(this DbFunctions _, Instant startDate, Instant endDate)
            => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");

        public static int? DateDiffMonth(this DbFunctions _, Instant? startDate, Instant? endDate)
            => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");

        public static int DateDiffWeek(this DbFunctions _, Instant startDate, Instant endDate)
           => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");

        public static int? DateDiffWeek(this DbFunctions _, Instant? startDate, Instant? endDate)
            => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");

        public static int DateDiffDay(this DbFunctions _, Instant startDate, Instant endDate)
            => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");

        public static int? DateDiffDay(this DbFunctions _, Instant? startDate, Instant? endDate)
            => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");

        public static int DateDiffHour(this DbFunctions _, Instant startDate, Instant endDate)
            => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");

        public static int? DateDiffHour(this DbFunctions _, Instant? startDate, Instant? endDate)
            => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");

        public static int DateDiffMinute(this DbFunctions _, Instant startDate, Instant endDate)
            => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");

        public static int? DateDiffMinute(this DbFunctions _, Instant? startDate, Instant? endDate)
            => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");

        public static int DateDiffSecond(this DbFunctions _, Instant startDate, Instant endDate)
            => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");

        public static int? DateDiffSecond(this DbFunctions _, Instant? startDate, Instant? endDate)
            => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");

        public static int DateDiffMillisecond(this DbFunctions _, Instant startDate, Instant endDate)
            => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");

        public static int? DateDiffMillisecond(this DbFunctions _, Instant? startDate, Instant? endDate)
            => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");

        public static int DateDiffMicrosecond(this DbFunctions _, Instant startDate, Instant endDate)
           => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");

        public static int? DateDiffMicrosecond(this DbFunctions _, Instant? startDate, Instant? endDate)
            => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");

        public static int DateDiffNanosecond(this DbFunctions _, Instant startDate, Instant endDate)
           => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");

        public static int? DateDiffNanosecond(this DbFunctions _, Instant? startDate, Instant? endDate)
            => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");

        public static long DateDiffSecondBig(this DbFunctions _, Instant startDate, Instant endDate)
            => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");

        public static long? DateDiffSecondBig(this DbFunctions _, Instant? startDate, Instant? endDate)
            => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");

        public static long DateDiffMillisecondBig(this DbFunctions _, Instant startDate, Instant endDate)
           => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");

        public static long? DateDiffMillisecondBig(this DbFunctions _, Instant? startDate, Instant? endDate)
            => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");

        public static long DateDiffMicrosecondBig(this DbFunctions _, Instant startDate, Instant endDate)
           => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");

        public static long? DateDiffMicrosecondBig(this DbFunctions _, Instant? startDate, Instant? endDate)
            => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");

        public static long DateDiffNanosecondBig(this DbFunctions _, Instant startDate, Instant endDate)
           => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");

        public static long? DateDiffNanosecondBig(this DbFunctions _, Instant? startDate, Instant? endDate)
            => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");

        // OffsetDateTime
        public static int DateDiffYear(this DbFunctions _, OffsetDateTime startDate, OffsetDateTime endDate)
            => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");

        public static int? DateDiffYear(this DbFunctions _, OffsetDateTime? startDate, OffsetDateTime? endDate)
            => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");

        public static int DateDiffMonth(this DbFunctions _, OffsetDateTime startDate, OffsetDateTime endDate)
            => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");

        public static int? DateDiffMonth(this DbFunctions _, OffsetDateTime? startDate, OffsetDateTime? endDate)
            => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");

        public static int DateDiffWeek(this DbFunctions _, OffsetDateTime startDate, OffsetDateTime endDate)
           => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");

        public static int? DateDiffWeek(this DbFunctions _, OffsetDateTime? startDate, OffsetDateTime? endDate)
            => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");

        public static int DateDiffDay(this DbFunctions _, OffsetDateTime startDate, OffsetDateTime endDate)
            => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");

        public static int? DateDiffDay(this DbFunctions _, OffsetDateTime? startDate, OffsetDateTime? endDate)
            => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");

        public static int DateDiffHour(this DbFunctions _, OffsetDateTime startDate, OffsetDateTime endDate)
            => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");

        public static int? DateDiffHour(this DbFunctions _, OffsetDateTime? startDate, OffsetDateTime? endDate)
            => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");

        public static int DateDiffMinute(this DbFunctions _, OffsetDateTime startDate, OffsetDateTime endDate)
            => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");

        public static int? DateDiffMinute(this DbFunctions _, OffsetDateTime? startDate, OffsetDateTime? endDate)
            => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");

        public static int DateDiffSecond(this DbFunctions _, OffsetDateTime startDate, OffsetDateTime endDate)
            => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");

        public static int? DateDiffSecond(this DbFunctions _, OffsetDateTime? startDate, OffsetDateTime? endDate)
            => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");

        public static int DateDiffMillisecond(this DbFunctions _, OffsetDateTime startDate, OffsetDateTime endDate)
            => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");

        public static int? DateDiffMillisecond(this DbFunctions _, OffsetDateTime? startDate, OffsetDateTime? endDate)
            => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");

        public static int DateDiffMicrosecond(this DbFunctions _, OffsetDateTime startDate, OffsetDateTime endDate)
           => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");

        public static int? DateDiffMicrosecond(this DbFunctions _, OffsetDateTime? startDate, OffsetDateTime? endDate)
            => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");

        public static int DateDiffNanosecond(this DbFunctions _, OffsetDateTime startDate, OffsetDateTime endDate)
           => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");

        public static int? DateDiffNanosecond(this DbFunctions _, OffsetDateTime? startDate, OffsetDateTime? endDate)
            => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");

        public static long DateDiffSecondBig(this DbFunctions _, OffsetDateTime startDate, OffsetDateTime endDate)
           => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");

        public static long? DateDiffSecondBig(this DbFunctions _, OffsetDateTime? startDate, OffsetDateTime? endDate)
            => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");

        public static long DateDiffMillisecondBig(this DbFunctions _, OffsetDateTime startDate, OffsetDateTime endDate)
           => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");

        public static long? DateDiffMillisecondBig(this DbFunctions _, OffsetDateTime? startDate, OffsetDateTime? endDate)
            => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");

        public static long DateDiffMicrosecondBig(this DbFunctions _, OffsetDateTime startDate, OffsetDateTime endDate)
           => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");

        public static long? DateDiffMicrosecondBig(this DbFunctions _, OffsetDateTime? startDate, OffsetDateTime? endDate)
            => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");

        public static long DateDiffNanosecondBig(this DbFunctions _, OffsetDateTime startDate, OffsetDateTime endDate)
           => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");

        public static long? DateDiffNanosecondBig(this DbFunctions _, OffsetDateTime? startDate, OffsetDateTime? endDate)
            => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");

        // Local Date Time
        public static int DateDiffYear(this DbFunctions _, LocalDateTime startDate, LocalDateTime endDate)
            => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");

        public static int? DateDiffYear(this DbFunctions _, LocalDateTime? startDate, LocalDateTime? endDate)
            => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");

        public static int DateDiffMonth(this DbFunctions _, LocalDateTime startDate, LocalDateTime endDate)
            => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");

        public static int? DateDiffMonth(this DbFunctions _, LocalDateTime? startDate, LocalDateTime? endDate)
            => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");

        public static int DateDiffWeek(this DbFunctions _, LocalDateTime startDate, LocalDateTime endDate)
          => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");

        public static int? DateDiffWeek(this DbFunctions _, LocalDateTime? startDate, LocalDateTime? endDate)
            => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");

        public static int DateDiffDay(this DbFunctions _, LocalDateTime startDate, LocalDateTime endDate)
            => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");

        public static int? DateDiffDay(this DbFunctions _, LocalDateTime? startDate, LocalDateTime? endDate)
            => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");

        public static int DateDiffHour(this DbFunctions _, LocalDateTime startDate, LocalDateTime endDate)
            => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");

        public static int? DateDiffHour(this DbFunctions _, LocalDateTime? startDate, LocalDateTime? endDate)
            => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");

        public static int DateDiffMinute(this DbFunctions _, LocalDateTime startDate, LocalDateTime endDate)
            => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");

        public static int? DateDiffMinute(this DbFunctions _, LocalDateTime? startDate, LocalDateTime? endDate)
            => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");

        public static int DateDiffSecond(this DbFunctions _, LocalDateTime startDate, LocalDateTime endDate)
            => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");

        public static int? DateDiffSecond(this DbFunctions _, LocalDateTime? startDate, LocalDateTime? endDate)
            => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");

        public static int DateDiffMillisecond(this DbFunctions _, LocalDateTime startDate, LocalDateTime endDate)
            => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");

        public static int? DateDiffMillisecond(this DbFunctions _, LocalDateTime? startDate, LocalDateTime? endDate)
            => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");

        public static int DateDiffMicrosecond(this DbFunctions _, LocalDateTime startDate, LocalDateTime endDate)
           => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");

        public static int? DateDiffMicrosecond(this DbFunctions _, LocalDateTime? startDate, LocalDateTime? endDate)
            => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");

        public static int DateDiffNanosecond(this DbFunctions _, LocalDateTime startDate, LocalDateTime endDate)
           => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");

        public static int? DateDiffNanosecond(this DbFunctions _, LocalDateTime? startDate, LocalDateTime? endDate)
            => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");

        public static long DateDiffSecondBig(this DbFunctions _, LocalDateTime startDate, LocalDateTime endDate)
           => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");

        public static long? DateDiffSecondBig(this DbFunctions _, LocalDateTime? startDate, LocalDateTime? endDate)
            => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");

        public static long DateDiffMillisecondBig(this DbFunctions _, LocalDateTime startDate, LocalDateTime endDate)
           => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");

        public static long? DateDiffMillisecondBig(this DbFunctions _, LocalDateTime? startDate, LocalDateTime? endDate)
            => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");

        public static long DateDiffMicrosecondBig(this DbFunctions _, LocalDateTime startDate, LocalDateTime endDate)
           => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");

        public static long? DateDiffMicrosecondBig(this DbFunctions _, LocalDateTime? startDate, LocalDateTime? endDate)
            => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");

        public static long DateDiffNanosecondBig(this DbFunctions _, LocalDateTime startDate, LocalDateTime endDate)
           => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");

        public static long? DateDiffNanosecondBig(this DbFunctions _, LocalDateTime? startDate, LocalDateTime? endDate)
            => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");

        //Local Date 
        public static int DateDiffYear(this DbFunctions _, LocalDate startDate, LocalDate endDate)
            => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");

        public static int? DateDiffYear(this DbFunctions _, LocalDate? startDate, LocalDate? endDate)
            => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");

        public static int DateDiffMonth(this DbFunctions _, LocalDate startDate, LocalDate endDate)
            => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");

        public static int? DateDiffMonth(this DbFunctions _, LocalDate? startDate, LocalDate? endDate)
            => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");

        public static int DateDiffDay(this DbFunctions _, LocalDate startDate, LocalDate endDate)
            => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");

        public static int? DateDiffDay(this DbFunctions _, LocalDate? startDate, LocalDate? endDate)
            => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");

        public static int DateDiffWeek(this DbFunctions _, LocalDate startDate, LocalDate endDate)
           => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");

        public static int? DateDiffWeek(this DbFunctions _, LocalDate? startDate, LocalDate? endDate)
            => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");

        // Local Time
        public static int DateDiffHour(this DbFunctions _, LocalTime startDate, LocalTime endDate)
            => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");

        public static int? DateDiffHour(this DbFunctions _, LocalTime? startDate, LocalTime? endDate)
            => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");

        public static int DateDiffMinute(this DbFunctions _, LocalTime startDate, LocalTime endDate)
            => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");

        public static int? DateDiffMinute(this DbFunctions _, LocalTime? startDate, LocalTime? endDate)
            => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");

        public static int DateDiffSecond(this DbFunctions _, LocalTime startDate, LocalTime endDate)
            => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");

        public static int? DateDiffSecond(this DbFunctions _, LocalTime? startDate, LocalTime? endDate)
            => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");

        public static int DateDiffMillisecond(this DbFunctions _, LocalTime startDate, LocalTime endDate)
            => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");

        public static int? DateDiffMillisecond(this DbFunctions _, LocalTime? startDate, LocalTime? endDate)
            => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");

        public static int DateDiffMicrosecond(this DbFunctions _, LocalTime startDate, LocalTime endDate)
           => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");

        public static int? DateDiffMicrosecond(this DbFunctions _, LocalTime? startDate, LocalTime? endDate)
            => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");

        public static int DateDiffNanosecond(this DbFunctions _, LocalTime startDate, LocalTime endDate)
           => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");

        public static int? DateDiffNanosecond(this DbFunctions _, LocalTime? startDate, LocalTime? endDate)
            => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");

        public static long DateDiffSecondBig(this DbFunctions _, LocalTime startDate, LocalTime endDate)
           => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");

        public static long? DateDiffSecondBig(this DbFunctions _, LocalTime? startDate, LocalTime? endDate)
            => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");

        public static long DateDiffMillisecondBig(this DbFunctions _, LocalTime startDate, LocalTime endDate)
           => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");

        public static long? DateDiffMillisecondBig(this DbFunctions _, LocalTime? startDate, LocalTime? endDate)
            => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");

        public static long DateDiffMicrosecondBig(this DbFunctions _, LocalTime startDate, LocalTime endDate)
           => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");

        public static long? DateDiffMicrosecondBig(this DbFunctions _, LocalTime? startDate, LocalTime? endDate)
            => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");

        public static long DateDiffNanosecondBig(this DbFunctions _, LocalTime startDate, LocalTime endDate)
           => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");

        public static long? DateDiffNanosecondBig(this DbFunctions _, LocalTime? startDate, LocalTime? endDate)
            => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");

        // Duration
        public static int DateDiffHour(this DbFunctions _, Duration startDate, Duration endDate)
            => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");

        public static int? DateDiffHour(this DbFunctions _, Duration? startDate, Duration? endDate)
            => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");

        public static int DateDiffMinute(this DbFunctions _, Duration startDate, Duration endDate)
            => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");

        public static int? DateDiffMinute(this DbFunctions _, Duration? startDate, Duration? endDate)
            => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");

        public static int DateDiffSecond(this DbFunctions _, Duration startDate, Duration endDate)
            => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");

        public static int? DateDiffSecond(this DbFunctions _, Duration? startDate, Duration? endDate)
            => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");

        public static int DateDiffMillisecond(this DbFunctions _, Duration startDate, Duration endDate)
            => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");

        public static int? DateDiffMillisecond(this DbFunctions _, Duration? startDate, Duration? endDate)
            => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");

        public static int DateDiffMicrosecond(this DbFunctions _, Duration startDate, Duration endDate)
           => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");

        public static int? DateDiffMicrosecond(this DbFunctions _, Duration? startDate, Duration? endDate)
            => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");

        public static int DateDiffNanosecond(this DbFunctions _, Duration startDate, Duration endDate)
           => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");

        public static int? DateDiffNanosecond(this DbFunctions _, Duration? startDate, Duration? endDate)
            => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");

        public static long DateDiffSecondBig(this DbFunctions _, Duration startDate, Duration endDate)
           => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");

        public static long? DateDiffSecondBig(this DbFunctions _, Duration? startDate, Duration? endDate)
            => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");

        public static long DateDiffMillisecondBig(this DbFunctions _, Duration startDate, Duration endDate)
           => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");

        public static long? DateDiffMillisecondBig(this DbFunctions _, Duration? startDate, Duration? endDate)
            => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");

        public static long DateDiffMicrosecondBig(this DbFunctions _, Duration startDate, Duration endDate)
           => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");

        public static long? DateDiffMicrosecondBig(this DbFunctions _, Duration? startDate, Duration? endDate)
            => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");

        public static long DateDiffNanosecondBig(this DbFunctions _, Duration startDate, Duration endDate)
           => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");

        public static long? DateDiffNanosecondBig(this DbFunctions _, Duration? startDate, Duration? endDate)
            => throw new NotImplementedException($"This method is available only for consuming via LINQ for EntityFramework translation to SQL.");
    }
}
