using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Scaffolding;

namespace SimplerSoftware.EntityFrameworkCore.SqlServer.NodaTime.Design
{
    public class SqlServerNodaTimeCodeGeneratorPlugin : ProviderCodeGeneratorPlugin
    {
        public override MethodCallCodeFragment GenerateProviderOptions() 
            => new MethodCallCodeFragment(nameof(SqlServerDbContextOptionsBuilderExtensions.UseNodaTime));
    }
}
