---
title: SegmentContains Tests
classification: Independent
blocked_by: []
parent: TESTS_PRD.md
---

## Goal

Add unit tests for `SegmentContainsExtensions` and `SegmentContainsSubsegmentsExtensions` to verify substring and delimited-subsegment detection on StringSegment values.

## What to build

Create test files in `DotExtensions.Tests/MsExtensions/Primitives/Contains/`:

1. **`SegmentContainsTests.cs`** — Test `Contains(this StringSegment, char)` and `Contains(this StringSegment, StringSegment)`. Cover:
   - Char present / not present in segment
   - Substring present / not present
   - Case sensitivity behavior
   - Empty segment
   - Null segment (throws `ArgumentNullException`)
   - Null search value

2. **`SegmentContainsSubsegmentsTests.cs`** — Test `ContainsDelimitedSubSegments(this StringSegment, char delimiter, StringSegment subSegment)`. Cover:
   - Subsegment found / not found in delimited segment
   - Different delimiters (comma, pipe, space, etc.)
   - Subsegment at start, middle, end of delimited list
   - Empty segment or empty delimiter
   - Null segment (throws `ArgumentNullException`)

Follow existing patterns: `[Test]`, `async Task`, `await Assert.That(...)`, `[MethodDataSource]` for parameterized data.

## Acceptance criteria

- [ ] `Contains(char)` returns true/false correctly for present/absent characters
- [ ] `Contains(StringSegment)` returns true/false correctly for present/absent substrings
- [ ] `Contains` throws `ArgumentNullException` for null segment
- [ ] `ContainsDelimitedSubSegments` correctly finds subsegments in delimited strings
- [ ] `ContainsDelimitedSubSegments` handles subsegment at any position in the delimited list
- [ ] `ContainsDelimitedSubSegments` throws `ArgumentNullException` for null segment
- [ ] All tests pass on all target frameworks (net8.0, net9.0, net10.0)

## Context pointers

**Files**
- `DotExtensions/MsExtensions/Primitives/Contains/SegmentContainsExtensions.cs` — source
- `DotExtensions/MsExtensions/Primitives/Contains/SegmentContainsSubsegmentsExtensions.cs` — source
- `DotExtensions.Tests/MsExtensions/Primitives/Contains/` — target directory (exists, empty)
- `DotExtensions.Tests/Strings/Contains/ContainsSpacesTests.cs` — pattern for contains tests

## Dependencies

**Blocked by** — None. Can start immediately.
