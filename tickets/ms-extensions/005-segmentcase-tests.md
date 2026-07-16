---
title: SegmentCase Tests
classification: Independent
blocked_by: []
parent: TESTS_PRD.md
---

## Goal

Add unit tests for `SegmentCaseExtensions` to verify case detection on StringSegment values (IsUpperCase, IsLowerCase).

## What to build

Create `DotExtensions.Tests/MsExtensions/Primitives/Cases/SegmentCaseTests.cs` testing `IsUpperCase(this StringSegment)` and `IsLowerCase(this StringSegment)`.

Test scenarios:
- All-uppercase string (e.g., "HELLO") — IsUpperCase true, IsLowerCase false
- All-lowercase string (e.g., "hello") — IsUpperCase false, IsLowerCase true
- Mixed case (e.g., "Hello") — both false
- Empty StringSegment — both false (or define expected behavior)
- Null StringSegment — should throw `ArgumentNullException`
- Strings with non-letter characters (digits, symbols) — define expected behavior

Follow existing patterns: `[Test]`, `async Task`, `await Assert.That(...)`, `[MethodDataSource]` for parameterized data.

## Acceptance criteria

- [ ] `IsUpperCase` returns true for all-uppercase, false for mixed/lowercase/empty
- [ ] `IsLowerCase` returns true for all-lowercase, false for mixed/uppercase/empty
- [ ] Both methods throw `ArgumentNullException` for null StringSegment
- [ ] All tests pass on all target frameworks (net8.0, net9.0, net10.0)

## Context pointers

**Files**
- `DotExtensions/MsExtensions/Primitives/Cases/SegmentCaseExtensions.cs` — source
- `DotExtensions.Tests/MsExtensions/Primitives/Cases/` — target directory (exists, empty)
- `DotExtensions.Tests/Strings/Cases/CapitalizationTests.cs` — pattern for case-related tests

## Dependencies

**Blocked by** — None. Can start immediately.
