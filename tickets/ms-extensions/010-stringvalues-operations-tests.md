---
title: StringValues Operations Tests
classification: Independent
blocked_by: []
parent: TESTS_PRD.md
---

## Goal

Add unit tests for `StringValuesToStringExtensions`, `StringValuesLengthExtensions`, and `StringValuesIsNullExtensions` to verify StringValues conversion, total length computation, and null/empty detection.

## What to build

Create test files in `DotExtensions.Tests/MsExtensions/Primitives/StringValues/`:

1. **`StringValuesToStringTests.cs`** — Test `ToString(this StringValues, char)` and `ToString(this StringValues, string)` which join values with a delimiter. Cover:
   - Single-value StringValues
   - Multi-value StringValues
   - Empty StringValues
   - Null StringValues
   - Char and string delimiters

2. **`StringValuesLengthTests.cs`** — Test `TotalLength` property. Cover:
   - Single-value StringValues (length of the value)
   - Multi-value StringValues (sum of all value lengths)
   - Empty StringValues (total length 0)
   - Null StringValues

3. **`StringValuesIsNullTests.cs`** — Test `IsEmpty(this StringValues?)` and `IsNullOrWhiteSpace(this StringValues?)`. Cover:
   - Null, empty, whitespace-only, and non-empty StringValues
   - Single value that is whitespace
   - Multiple values where some are empty

Follow existing patterns: `[Test]`, `async Task`, `await Assert.That(...)`, `[MethodDataSource]` for parameterized data.

## Acceptance criteria

- [ ] `ToString(char)` and `ToString(string)` produce correct joined output
- [ ] `ToString` on empty/null StringValues returns expected empty string
- [ ] `TotalLength` returns correct character count for all cases
- [ ] `IsEmpty` returns true for empty/null, false for non-empty StringValues
- [ ] `IsNullOrWhiteSpace` returns true for null/empty/whitespace, false for non-empty
- [ ] All tests pass on all target frameworks (net8.0, net9.0, net10.0)

## Context pointers

**Files**
- `DotExtensions/MsExtensions/Primitives/StringValuesToStringExtensions.cs` — source
- `DotExtensions/MsExtensions/Primitives/StringValuesLengthExtensions.cs` — source
- `DotExtensions/MsExtensions/Primitives/StringValuesIsNullExtensions.cs` — source
- `DotExtensions.Tests/MsExtensions/Primitives/StringValues/` — target directory (needs creation)

## Dependencies

**Blocked by** — None. Can start immediately.
