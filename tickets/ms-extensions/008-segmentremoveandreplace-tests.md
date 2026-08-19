---
title: SegmentRemove Tests
classification: Independent
blocked_by: []
parent: TESTS_PRD.md
---

## Goal

Add unit tests for `SegmentRemoveAndReplaceExtensions` to verify all Remove overloads on StringSegment values handle boundaries, nulls, and edge cases correctly.

## What to build

Create `DotExtensions.Tests/MsExtensions/Primitives/Removal/SegmentRemoveAndReplaceTests.cs` testing the Remove overloads on StringSegment.

Test scenarios:
- `Remove(int startIndex)` — removes characters from a specified index to the end
- `Remove(int startIndex, int count)` — removes a specific range of characters
- `Remove(Index startIndex, Index endIndex)` — removes between two indices
- `Remove(Range range)` — removes a range
- Null or empty segment throws `ArgumentException`
- Negative startIndex throws `ArgumentOutOfRangeException`
- startIndex >= segment length throws `ArgumentOutOfRangeException`
- Negative or zero count throws `ArgumentOutOfRangeException`
- count >= segment length throws `ArgumentOutOfRangeException`
- Zero-length removal (startIndex == 0 for single-param overload)

Follow existing patterns: `[Test]`, `async Task`, `await Assert.That(...)`, `[MethodDataSource]` for parameterized data.

## Acceptance criteria

- [ ] All Remove overloads produce correct results for valid inputs
- [ ] All Remove overloads throw `ArgumentException` for null or empty segment
- [ ] Negative startIndex throws `ArgumentOutOfRangeException`
- [ ] startIndex >= segment length throws `ArgumentOutOfRangeException`
- [ ] Negative or zero count throws `ArgumentOutOfRangeException`
- [ ] count >= segment length throws `ArgumentOutOfRangeException`
- [ ] All tests pass on all target frameworks (net8.0, net9.0, net10.0)

## Context pointers

**Files**
- `DotExtensions/MsExtensions/Primitives/Removal/SegmentRemoveAndReplaceExtensions.cs` — source
- `DotExtensions.Tests/MsExtensions/Primitives/Removal/` — target directory (need to create)
- `DotExtensions.Tests/Strings/EscapeCharacters/EscapeCharacterRemovalTests.cs` — pattern for removal tests

## Dependencies

**Blocked by** — None. Can start immediately.
