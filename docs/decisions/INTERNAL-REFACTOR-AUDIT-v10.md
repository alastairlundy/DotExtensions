# Internal Refactor Audit — v10 Collection/Array + Boxing/Conversion

## Purpose

Audit of the main `DotExtensions` package source files for collection/array
allocation hotspots and boxing/conversion issues. Each finding is documented
with file path, line number, current behavior, proposed fix, and estimated
impact. Each actionable finding becomes a separate v10 PR per T008.

## Scope

Per T008, v10 internal-refactor work covers collection/array allocations and
boxing/conversion issues only. LINQ/enumerator allocations and async/await
internal optimizations are deferred to v11.

## Out of Scope (Deferred to v11)

- LINQ/enumerator allocations (iterator object allocations from `.Where()`,
  `.Select()`, etc.) — deferred to v11 per T008.
- async/await internal optimizations — deferred to v11 per T008.
- Findings marked "by design" where the materialization is the explicit
  purpose of the API (e.g., `SafelyGet*` methods that return arrays).

---

## Category A: Collection/Array Allocation Hotspots

### A-1 — CharacterConstants.EscapeCharacters allocates on every access

- **File:** `DotExtensions/System/Strings/Constants/CharacterConstants.cs`
- **Line:** 37
- **Current behavior:** `EscapeCharacters` is a get-only property using a
  collection expression (`=> [...]`). Every access allocates a new `string[]`.
  Accessed inside loops in `EscapeCharacterRemovalExtensions.cs` (lines 45, 58).
- **Proposed fix:** Cache in a `static readonly` field:
  `public static readonly string[] EscapeCharacters = [...];`
- **Impact:** Medium. Small array (12 elements) but allocated on every access
  inside loops.

### A-2 — ContainsSubstringsExtensions.Split allocates array to check count

- **File:** `DotExtensions/System/Strings/Contains/ContainsSubstringsExtensions.cs`
- **Line:** 52
- **Current behavior:** `s.Split(delimiter).Length > 1` allocates a full
  `string[]` just to check whether there is more than one element.
- **Proposed fix:** Replace with a second `IndexOf` call starting after the
  first match: `int firstIdx = s.IndexOf(delimiter);
  return firstIdx != -1 && s.IndexOf(delimiter, firstIdx + 1) != -1;`
- **Impact:** Medium. `string.Split` allocates an array plus one string per
  segment.

### A-3 — GetRandomIOExtensions materializes directory tree in loop

- **File:** `DotExtensions/System/IO/GetRandomIOExtensions.cs`
- **Lines:** 154-155
- **Current behavior:** Inside a `while(true)` loop,
  `.SafelyEnumerateDirectories().Where(d => d.HasFiles).ToArray()` materializes
  a filtered directory enumeration into a new array on every iteration.
- **Proposed fix:** Use lazy enumeration with `FirstOrDefault()` or a
  stack-based approach rather than materializing the full filtered set.
- **Impact:** High. Repeated allocation inside a potentially unbounded loop.
  Each iteration allocates LINQ iterators and a full array.
- **Note:** The LINQ iterator allocation part is v11 scope per T008; the array
  materialization in a loop is v10 scope.

### A-4 — GetRandomIOExtensions materializes full directory tree for random pick

- **File:** `DotExtensions/System/IO/GetRandomIOExtensions.cs`
- **Line:** 111
- **Current behavior:** `DirectoryInfo[] array = dirs.ToArray();` materializes
  the entire filtered directory tree into a single array, only to pick one
  random item via `Random.Shared.GetItems(array, 1)`.
- **Proposed fix:** Use reservoir sampling or count-then-pick to avoid
  materializing the full tree.
- **Impact:** High. On large directory trees, allocates a huge array.
- **Note:** By design for uniform random selection; document the allocation
  cost or provide a lazy alternative.

### A-5 — GetRandomIOExtensions triggers IO per drive for HasDirectories check

- **File:** `DotExtensions/System/IO/GetRandomIOExtensions.cs`
- **Lines:** 49-51
- **Current behavior:** `DriveInfo.SafelyEnumerateLogicalDrives()
  .Where(d => d.HasDirectories).ToArray()` — `HasDirectories` triggers
  directory enumeration per drive.
- **Proposed fix:** Combine filtering or cache the `HasDirectories` result.
- **Impact:** Medium. Drive count is small but `HasDirectories` triggers IO.

