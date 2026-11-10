# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

### All packages

#### Changed
- **v11 public API reduction, week members.** `WeekOfMonth` and `WeekOfYear` are now `DateOnly`-only members taking a required `System.Globalization.CultureInfo`. The `DateTime` receivers, the parameterless week properties, and the `CalendarWeekRule` parameter are gone; the week rule now comes from `culture.DateTimeFormat.CalendarWeekRule`, and no member reads `CultureInfo.CurrentCulture`. **Migration.** Call `someDateOnly.WeekOfMonth(culture)` / `someDateOnly.WeekOfYear(culture)` and pass a `CultureInfo` explicitly. `WeekOfMonth` returns `0` for a date before the culture's first week start.
- **v11 public API reduction, guard module merge.** The three `ArgumentExceptionExtensions` types (`DotExtensions.MsExtensions.Primitives`, `DotExtensions.Memory.Exceptions`, and the char-family guards) collapsed into a single public `ArgumentExceptionExtensions` in `DotExtensions.Exceptions` carrying exactly eight members: `ThrowIfNullOrEmpty(StringSegment)`, `ThrowIfNullOrWhiteSpace(StringSegment)`, `ThrowIfNullOrEmpty(StringValues)`, `ThrowIfNullOrWhiteSpace(StringValues)`, and `ThrowIfEmptyOrWhiteSpace` over `Span<char>`, `ReadOnlySpan<char>`, `Memory<char>`, and `ReadOnlyMemory<char>`. All eight capture `paramName` via `CallerArgumentExpression` (optional, defaulting to `null`; the old hand-written `paramName = ""` defaults are gone), and the `ThrowIfNullOrWhitespace` casing is retired in favour of `ThrowIfNullOrWhiteSpace`. **Migration.** Qualify the guards with `using DotExtensions.Exceptions;`; member names and argument lists are otherwise unchanged.
- **Runtime dependency.** `Microsoft.Extensions.Primitives` updated to 11.0.0.

#### Removed
- **Dates arithmetic and BCL-restating members.** `Add(DateTime)`, `Subtract(DateTime)`, `SubtractMonths(double)`, `SubtractYears(double)`, the three `TimeSpanDifferenceExtensions.Difference` overloads (`DateTime`, `TimeOnly`, `DateOnly`), the `DateOnly` `SubtractDays` / `SubtractMonths` / `SubtractYears` family, and the `DateOnly` `ToDateTime` receiver, eleven members in total. **Migration.** Use the BCL operators `a + b`, `a - b`, and `(a - b).Duration()`; `d.AddMonths(-n)`, `d.AddYears(-n)`, and `d.AddDays(-n)` for negative arithmetic; and the BCL conversions `dateOnly.ToDateTime()` / `DateOnly.FromDateTime(...)` (or `new DateTime(y, m, d)` when building a `DateTime` from parts).
- **DayOf and DayIn families.** `CalculateDayOfWeekAsInteger` (both receivers) and `CalculateNumberOfDaysInYear` (both receivers). **Migration.** Compose the Monday-first day numbering as `date.DayOfWeek == DayOfWeek.Sunday ? 7 : (int)date.DayOfWeek`, and the year length as `DateTime.IsLeapYear(date.Year) ? 366 : 365`.
- **Platform guards.** All four `ThrowIfOSPlatform` / `ThrowIfNotOSPlatform` overloads on `PlatformNotSupportedExceptionExtensions`; no platform guard of any form remains. **Migration.** The BCL boolean `OperatingSystem.IsOSPlatform(...)` plus a caller-thrown `PlatformNotSupportedException`.
- **SecureString guards.** `ThrowIfNullOrEmpty(SecureString)` and `ThrowIfNullOrWhiteSpace(SecureString)`; no replacement SecureString guard exists. **Migration.** The BCL `ArgumentNullException.ThrowIfNull(secureString)` plus a local length check (`secureString.Length == 0`).
- **String-receiver span and memory guards.** `ThrowIfEmptyOrWhiteSpace` over `Span<string>`, `ReadOnlySpan<string>`, `Memory<string>`, and `ReadOnlyMemory<string>`. **Migration.** A local element check (e.g. `string.IsNullOrWhiteSpace` per element); the surviving guards cover `char` spans and memory only.
- **Public emptiness guards.** The four `ThrowIfSpanIsEmpty` / `ThrowIfMemoryIsEmpty` members on `InvalidOperationThrowIfEmptyExtensions` left the public API and survive only as internal workers of the merged guard module. **Migration.** The surviving char-family `ThrowIfEmptyOrWhiteSpace` guards for `char` data, or a local emptiness check for other element types.

