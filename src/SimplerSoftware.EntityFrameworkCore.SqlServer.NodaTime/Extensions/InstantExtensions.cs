using NodaTime;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.EntityFrameworkCore.SqlServer.NodaTime.Extensions
{
    public static class InstantExtensions
    {
        public static Instant AddDays(this Instant instance, double days)
        {
            return instance.Plus(Duration.FromDays(days));
        }
    }
}
