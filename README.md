# EFCore.NodaTime

Adds native support to EntityFrameworkCore for the [NodaTime](https://nodatime.org/) types.

The following types are supported:
* Instant
* OffsetDateTime
* LocalDateTime
* LocalDate
* LocalTime
* Duration

## Unit Tests
All types and their methods have unit tests written to verify that the SQL is translated as expected. See individual tests for more information.

## Usage
To use, simply install the NuGet package:
```shell
Install-Package SimplerSoftware.EntityFrameworkCore.SqlServer.NodaTime
```

Then call the `UseNodaTime()` method as part of your SqlServer configuration during the `UseSqlServer` method call:
```csharp
Using Microsoft.EntityFrameworkCore.SqlServer.NodaTime.Extensions;

options.UseSqlServer("your DB Connection",
                    x => x.UseNodaTime());
```
## DATEADD Support
The SQL `DATEADD` function is supported for the following types:
* Instant (extension methods)
* LocalDate (native and some extension methods)
* LocalTime (native and some extension methods)

### Supported Methods
* PlusYears
* PlusMonths
* PlusDays
* PlusHours
* PlusMinutes
* PlusSeconds
* PlusMilliseconds

```csharp
Using Microsoft.EntityFrameworkCore.SqlServer.NodaTime.Extensions;

// PlusYears
await this.Db.RaceResult
    .Where(r => r.StartTime.PlusYears(1) >= Instant.FromUtc(2019, 7, 1, 1, 0))
    .ToListAsync();

// Translates to: 
// SELECT [r].[Id], [r].[EndTime], [r].[StartTime], [r].[StartTimeOffset] 
// FROM [RaceResult] AS [r] WHERE DATEADD(year, CAST(1 AS int), [r].[StartTime]) >= '2019-07-01T01:00:00.0000000Z'
```

## DATEPART Support
The SQL `DATEPART` function is supported for the following types:
* Instant (extension methods)
* LocalDate (native and some extension methods)
* LocalTime (native and some extension methods)

### Supported Methods
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
Using Microsoft.EntityFrameworkCore.SqlServer.NodaTime.Extensions;

// Compare the 'Year' DatePart
await this.Db.RaceResult
    .Where(r => r.StartTime.Year() == 2019)
    .ToListAsync();

// Translates to: 
// SELECT [r].[Id], [r].[EndTime], [r].[StartTime], [r].[StartTimeOffset] 
// FROM [RaceResult] AS [r] WHERE DATEPART(year, [r].[StartTime]) = 2019
```
