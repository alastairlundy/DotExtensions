---
title: SegmentIsNull Tests
classification: Independent
blocked_by: []
parent: TESTS_PRD.md
---

## Goal

Add unit tests for `SegmentIsNullExtensions` to verify correct detection of null, empty, and whitespace-only StringSegment values.

## What to build

Create `DotExtensions.Tests/MsExtensions/Primitives/SegmentIsNullTests.cs` testing `IsEmpty(this StringSegment?)`, `IsNullOrEmpty(this StringSegment?)`, and `IsNullOrWhiteSpace(this StringSegment?)`.

Test scenarios:
- `IsEmpty` — default StringSegment (empty), StringSegment with content, null StringSegment
- `IsNullOrEmpty` — null, empty, whitespace-only, and non-empty StringSegment values
- `IsNullOrWhiteSpace` — null, empty, whitespace-only, and non-empty StringSegment values
- Edge: StringSegment with a single whitespace character

Follow existing patterns: `[Test]`, `async Task`, `await Assert.That(...)`. Use `[MethodDataSource]` for parameterized data covering combinations of null/empty/whitespace/non-empty.

## Acceptance criteria

- [ ] `IsEmpty` returns true for default/empty StringSegment and false for non-empty segments
- [ ] `IsNullOrEmpty` returns true for null and empty segments, false for whitespace-only and non-empty
- [ ] `IsNullOrWhiteSpace` returns true for null, empty, and whitespace-only segments, false for non-empty
- [ ] All tests pass on all target frameworks (net8.0, net9.0, net10.0)

## Context pointers

**Files**
- `DotExtensions/MsExtensions/Primitives/SegmentIsNullExtensions.cs` — source
- `DotExtensions.Tests/MsExtensions/Primitives/` — target directory

## Dependencies

**Blocked by** — None. Can start immediately.
