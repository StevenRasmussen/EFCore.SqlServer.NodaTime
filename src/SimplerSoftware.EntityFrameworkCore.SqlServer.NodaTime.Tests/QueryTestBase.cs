using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SimplerSoftware.EntityFrameworkCore.SqlServer.NodaTime.Tests.Models;
using System;
using Xunit;

namespace SimplerSoftware.EntityFrameworkCore.SqlServer.NodaTime.Tests
{
    [Collection(nameof(DatabaseTestCollection))]
    public abstract class QueryTestBase
    {
        public QueryTestBase(DatabaseTestFixture databaseTestFixture)
        {
            this.Db = databaseTestFixture.DbContext;
            this.DbContextOptions = databaseTestFixture.DbContextOptions;
            this.SqlConnection = databaseTestFixture.SqlConnection;
        }

        protected RacingContext Db { get; }

        protected DbContextOptions<RacingContext> DbContextOptions { get; }

        protected DbFunctions Functions { get; }

        protected string RaceSelectStatement => "SELECT [r].[Id], [r].[Date], [r].[ScheduledDuration], [r].[ScheduledStart], [r].[ScheduledStartTime] FROM [Race] AS [r]";

        protected string RaceResultSelectStatement => "SELECT [r].[Id], [r].[EndTime], [r].[OffsetFromWinner], [r].[StartTime], [r].[StartTimeOffset] FROM [RaceResult] AS [r]";

        protected string RaceSplitSelectStatement => "SELECT [r].[Id], [r].[TimeStampInstant], [r].[TimeStampLocalDateTime], [r].[TimeStampLocalTime], [r].[TimeStampOffsetDateTime] FROM [RaceSplit] AS [r]";

        protected SqlConnection SqlConnection { get; }

        protected static string condense(string str)
        {
            var split = str.Split((char[])null, StringSplitOptions.RemoveEmptyEntries);
            return string.Join(" ", split);
        }
    }
}