### A-6 — GetRandomIOExtensions materializes directories with HasFiles check

- **File:** `DotExtensions/System/IO/GetRandomIOExtensions.cs`
- **Lines:** 146-148
- **Current behavior:** `.SafelyEnumerateDirectories().Where(d => d.HasFiles)
  .ToArray()` — `HasFiles` triggers file enumeration per directory, then
  materializes into array.
- **Proposed fix:** Same as A-4; lazy approaches.
- **Impact:** High. Each `HasFiles` triggers file enumeration.

### A-7 — StringValuesIsNullExtensions allocates bool array for Any check

- **File:** `DotExtensions/MsExtensions/Primitives/StringValuesIsNullExtensions.cs`
- **Line:** 68
- **Current behavior:** `bool[] vals = new bool[other.Count]` allocates a
  boolean array, fills it, then uses `.Any(x => x)`. A simple loop with early
  return would suffice.
- **Proposed fix:** Replace with a direct loop:
  `for (int i = 0; i < other.Count; i++)
  { if (string.IsNullOrWhiteSpace(other[i])) return true; } return false;`
- **Impact:** Low-Medium. Unnecessary array allocation.

### A-8 — SegmentEnumerableToStringExtensions creates StringBuilder without capacity

- **File:** `DotExtensions/MsExtensions/Primitives/Collections/SegmentEnumerableToStringExtensions.cs`
- **Lines:** 48, 80
- **Current behavior:** `StringBuilder stringBuilder = new();` without initial
  capacity. Resizes multiple times as content is appended.
- **Proposed fix:** Estimate capacity from segments (e.g., total length plus
  separator overhead).
- **Impact:** Low-Medium. StringBuilder resizing allocates intermediate char arrays.

### A-9 — VersionParseExtensions.ParseChars creates StringBuilder without capacity

- **File:** `DotExtensions/System/Versions/VersionParseExtensions.cs`
- **Line:** 35
- **Current behavior:** `StringBuilder stringBuilder = new();` without capacity.
- **Proposed fix:** Use `new StringBuilder(chars.Length)`.
- **Impact:** Low. Input is typically short; fix is trivial.

### A-11 — VersionParseExtensions allocates StringBuilder inside loop

- **File:** `DotExtensions/System/Versions/VersionParseExtensions.cs`
- **Line:** 117
- **Current behavior:** `StringBuilder stringBuilder = new(component.Length);`
  allocated inside a `for` loop (up to 4 iterations).
- **Proposed fix:** Reuse a single StringBuilder via `.Clear()` between uses,
  or parse digits directly without StringBuilder.
- **Impact:** Low. Only up to 4 small allocations.

---

## Category B: Boxing/Conversion Issues

### B-1 — FileSizeExtensions uses string.Format with value type (boxing)

- **File:** `DotExtensions/System/IO/FileSizeExtensions.cs`
- **Line:** 50
- **Current behavior:** `string.Format(...)` with `long quantity` parameter
  causes boxing via `params object[]`.
- **Proposed fix:** Use string interpolation:
  `$"{quantity}{file.GetFileSizeUnitString()}"`. On .NET 6+, the interpolated
  string handler avoids boxing.
- **Impact:** Low. Single boxing; not in a tight loop.

### B-2 — UnixPermissionParsingExtensions converts char to int via string

- **File:** `DotExtensions/System/IO/Permissions/Unix/UnixPermissionParsingExtensions.cs`
- **Lines:** 172-174
- **Current behavior:** `int.Parse(notation.First().ToString(), ...)` extracts
  a char, allocates a string via `.ToString()`, then parses back to int.
  Three occurrences per call.
- **Proposed fix:** Use `notation[0] - '0'` directly.
- **Impact:** Low-Medium. Three string allocations per call; fix is trivial.

### B-3 — NumberToTNumber converts via string round-trip

- **File:** `DotExtensions/System/Numbers/NumberToTNumber.cs`
- **Line:** 61
- **Current behavior:** `TDestinationNumber.Parse(number.ToString(), ...)`
  converts number to string then parses back. Used by all number-to-number
  conversions.
- **Proposed fix:** Use `TDestinationNumber.CreateChecked(number)` (available
  in .NET 7+ with `INumber<T>`).
