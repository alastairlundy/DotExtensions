# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

### All Packages

#### Changed
- **v11 surface reduction - week members**: `WeekOfMonth` and `WeekOfYear` are now `DateOnly`-only members taking a required `System.Globalization.CultureInfo`. The `DateTime` receivers, the parameterless week properties, and the `CalendarWeekRule` parameter are gone; the week rule now comes from `culture.DateTimeFormat.CalendarWeekRule`, and no member reads `CultureInfo.CurrentCulture`. **Migration:** call `someDateOnly.WeekOfMonth(culture)` / `someDateOnly.WeekOfYear(culture)` and pass a `CultureInfo` explicitly. `WeekOfMonth` returns `0` for a date before the culture's first week start.
- **v11 surface reduction - guard module merge**: the three `ArgumentExceptionExtensions` types (`DotExtensions.MsExtensions.Primitives`, `DotExtensions.Memory.Exceptions`, and the char-family guards) collapsed into a single public `ArgumentExceptionExtensions` in `DotExtensions.Exceptions` carrying exactly eight members: `ThrowIfNullOrEmpty(StringSegment)`, `ThrowIfNullOrWhiteSpace(StringSegment)`, `ThrowIfNullOrEmpty(StringValues)`, `ThrowIfNullOrWhiteSpace(StringValues)`, and `ThrowIfEmptyOrWhiteSpace` over `Span<char>`, `ReadOnlySpan<char>`, `Memory<char>`, and `ReadOnlyMemory<char>`. All eight capture `paramName` via `CallerArgumentExpression` (optional, defaulting to `null`; the old hand-written `paramName = ""` defaults are gone), and the `ThrowIfNullOrWhitespace` casing is retired in favour of `ThrowIfNullOrWhiteSpace`. **Migration:** qualify the guards with `using DotExtensions.Exceptions;` - member names and argument lists are otherwise unchanged.

#### Removed
- **Dates arithmetic and BCL-restating members**: `Add(DateTime)`, `Subtract(DateTime)`, `SubtractMonths(double)`, `SubtractYears(double)`, the three `TimeSpanDifferenceExtensions.Difference` overloads (`DateTime`, `TimeOnly`, `DateOnly`), the `DateOnly` `SubtractDays` / `SubtractMonths` / `SubtractYears` family, and the `DateOnly` `ToDateTime` receiver - eleven members in total. **Migration:** use the BCL operators `a + b`, `a - b`, and `(a - b).Duration()`; `d.AddMonths(-n)`, `d.AddYears(-n)`, and `d.AddDays(-n)` for negative arithmetic; and the BCL conversions `dateOnly.ToDateTime()` / `DateOnly.FromDateTime(...)` (or `new DateTime(y, m, d)` when building a `DateTime` from parts).
- **DayOf and DayIn families**: `CalculateDayOfWeekAsInteger` (both receivers) and `CalculateNumberOfDaysInYear` (both receivers). **Migration:** compose the Monday-first day numbering as `date.DayOfWeek == DayOfWeek.Sunday ? 7 : (int)date.DayOfWeek`, and the year length as `DateTime.IsLeapYear(date.Year) ? 366 : 365`.
- **Platform guards**: all four `ThrowIfOSPlatform` / `ThrowIfNotOSPlatform` overloads on `PlatformNotSupportedExceptionExtensions`; no platform guard of any form remains. **Migration:** the BCL boolean `OperatingSystem.IsOSPlatform(...)` plus a caller-thrown `PlatformNotSupportedException`.
- **SecureString guards**: `ThrowIfNullOrEmpty(SecureString)` and `ThrowIfNullOrWhiteSpace(SecureString)`; no replacement SecureString guard exists. **Migration:** the BCL `ArgumentNullException.ThrowIfNull(secureString)` plus a local length check (`secureString.Length == 0`).
- **String-receiver span/memory guards**: `ThrowIfEmptyOrWhiteSpace` over `Span<string>`, `ReadOnlySpan<string>`, `Memory<string>`, and `ReadOnlyMemory<string>`. **Migration:** a local element check (e.g. `string.IsNullOrWhiteSpace` per element); the surviving guards cover `char` spans and memory only.
- **Public span/memory emptiness guards**: the four `ThrowIfSpanIsEmpty` / `ThrowIfMemoryIsEmpty` members on `InvalidOperationThrowIfEmptyExtensions` left the public surface and survive only as internal workers of the merged guard module. **Migration:** the surviving char-family `ThrowIfEmptyOrWhiteSpace` guards for `char` data, or a local emptiness check for other element types.

[Unreleased]: https://github.com/alastairlundy/DotExtensions/compare/10.5.2...HEAD
