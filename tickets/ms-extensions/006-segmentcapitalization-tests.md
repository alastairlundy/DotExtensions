---
title: SegmentCapitalization Tests
classification: Independent
blocked_by: []
parent: TESTS_PRD.md
---

## Goal

Add unit tests for `SegmentCapitalizationExtensions` to verify character-level capitalization on StringSegment values (CapitalizeChar, CapitalizeChars).

## What to build

Create `DotExtensions.Tests/MsExtensions/Primitives/Cases/SegmentCapitalizationTests.cs` testing `CapitalizeChar(this StringSegment, int index)` and `CapitalizeChars(this StringSegment, IEnumerable<int> indices)`.

Test scenarios:
- CapitalizeChar: capitalize first character, middle character, last character
- CapitalizeChar: null or empty segment throws `ArgumentException`
- CapitalizeChar: out-of-range index (negative, beyond length) throws `ArgumentOutOfRangeException`
- CapitalizeChar: character already uppercase returns segment unchanged
- CapitalizeChars: capitalize multiple characters via index collection
- CapitalizeChars: null or empty segment throws `ArgumentException`
- CapitalizeChars: null indices throws `ArgumentNullException`
- CapitalizeChars: index of -1 throws `ArgumentException`
- CapitalizeChars: index >= segment length throws `ArgumentException`
- CapitalizeChars: range covering the entire segment
- Edge cases: indices at boundaries (0, length-1)

Follow existing patterns: `[Test]`, `async Task`, `await Assert.That(...)`, `[MethodDataSource]` for parameterized data covering index/length boundaries.

## Acceptance criteria

- [ ] `CapitalizeChar` correctly capitalizes the character at the given index
- [ ] `CapitalizeChar` throws `ArgumentException` for null or empty segment
- [ ] `CapitalizeChar` throws `ArgumentOutOfRangeException` for out-of-range index
- [ ] `CapitalizeChar` returns segment unchanged when character is already uppercase
- [ ] `CapitalizeChars` correctly capitalizes all characters at the given indices
- [ ] `CapitalizeChars` throws `ArgumentException` for null or empty segment
- [ ] `CapitalizeChars` throws `ArgumentNullException` for null indices
- [ ] `CapitalizeChars` throws `ArgumentException` for index of -1 or index >= segment length
- [ ] All tests pass on all target frameworks (net8.0, net9.0, net10.0)

## Context pointers

**Files**
- `DotExtensions/MsExtensions/Primitives/Cases/SegmentCapitalizationExtensions.cs` — source
- `DotExtensions.Tests/MsExtensions/Primitives/Cases/` — target directory (exists, empty)
- `DotExtensions.Tests/Strings/Cases/CapitalizationTests.cs` — pattern for similar capitalization tests

## Dependencies

**Blocked by** — None. Can start immediately.
