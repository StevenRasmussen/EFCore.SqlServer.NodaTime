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
        public LocalDate LocalDateNonNull { get; set; }
        public LocalDate? LocalDateNull { get; set; }
        public LocalDateTime LocalDateTimeNonNull { get; set; }
        public LocalDateTime? LocalDateTimeNull { get; set; }
        public LocalTime LocalTimeNonNull { get; set; }
        public LocalTime? LocalTimeNull { get; set; }
        public OffsetDateTime OffsetDateTimeNonNull { get; set; }
        public OffsetDateTime? OffsetDateTimeNull { get; set; }

    }
}
