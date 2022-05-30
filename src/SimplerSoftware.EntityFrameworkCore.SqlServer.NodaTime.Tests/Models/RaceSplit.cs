using NodaTime;

namespace SimplerSoftware.EntityFrameworkCore.SqlServer.NodaTime.Tests.Models
{
    public class RaceSplit
    {
        public int Id { get; set; }

        public Instant TimeStampInstant { get; set; }

        public OffsetDateTime TimeStampOffsetDateTime { get; set; }

        public LocalDateTime TimeStampLocalDateTime { get; set; }

        public LocalTime TimeStampLocalTime { get; set; }
    }
}