- **Impact:** High. Core conversion method; string allocated on every call.

### B-4 — CountDigitsExtensions calls string-round-trip conversion in loop

- **File:** `DotExtensions/System/Numbers/CountDigitsExtensions.cs`
- **Lines:** 49, 54
- **Current behavior:** `(-1.ToNumber<TNumber>())` and
  `(number /= 10.ToNumber<TNumber>())` call `ToNumber<TNumber>()` which uses
  the string round-trip from B-3. The while loop calls `10.ToNumber<TNumber>()`
  on every iteration.
- **Proposed fix:** Cache converted constants outside the loop. Better: use
  `TNumber.CreateChecked` to avoid string conversion entirely.
- **Impact:** High. O(digits) string allocations per call.

### B-5 — DateOnlyToDateTimeExtension converts via long date string

- **File:** `DotExtensions/System/Dates/DateOnlyExtensions/DateOnlyToDateTimeExtension.cs`
- **Line:** 44
- **Current behavior:** `DateTime.Parse(dateOnly.ToLongDateString(), ...)`
  converts to a culture-formatted date string then parses back.
- **Proposed fix:** Use `new DateTime(dateOnly.Year, dateOnly.Month,
  dateOnly.Day)` or `dateOnly.ToDateTime(TimeOnly.MinValue)`.
- **Impact:** Medium. Allocates 30+ char string plus parsing overhead. Called
  by multiple subtraction/difference extension methods.

### B-6 — CapitalizationExtensions uses "N" format for index

- **File:** `DotExtensions/System/Strings/Cases/CapitalizationExtensions.cs`
- **Line:** 75
- **Current behavior:** `index.ToString("N", CultureInfo.CurrentCulture)` uses
  Number format with group separators for an index value.
- **Proposed fix:** Use `index.ToString(CultureInfo.InvariantCulture)` or
  `$"{index}"`.
- **Impact:** Low. Error path only; semantically wrong format.

### B-7 — SegmentCapitalizationExtensions uses "N" format for index

- **File:** `DotExtensions/MsExtensions/Primitives/Cases/SegmentCapitalizationExtensions.cs`
- **Line:** 77
- **Current behavior:** Same as B-6.
- **Proposed fix:** Same as B-6.
- **Impact:** Low.

### B-8 — StringValuesToStringExtensions interpolates separator in loop

- **File:** `DotExtensions/MsExtensions/Primitives/StringValuesToStringExtensions.cs`
- **Lines:** 57, 89
- **Current behavior:** `stringBuilder.Append($"{separator}")` inside a
  `foreach` loop allocates a new string per iteration when separator is a char.
- **Proposed fix:** Use `stringBuilder.Append(separator)` directly.
- **Impact:** Medium. One string allocation per entry in the loop.

### B-9 — StringValuesToStringExtensions interpolates separator for EndsWith

- **File:** `DotExtensions/MsExtensions/Primitives/StringValuesToStringExtensions.cs`
- **Lines:** 63, 95
- **Current behavior:** `output.EndsWith($"{separator}", ...)` allocates a
  string for comparison.
- **Proposed fix:** Use `output.EndsWith(separator, StringComparison.Ordinal)`
  directly.
- **Impact:** Low. Single allocation per call.

### B-10 — SegmentCapitalizationExtensions allocates via string interpolation

- **File:** `DotExtensions/MsExtensions/Primitives/Cases/SegmentCapitalizationExtensions.cs`
- **Line:** 52
- **Current behavior:** `$"{segment.Substring(0, index)}{char.ToUpper(c, ...)}
  {segment.Substring(index + 1)}"` allocates 3+ strings (two Substrings +
  interpolation result).
- **Proposed fix:** Use `StringBuilder` with pre-allocated capacity.
- **Impact:** Low-Medium.

### B-11 — SegmentRemoveAndReplaceExtensions interpolates two segments

- **File:** `DotExtensions/MsExtensions/Primitives/Removal/SegmentRemoveAndReplaceExtensions.cs`
- **Line:** 81
- **Current behavior:** `new StringSegment($"{firstSegment}{secondSegment}")`
  allocates 2-3 strings.
- **Proposed fix:** Use `StringBuilder` or compute from underlying buffer.
- **Impact:** Low-Medium.

### B-13 — ReadableVersionStringExtensions interpolation on older TFMs

