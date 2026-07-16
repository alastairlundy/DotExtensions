---
title: ArgumentExceptionStringSegment Tests
classification: Independent
blocked_by: []
parent: TESTS_PRD.md
---

## Goal

Add unit tests for `ArgumentExceptionStringSegmentExtensions` to verify argument validation on StringSegment parameters throws correct exceptions.

## What to build

Create `DotExtensions.Tests/MsExtensions/Primitives/Exceptions/ArgumentExceptionStringSegmentTests.cs` testing `ThrowIfNullOrEmpty(this ArgumentException, StringSegment)` and `ThrowIfNullOrWhitespace(this ArgumentException, StringSegment)`.

Test scenarios:
- Non-null, non-empty segment — no exception thrown
- Null segment — throws `ArgumentNullException`
- Empty segment — throws `ArgumentException` (or appropriate derived type)
- Whitespace-only segment — throws `ArgumentException` for ThrowIfNullOrWhitespace
- Verify exception messages are non-empty and descriptive

Follow existing patterns: `[Test]`, `async Task`, `await Assert.That(...)`, `await Assert.ThrowsAsync<T>(...)`.

## Acceptance criteria

- [ ] `ThrowIfNullOrEmpty` passes for non-null, non-empty segment
- [ ] `ThrowIfNullOrEmpty` throws for null segment
- [ ] `ThrowIfNullOrEmpty` throws for empty segment
- [ ] `ThrowIfNullOrWhitespace` passes for non-null, non-empty, non-whitespace segment
- [ ] `ThrowIfNullOrWhitespace` throws for null, empty, and whitespace-only segments
- [ ] All tests pass on all target frameworks (net8.0, net9.0, net10.0)

## Context pointers

**Files**
- `DotExtensions/MsExtensions/Primitives/Exceptions/ArgumentExceptionStringSegmentExtensions.cs` — source
- `DotExtensions.Tests/MsExtensions/Primitives/Exceptions/` — target directory (needs creation)

## Dependencies

**Blocked by** — None. Can start immediately.
