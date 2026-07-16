---
title: SegmentRemoveAndReplace Tests
classification: Independent
blocked_by: []
parent: TESTS_PRD.md
---

## Goal

Add unit tests for `SegmentRemoveAndReplaceExtensions` to verify all Remove overloads on StringSegment values handle boundaries, nulls, and edge cases correctly.

## What to build

Create `DotExtensions.Tests/MsExtensions/Primitives/Removal/SegmentRemoveAndReplaceTests.cs` testing the Remove overloads on StringSegment.

Test scenarios:
- Remove at specific index with length
- Remove all instances of a character
- Remove all instances of a substring
- Remove from start index to end
- Null segment throws `ArgumentNullException`
- Out-of-range indices (negative, beyond length)
- Zero-length removal (no-op)
- Removal at boundaries (index 0, index at length-1)
- Replace character/substring with another
- No-op cases (character/substring not found)
- Empty segment

Follow existing patterns: `[Test]`, `async Task`, `await Assert.That(...)`, `[MethodDataSource]` for parameterized data.

## Acceptance criteria

- [ ] All Remove overloads produce correct results for valid inputs
- [ ] All Remove overloads throw `ArgumentNullException` for null segment
- [ ] Out-of-range indices produce appropriate exceptions
- [ ] Empty segment cases are handled correctly
- [ ] All tests pass on all target frameworks (net8.0, net9.0, net10.0)

## Context pointers

**Files**
- `DotExtensions/MsExtensions/Primitives/Removal/SegmentRemoveAndReplaceExtensions.cs` — source
- `DotExtensions.Tests/MsExtensions/Primitives/Removal/` — target directory (need to create)
- `DotExtensions.Tests/Strings/EscapeCharacters/EscapeCharacterRemovalTests.cs` — pattern for removal tests

## Dependencies

**Blocked by** — None. Can start immediately.