- **File:** `DotExtensions/System/Versions/ReadableVersionStringExtensions.cs`
- **Lines:** 49, 53, 55
- **Current behavior:** String interpolation with `int` value types. On .NET 6+
  the interpolated string handler avoids boxing. On older TFMs (netstandard2.0),
  this boxes each `int`.
- **Proposed fix:** On older TFMs, use `.ToString(CultureInfo.InvariantCulture)`
  concatenated with `.` literals.
- **Impact:** Low. Multi-targeted; only affects older TFMs.

### B-14 — ArgumentExceptionExtensions.SecureStrings allocates SecureString for comparison

- **File:** `DotExtensions/System/Exceptions/ArgumentExceptionExtensions.SecureStrings.cs`
- **Lines:** 73-78
- **Current behavior:** `ThrowIfNullOrWhiteSpace` creates a new `SecureString`
  and fills with spaces for comparison. `SecureString.Equals` uses reference
  equality so the comparison always returns `false` — logic bug plus allocation.
- **Proposed fix:** Iterate through the `SecureString` characters directly via
  `Marshal.SecureStringToBSTR`, check each character, then free the BSTR.
- **Impact:** Low-Medium. Expensive allocation plus logic bug.

---

## Summary Table

| ID | Category | File (short) | Line(s) | Impact | v10 PR Scope |
|----|----------|-------------|---------|--------|-------------|
| A-1 | A | CharacterConstants.cs | 37 | Medium | Yes |
| A-2 | A | ContainsSubstringsExtensions.cs | 52 | Medium | Yes |
| A-3 | A | GetRandomIOExtensions.cs | 154-155 | High | Partial (array only) |
| A-4 | A | GetRandomIOExtensions.cs | 111 | High | Yes |
| A-5 | A | GetRandomIOExtensions.cs | 49-51 | Medium | Yes |
| A-6 | A | GetRandomIOExtensions.cs | 146-148 | High | Partial (array only) |
| A-7 | A | StringValuesIsNullExtensions.cs | 68 | Low-Med | Yes |
| A-8 | A | SegmentEnumerableToStringExtensions.cs | 48, 80 | Low-Med | Yes |
| A-9 | A | VersionParseExtensions.cs | 35 | Low | Yes |
| A-11 | A | VersionParseExtensions.cs | 117 | Low | Yes |
| B-1 | B | FileSizeExtensions.cs | 50 | Low | Yes |
| B-2 | B | UnixPermissionParsingExtensions.cs | 172-174 | Low-Med | Yes |
| B-3 | B | NumberToTNumber.cs | 61 | High | Yes |
| B-4 | B | CountDigitsExtensions.cs | 49, 54 | High | Yes |
| B-5 | B | DateOnlyToDateTimeExtension.cs | 44 | Medium | Yes |
| B-6 | B | CapitalizationExtensions.cs | 75 | Low | Yes |
| B-7 | B | SegmentCapitalizationExtensions.cs | 77 | Low | Yes |
| B-8 | B | StringValuesToStringExtensions.cs | 57, 89 | Medium | Yes |
| B-9 | B | StringValuesToStringExtensions.cs | 63, 95 | Low | Yes |
| B-10 | B | SegmentCapitalizationExtensions.cs | 52 | Low-Med | Yes |
| B-11 | B | SegmentRemoveAndReplaceExtensions.cs | 81 | Low-Med | Yes |
| B-13 | B | ReadableVersionStringExtensions.cs | 49, 53, 55 | Low | Yes |
| B-14 | B | ArgumentExceptionExtensions.SecureStrings.cs | 73-78 | Low-Med | Yes |

## Top Priority Fixes (High Impact)

1. **B-3 + B-4**: `NumberToTNumber` string round-trip used in
   `CountDigitsExtensions` loop — O(digits) string allocations per call.
2. **A-3, A-4, A-6**: `GetRandomIOExtensions` materializes large directory
   trees into arrays inside loops.
3. **A-1**: `CharacterConstants.EscapeCharacters` allocates on every access;
   one-line fix.

## References

- Decision ledger: `DECISIONS-DotExtensions-performance-improvements.md#T008`
- Decision ledger: `DECISIONS-DotExtensions-performance-improvements.md#D005`
- Decision ledger: `DECISIONS-DotExtensions-performance-improvements.md#D006`
