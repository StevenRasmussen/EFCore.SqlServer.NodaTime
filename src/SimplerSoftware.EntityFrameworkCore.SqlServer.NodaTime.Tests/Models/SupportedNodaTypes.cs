using NodaTime;

namespace SimplerSoftware.EntityFrameworkCore.SqlServer.NodaTime.Tests.Models
{
    public class SupportedNodaTypes
    {
        public int Id { get; set; }

        public Duration DurationNonNull { get; set; }
        public Duration? DurationNull { get; set; }
        public Instant InstantNonNull { get; set; }
        public Instant? InstantNull { get; set; }
        public LocalDate LocalDateNonNull { get; set; } // SqlDateTime overflow. Must be between 1/1/1753 12:00:00 AM and 12/31/9999 11:59:59 PM.
        public LocalDate? LocalDateNull { get; set; }
        public LocalDateTime LocalDateTimeNonNull { get; set; } // SqlDateTime overflow. Must be between 1/1/1753 12:00:00 AM and 12/31/9999 11:59:59 PM.
        public LocalDateTime? LocalDateTimeNull { get; set; }
        public LocalTime LocalTimeNonNull { get; set; }
        public LocalTime? LocalTimeNull { get; set; }
        public OffsetDateTime OffsetDateTimeNonNull { get; set; }
        public OffsetDateTime? OffsetDateTimeNull { get; set; }

    }
}
