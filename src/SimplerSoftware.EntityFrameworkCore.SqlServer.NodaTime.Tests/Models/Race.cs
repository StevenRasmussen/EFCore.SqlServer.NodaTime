using NodaTime;
using System;

namespace SimplerSoftware.EntityFrameworkCore.SqlServer.NodaTime.Tests.Models
{
    public class Race
    {
        public int Id { get; set; }

        public LocalDate Date { get; set; }

        public DateTime DateTimeDate { get; set; }

        public LocalDateTime ScheduledStart { get; set; }

        public LocalTime ScheduledStartTime { get; set; }

        public Duration ScheduledDuration { get; set; }
    }
}
