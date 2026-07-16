---
title: SegmentEnumerableToString Tests
classification: Independent
blocked_by: []
parent: TESTS_PRD.md
---

## Goal

Add unit tests for `SegmentEnumerableToStringExtensions` to verify conversion of StringSegment enumerables to delimited strings handles delimiters, empty collections, and edge cases.

## What to build

Create `DotExtensions.Tests/MsExtensions/Primitives/Collections/SegmentEnumerableToStringTests.cs` testing `ToString(this IEnumerable<StringSegment>, char)` and `ToString(this IEnumerable<StringSegment>, string)`.

Test scenarios:
- Multiple segments joined with char delimiter
- Multiple segments joined with string delimiter
- Single-element collection
- Empty collection (returns empty string)
- Null collection (throws `ArgumentNullException`)
- Segments containing the delimiter character
- Large number of segments
- Delimiters of various types (comma, pipe, newline)

Follow existing patterns: `[Test]`, `async Task`, `await Assert.That(...)`, `[MethodDataSource]` for parameterized data.

## Acceptance criteria

- [ ] Segments are joined correctly with char delimiter
- [ ] Segments are joined correctly with string delimiter
- [ ] Empty collection returns empty string
- [ ] Null collection throws `ArgumentNullException`
- [ ] All tests pass on all target frameworks (net8.0, net9.0, net10.0)

## Context pointers

**Files**
- `DotExtensions/MsExtensions/Primitives/Collections/SegmentEnumerableToStringExtensions.cs` — source
- `DotExtensions.Tests/MsExtensions/Primitives/Collections/` — target directory (exists, empty)

## Dependencies

**Blocked by** — None. Can start immediately.
