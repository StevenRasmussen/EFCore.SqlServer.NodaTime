![.NET Core](https://github.com/StevenRasmussen/EFCore.SqlServer.NodaTime/workflows/.NET%20Core/badge.svg?branch=master)
[![NuGet version (EFCore.SqlServer.NodaTime)](https://img.shields.io/nuget/v/SimplerSoftware.EntityFrameworkCore.SqlServer.NodaTime.svg)](https://www.nuget.org/packages/SimplerSoftware.EntityFrameworkCore.SqlServer.NodaTime/)
[Version History](#version-history)

# EFCore.SqlServer.NodaTime

Adds native support to EntityFrameworkCore for SQL Server for the [NodaTime](https://nodatime.org/) types.

When modelling, usage of the following NodaTime types are supported:
* Instant
* OffsetDateTime
* LocalDateTime
* LocalDate
* LocalTime
* Duration (stored as `time` in SQL Server which limits this type to < 24 hours)

When querying, standard operators are supported as well as a range of additional mappings from NodaTime properties/function to their SQL Server equivalents.

## Unit Tests
All types and their methods have unit tests written to verify that the SQL is translated as expected. See individual tests for more information.

**Note:** To run the unit tests for the first time, you will need to uncomment the lines [here](https://github.com/StevenRasmussen/EFCore.SqlServer.NodaTime/blob/017f19d68ac12e0ff2ce3ba34f60f1edd42badfe/src/SimplerSoftware.EntityFrameworkCore.SqlServer.NodaTime.Tests/DatabaseTestFixture.cs#L20-L21). This ensures that the test DB is created locally.

## Usage
To use, simply install the NuGet package:
```shell
Install-Package SimplerSoftware.EntityFrameworkCore.SqlServer.NodaTime
```

**Note:** Versioning follows the [major.minor] of EF Core so that it is easy to know which version to install. Ie, if you are using EF Core v5.x then you would install v5.x of this package. Build and revision numbers are not guaranteed to follow the same numbers.

Then call the `UseNodaTime()` method as part of your SqlServer configuration during the `UseSqlServer` method call:
```csharp
using Microsoft.EntityFrameworkCore;

options.UseSqlServer("your DB Connection",
                    x => x.UseNodaTime());
```

## Reverse Engineering (Scaffolding)
Support for [reverse engineering](https://docs.microsoft.com/en-us/ef/core/managing-schemas/scaffolding?tabs=dotnet-core-cli) has been added starting in v5.0.2.

The SQL Server types map as follows:
* `smalldatetime` -> `Instant`
* `datetime` -> `Instant`
* `datetime2` -> `Instant`
* `date` -> `LocalDate`
* `time` -> `LocalTime`
* `datetimeoffset` -> `OffsetDateTime`

## Additional property / function mappings

### DATEADD Support
The SQL `DATEADD` function is supported for the following types:
* Instant (extension methods)
* OffsetDateTime (native and some extension methods)
* LocalDateTime (native and some extension methods)
* LocalDate (native and some extension methods)
* LocalTime (native and some extension methods)
* Duration (native and some extension methods)

**Note**: Please add a using statement in order to use the extension methods:
```csharp
using Microsoft.EntityFrameworkCore.SqlServer.NodaTime.Extensions;
```

#### Supported Methods
* PlusYears
* PlusMonths
* PlusDays
* PlusHours
* PlusMinutes
* PlusSeconds
* PlusMilliseconds

```csharp
using Microsoft.EntityFrameworkCore.SqlServer.NodaTime.Extensions;

// PlusYears
await this.Db.RaceResult
    .Where(r => r.StartTime.PlusYears(1) >= Instant.FromUtc(2019, 7, 1, 1, 0))
    .ToListAsync();

// Translates to: 
// SELECT [r].[Id], [r].[EndTime], [r].[StartTime], [r].[StartTimeOffset] 
// FROM [RaceResult] AS [r] WHERE DATEADD(year, CAST(1 AS int), [r].[StartTime]) >= '2019-07-01T01:00:00.0000000Z'
```

### DATEPART Support
The SQL `DATEPART` function is supported for the following types:
* Instant (extension methods)
* OffsetDateTime (native and some extension methods)
* LocalDateTime (native and some extension methods)
* LocalDate (native and some extension methods)
* LocalTime (native and some extension methods)
* Duration (native and some extension methods)

**Note**: Please add a using statement in order to use the extension methods:
```csharp
using Microsoft.EntityFrameworkCore.SqlServer.NodaTime.Extensions;
```

#### Supported Parts
* Year
* Quarter
* Month
* DayOfYear
* Day
* Week
* WeekDay
* Hour
* Minute
* Second
* Millisecond
* Microsecond
* Nanosecond
* TzOffset
* IsoWeek

```csharp
using Microsoft.EntityFrameworkCore.SqlServer.NodaTime.Extensions;

// Compare the 'Year' DatePart
await this.Db.RaceResult
    .Where(r => r.StartTime.Year() == 2019)
    .ToListAsync();

// Translates to: 
// SELECT [r].[Id], [r].[EndTime], [r].[StartTime], [r].[StartTimeOffset] 
// FROM [RaceResult] AS [r] WHERE DATEPART(year, [r].[StartTime]) = 2019
```

### DATEDIFF Support
The SQL `DATEDIFF` function is supported for the following types:
* Instant (extension methods)
* OffsetDateTime (extension methods)
* LocalDateTime (extension methods)
* LocalDate (extension methods)
* LocalTime (extension methods)
* Duration (extension methods)

**Note**: Please add a using statement in order to use the extension methods:
```csharp
using Microsoft.EntityFrameworkCore.SqlServer.NodaTime.Extensions;
```

#### Supported Parts
* Year
* Quarter
* Month
* DayOfYear
* Day
* Week
* WeekDay
* Hour
* Minute
* Second
* Millisecond
* Microsecond
* Nanosecond
* TzOffset
* IsoWeek

```csharp
using Microsoft.EntityFrameworkCore.SqlServer.NodaTime.Extensions;

// DateDiff based on 'day'
DbFunctions dbFunctions = null;

await this.Db.Race
    .Where(r => dbFunctions.DateDiffDay(r.Date, new LocalDate(2020, 1, 1)) >= 200)
    .ToListAsync();

// Translates to: 
// SELECT [r].[Id], [r].[Date], [r].[ScheduledDuration], [r].[ScheduledStart], [r].[ScheduledStartTime]
// FROM [Race] AS [r]
// WHERE DATEDIFF(DAY, [r].[Date], '2020-01-01') >= 200
```

### DATEDIFF_BIG Support
The SQL `DATEDIFF_BIG` function is supported for the following types:
* Instant (extension methods)
* OffsetDateTime (extension methods)
* LocalDateTime (extension methods)
* LocalTime (extension methods)
* Duration (extension methods)

**Note**: Please add a using statement in order to use the extension methods:
```csharp
using Microsoft.EntityFrameworkCore.SqlServer.NodaTime.Extensions;
```

#### Supported Parts
* Second
* Millisecond
* Microsecond
* Nanosecond

```csharp
using Microsoft.EntityFrameworkCore.SqlServer.NodaTime.Extensions;

// DateDiffBig based on 'second'
DbFunctions dbFunctions = null;

await this.Db.RaceResult
    .Where(r => dbFunctions.DateDiffBigSecond(r.StartTime, Instant.FromUtc(2019, 7, 1, 0, 0)) >= 100000)
    .ToListAsync();

// Translates to: 
// SELECT [r].[Id], [r].[EndTime], [r].[OffsetFromWinner], [r].[StartTime], [r].[StartTimeOffset]
// FROM [RaceResult] AS [r]
// WHERE DATEDIFF_BIG(SECOND, [r].[StartTime], '2019-07-01T00:00:00.0000000Z') >= CAST(100000 AS bigint)
```

## Sponsors

[Entity Framework Extensions](https://entityframework-extensions.net/) and [Dapper Plus](https://dapper-plus.net/) are major sponsors and proud to contribute to the development of **EFCore.SqlServer.NodaTime**.

[![Entity Framework Extensions](https://raw.githubusercontent.com/StevenRasmussen/EFCore.SqlServer.NodaTime/master/entity-framework-extensions-sponsor.png)](https://entityframework-extensions.net/bulk-insert)

[![Dapper Plus](https://raw.githubusercontent.com/StevenRasmussen/EFCore.SqlServer.NodaTime/master/dapper-plus-sponsor.png)](https://dapper-plus.net/bulk-insert)

## Version History

* 9.1.0 (Mar 14, 2025)
  * Added support for Azure SQL DB's and the `AzureSqlDbContextOptionsBuilder`
* 9.0.0 (Nov 20, 2024)
  * Release for EF Core 9
* 9.0.0-rc.1.24451.1 (Sept 27, 2024)
  * Release candidate 1 for EF Core 9
* 8.0.1 (April 6, 2024)
  * Added support for compiled models - [#39](/../../issues/39)
* 8.0.0 (November 18, 2023)
  * Release for EF Core 8
* 8.0.0-rc.1.23419.6 (September 15, 2023)
  * Release candidate 1 for EF Core 8
* 7.1.0 (August 12, 2023)
  * Added support for `LocalDateTime.Date` property - [#35](/../../issues/35)
  * Added support for `OffsetDateTime.Date` property
* 7.0.0 (November 9, 2022)
  * Initial release supporting EF Core 7.0.0
* 7.0.0-rc.1.22426.7 (September 25, 2022)
  * Release candidate for EF Core 7
* 6.0.1
  * Fixed an issue where an `ArgumentNullException` would throw if you called `UseInternalServiceProvider` - [#27](/../../issues/27)
* 6.0.0
  * Support for .Net 6 - [#23](/../../issues/23)
* 5.0.3
  * Fixed an issue where `DateTime` queries failed in some instances - [#25](/../../issues/25)
* 5.0.2
  * Added design time support - [#16](/../../issues/16)
* 5.0.1
  * Fix SqlDateTime overflow - [#13](/../../issues/13) 
* 5.0.0
  * Support for .Net 5
* 3.1.2
  * Fixed an issue where `DateTime` queries failed in some instances - [#25](/../../issues/25)
* 3.1.1
  * Fix SqlDateTime overflow - [#13](/../../issues/13)
* 3.1.0
  * Sync version number with .Net Core 3.1.0
