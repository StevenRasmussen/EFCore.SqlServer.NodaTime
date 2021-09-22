using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Scaffolding;
using System.Reflection;

namespace SimplerSoftware.EntityFrameworkCore.SqlServer.NodaTime.Design
{
    public class SqlServerNodaTimeCodeGeneratorPlugin : ProviderCodeGeneratorPlugin
    {
        public override MethodCallCodeFragment GenerateProviderOptions()
        {
            var methodInfo = typeof(SqlServerDbContextOptionsBuilderExtensions).GetMethod(nameof(SqlServerDbContextOptionsBuilderExtensions.UseNodaTime), BindingFlags.Public | BindingFlags.Static);
            return new MethodCallCodeFragment(methodInfo);
        }
    }
}