#### Fixed
- **WeekOfYear semantics.** `WeekOfYear` now matches `Calendar.GetWeekOfYear`: a date before week 1 reports the previous year's trailing week number (52 or 53) instead of 0.
- **Guard empty-segment handling.** `ThrowIfNullOrWhiteSpace(StringSegment)` now rejects empty segments too, matching the BCL `ThrowIfNullOrWhiteSpace` doctrine where empty counts as whitespace-only content.
- **IO enumeration.** `SafeEnumerator` returns an empty sequence when the root directory does not exist or cannot be constructed instead of throwing. Hidden and system entries are excluded from safe enumeration. `SafelyGetDirectories` returns an empty array for a missing or inaccessible path. Drive empty checks no longer require the drive to be full, and `DriveInfo.IsEmpty` returns false when the drive is not ready.
- **Directory resolution.** `GetDirectory` resolves the parent directory without scanning logical drives and throws `ArgumentException` when no parent can be resolved.
- **Random IO.** `GetRandomDirectory` uses reservoir-style selection, excludes reparse points and transiently inaccessible entries, swallows mid-walk I/O failures, and throws `InvalidOperationException` when no random directory can be found.
- **File size rounding.** File-size unit conversion now divides at exactly 1000 (previously values of exactly 1000 were left undivided).
- **Unix permissions.** Symbolic notation validation is now structural (optional file-type prefix, nine or ten characters, per-position r/w/x checks) instead of a hard-coded allow-list; numeric notation validates its digits directly. Parsing preserves leading zeros in three-digit input and applies the special permission bits (set-user-ID, set-group-ID, sticky bit) from four-digit notation; the symbolic parser only strips the prefix when ten characters are present.
- **Windows permissions.** Directory access rules added through `WindowsFileAccessRulesExtensions` now carry `ContainerInherit | ObjectInherit` inheritance flags.
- **String insertion.** `InsertRange` allows insertion at the end of the string, validates a null character collection with `ArgumentNullException`, and materialises the input before insertion.
- **String reversal.** `Reverse` reverses one Unicode scalar value (rune) at a time so surrogate pairs are not split.
- **String removal ranges.** `Remove(Index, Index)` and `Remove(Range)` now use an inclusive end index with correct from-end (`^n`) resolution; `Remove(int)` keeps the characters before the start index; `Remove(int, int)` removes the half-open window `[start, start + count)`.
- **Capitalization.** `CapitalizeChars` reports negative indices as `ArgumentOutOfRangeException` and formats the out-of-range message with the offending index.
- **Escape removal.** `RemoveEscapeCharacters` no longer trims spaces and returns the original string when nothing changed.
- **Line endings.** `GetLineEnding` prioritises an adjacent CRLF pair, classifies bare LF/CR by whichever is present, and falls back to the platform default when no line ending exists.
- **SecureString helpers.** `AppendChars` and `SetChars` no longer reject an empty `SecureString`; `SetChars` clears then re-appends; read-only `SecureString` instances throw `InvalidOperationException` with a message.
- **Span copy.** `OptimisticCopy` allocates a fresh array when the destination is too small instead of aliasing the source; the `TryCopy` paths no longer swallow all exceptions via a bare catch.
- **String span and memory emptiness.** `IsEmptyOrWhiteSpace` over `Span<string>`, `ReadOnlySpan<string>`, `Memory<string>`, and `ReadOnlyMemory<string>` returns false instead of throwing a `NullReferenceException` when an element is null.
- **Number counting.** `CountNumberOfDigits` counts the digits of the integer part of the absolute value (including floats, decimals, and generic `INumber<T>` types), returns 1 for magnitudes below 1, returns -1 for NaN and both infinities, and no longer hangs on infinity.
- **Number ranges.** `IncrementedNumberRange` rejects sequences that wrap around a type's bounds; `NumberRange.AsRange` supports from-end starts.
- **Number conversion.** `NumberToTNumber` conversions use `CreateChecked` (culture-independent, no text round-trip) and throw `OverflowException` for unrepresentable values.
- **Version parsing.** `GracefulParse` tokenises on the detected separator, drops digit-less tokens before applying the four-component cap, handles Unicode digits, and chains the inner exception; `FindSeparator` distinguishes "no separator" from a space.
- **StringSegment guards and conversion.** `IsNullOrEmpty` / `IsNullOrWhiteSpace` documentation corrected; `Reverse` and `Remove` fixes mirror the string-family fixes; trailing-separator trimming guards against empty sequences.
- **Version comparison.** Documentation clarified to state the comparison direction of each member.
- **Localization.** New resource strings for IO random-directory-not-found, SecureString read-only, and whitespace guard messages.

[Unreleased]: https://github.com/alastairlundy/DotExtensions/compare/10.6.3...HEAD