---
title: SegmentCapitalization Tests
classification: Independent
blocked_by: []
parent: TESTS_PRD.md
---

## Goal

Add unit tests for `SegmentCapitalizationExtensions` to verify character-level capitalization on StringSegment values (CapitalizeChar, CapitalizeChars).

## What to build

Create `DotExtensions.Tests/MsExtensions/Primitives/Cases/SegmentCapitalizationTests.cs` testing `CapitalizeChar(this StringSegment, int index)` and `CapitalizeChars(this StringSegment, int startIndex, int length)`.

Test scenarios:
- CapitalizeChar: capitalize first character, middle character, last character
- CapitalizeChar: null segment throws `ArgumentNullException`
- CapitalizeChar: out-of-range index (negative, beyond length)
- CapitalizeChars: capitalize a range in the middle
- CapitalizeChars: null segment throws `ArgumentNullException`
- CapitalizeChars: invalid startIndex or length (negative, beyond bounds)
- CapitalizeChars: empty StringSegment
- CapitalizeChars: range covering the entire segment
- Edge cases: indices at boundaries (0, length-1, length)

Follow existing patterns: `[Test]`, `async Task`, `await Assert.That(...)`, `[MethodDataSource]` for parameterized data covering index/length boundaries.

## Acceptance criteria

- [ ] `CapitalizeChar` correctly capitalizes the character at the given index
- [ ] `CapitalizeChar` throws `ArgumentNullException` for null segment
- [ ] `CapitalizeChar` throws appropriate exception for out-of-range index
- [ ] `CapitalizeChars` correctly capitalizes the specified range
- [ ] `CapitalizeChars` throws `ArgumentNullException` for null segment
- [ ] `CapitalizeChars` throws appropriate exception for invalid bounds
- [ ] All tests pass on all target frameworks (net8.0, net9.0, net10.0)

## Context pointers

**Files**
- `DotExtensions/MsExtensions/Primitives/Cases/SegmentCapitalizationExtensions.cs` — source
- `DotExtensions.Tests/MsExtensions/Primitives/Cases/` — target directory (exists, empty)
- `DotExtensions.Tests/Strings/Cases/CapitalizationTests.cs` — pattern for similar capitalization tests

## Dependencies

**Blocked by** — None. Can start immediately.
