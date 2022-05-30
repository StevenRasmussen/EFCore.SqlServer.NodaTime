using Microsoft.Extensions.Logging;
using System;

namespace SimplerSoftware.EntityFrameworkCore.SqlServer.NodaTime.Tests.Logging
{
    class TestLoggerFactory : ILoggerFactory
    {
        public TestLogger Logger { get; }
            = new TestLogger();

        public void AddProvider(ILoggerProvider provider)
            => throw new NotImplementedException();

        public ILogger CreateLogger(string categoryName)
            => Logger;

        public void Dispose()
        {
        }
    }
}
