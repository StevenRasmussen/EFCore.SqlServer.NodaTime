using Microsoft.EntityFrameworkCore;
using SimplerSoftware.EntityFrameworkCore.SqlServer.NodaTime.Tests.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SimplerSoftware.EntityFrameworkCore.SqlServer.NodaTime.Tests
{
    [Collection(nameof(DatabaseTestCollection))]
    public abstract class QueryTestBase
    {
        public QueryTestBase(DatabaseTestFixture databaseTestFixture)
        {
            this.Db = databaseTestFixture.DbContext;
        }

        protected RacingContext Db { get; }

        protected DbFunctions Functions { get; }

        protected string RaceSelectStatement => "SELECT [r].[Id], [r].[Date], [r].[ScheduledDuration], [r].[ScheduledStart], [r].[ScheduledStartTime] FROM [Race] AS [r]";

        protected string RaceResultSelectStatement => "SELECT [r].[Id], [r].[EndTime], [r].[OffsetFromWinner], [r].[StartTime], [r].[StartTimeOffset] FROM [RaceResult] AS [r]";

        protected string RaceSplitSelectStatement => "SELECT [r].[Id], [r].[TimeStampInstant], [r].[TimeStampLocalDateTime], [r].[TimeStampLocalTime], [r].[TimeStampOffsetDateTime] FROM [RaceSplit] AS [r]";

        protected static string condense(string str)
        {
            var split = str.Split((char[])null, StringSplitOptions.RemoveEmptyEntries);
            return string.Join(" ", split);
        }
    }
}
