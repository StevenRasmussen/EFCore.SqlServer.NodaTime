using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

using SimplerSoftware.EntityFrameworkCore.SqlServer.NodaTime.Tests.Models;

using Xunit;
using Xunit.Abstractions;

namespace SimplerSoftware.EntityFrameworkCore.SqlServer.NodaTime.Tests
{
    public class SupportedTypesTests : QueryTestBase
    {
        public SupportedTypesTests(DatabaseTestFixture databaseTestFixture, ITestOutputHelper output)
            : base(databaseTestFixture)
        {
        }


        [Fact]
        public async Task AllSupported_NodatimeTypes_AreSavedCorrectly()
        {
            await using var transaction = await this.Db.Database.BeginTransactionAsync();

            var sut = new SupportedNodaTypes();

            await this.Db.AddAsync(sut);
            await this.Db.SaveChangesAsync();

            var newContext = new RacingContext(this.DbContextOptions);
            await newContext.Database.UseTransactionAsync(transaction.GetDbTransaction());

            var raceResultFromDb = await newContext.TypeResults.FirstOrDefaultAsync(x => x.Id == sut.Id);

            Assert.NotNull(raceResultFromDb);

            await transaction.CommitAsync();
        }
    }
}