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
   - Empty segment returns false
   - Empty search value returns false

2. **`SegmentContainsSubsegmentsTests.cs`** — Test `ContainsDelimitedSubSegments(this StringSegment, char delimiter)`. Cover:
   - Segment with multiple delimited parts returns true
   - Segment with a single part (no delimiter) returns false
   - Different delimiters (comma, pipe, space, etc.)
   - Empty segment returns false
   - Whitespace-only segment returns false
   - Segment containing the delimiter but only one part returns false

Follow existing patterns: `[Test]`, `async Task`, `await Assert.That(...)`, `[MethodDataSource]` for parameterized data.

## Acceptance criteria

- [ ] `Contains(char)` returns true/false correctly for present/absent characters
- [ ] `Contains(StringSegment)` returns true/false correctly for present/absent substrings
- [ ] `Contains` returns false for empty segment
- [ ] `ContainsDelimitedSubSegments` returns true when segment contains multiple delimited parts
- [ ] `ContainsDelimitedSubSegments` returns false for single-part segments or segments without the delimiter
- [ ] `ContainsDelimitedSubSegments` handles different delimiter characters
- [ ] All tests pass on all target frameworks (net8.0, net9.0, net10.0)

## Context pointers

**Files**
- `DotExtensions/MsExtensions/Primitives/Contains/SegmentContainsExtensions.cs` — source
- `DotExtensions/MsExtensions/Primitives/Contains/SegmentContainsSubsegmentsExtensions.cs` — source
- `DotExtensions.Tests/MsExtensions/Primitives/Contains/` — target directory (exists, empty)
- `DotExtensions.Tests/Strings/Contains/ContainsSpacesTests.cs` — pattern for contains tests

## Dependencies

**Blocked by** — None. Can start immediately.
