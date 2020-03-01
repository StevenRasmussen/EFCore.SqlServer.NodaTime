# EFCore.NodaTime

Adds native support to EntityFrameworkCore for the [NodaTime](https://nodatime.org/) types.

The following types are supported:
* Instant
* OffsetDateTime
* LocalDateTime
* LocalDate
* LocalTime
* Duration

As of version 1.0.0, only basic operations are supported.  There is not support for functions related to these types.

To use, simply install the NuGet package:
```
Install-Package SimplerSoftware.EntityFrameworkCore.SqlServer.NodaTime
```

Then call the `UseNodaTime()` method as part of your SqlServer configuration during the `UseSqlServer` method call:
```
options.UseSqlServer("your DB Connection",
                    x => x.UseNodaTime());
```
