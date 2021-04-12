using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Scaffolding;
using Microsoft.Extensions.DependencyInjection;

namespace SimplerSoftware.EntityFrameworkCore.SqlServer.NodaTime.Design
{
    public class SqlServerNodaTimeCodeGeneratorPlugin : ProviderCodeGeneratorPlugin
    {
        public override MethodCallCodeFragment GenerateProviderOptions() 
            => new MethodCallCodeFragment(nameof(ServiceCollectionExtensions.AddNodaTime));
    }
}
