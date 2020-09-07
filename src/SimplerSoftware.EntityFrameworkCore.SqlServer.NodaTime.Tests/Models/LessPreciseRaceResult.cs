using NodaTime;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimplerSoftware.EntityFrameworkCore.SqlServer.NodaTime.Tests.Models
{
    public class LessPreciseRaceResult
    {
        public int Id { get; set; }

        [Column(TypeName = "datetime")]
        public Instant StartTime { get; set; }

        [Column(TypeName = "datetime")]
        public Instant EndTime { get; set; }

        public OffsetDateTime StartTimeOffset { get; set; }

        public Duration OffsetFromWinner { get; set; }
    }
}
