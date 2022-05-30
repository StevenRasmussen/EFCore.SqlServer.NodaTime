using NodaTime;

namespace SimplerSoftware.EntityFrameworkCore.SqlServer.NodaTime.Tests.Models
{
    public class RaceResult
    {
        public int Id { get; set; }

        public Instant StartTime { get; set; }

        public Instant EndTime { get; set; }

        public OffsetDateTime StartTimeOffset { get; set; }

        public Duration OffsetFromWinner { get; set; }
    }
}
