using NodaTime;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimplerSoftware.EntityFrameworkCore.SqlServer.NodaTime.Tests.Models
{
    public class RaceResult
    {
        public int Id { get; set; }

        public Instant StartTime { get; set; }

        public Instant EndTime { get; set; }
    }
}
