---
title: ArgumentExceptionStringValues Tests
classification: Independent
blocked_by: []
parent: TESTS_PRD.md
---

## Goal

Add unit tests for `ArgumentExceptionStringValuesExtensions` to verify argument validation on StringValues parameters throws correct exceptions.

## What to build

Create `DotExtensions.Tests/MsExtensions/Primitives/Exceptions/ArgumentExceptionStringValuesTests.cs` testing `ThrowIfNullOrEmpty(this ArgumentException, StringValues)` and `ThrowIfNullOrWhiteSpace(this ArgumentException, StringValues)`.

Test scenarios:
- Non-null, non-empty StringValues — no exception thrown
- Null StringValues — throws `ArgumentNullException`
- Empty StringValues — throws `ArgumentException` (or appropriate derived type)
- Whitespace-only StringValues — throws `ArgumentException` for ThrowIfNullOrWhiteSpace
- Verify exception messages are non-empty and descriptive

Follow existing patterns: `[Test]`, `async Task`, `await Assert.That(...)`, `await Assert.ThrowsAsync<T>(...)`.

## Acceptance criteria

- [ ] `ThrowIfNullOrEmpty` passes for non-null, non-empty StringValues
- [ ] `ThrowIfNullOrEmpty` throws for null and empty StringValues
- [ ] `ThrowIfNullOrWhitespace` passes for non-null, non-empty, non-whitespace StringValues
- [ ] `ThrowIfNullOrWhitespace` throws for null, empty, and whitespace-only StringValues
- [ ] All tests pass on all target frameworks (net8.0, net9.0, net10.0)

## Context pointers

**Files**
- `DotExtensions/MsExtensions/Primitives/Exceptions/ArgumentExceptionStringValuesExtensions.cs` — source
- `DotExtensions.Tests/MsExtensions/Primitives/Exceptions/` — target directory (needs creation)

## Dependencies

**Blocked by** — None. Can start immediately.
