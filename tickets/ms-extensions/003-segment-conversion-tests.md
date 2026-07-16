---
title: Segment Conversion Tests
classification: Independent
blocked_by: []
parent: TESTS_PRD.md
---

## Goal

Add unit tests for `AsStringSegmentsExtensions`, `SegmentReverseExtensions`, and `SegmentToCharsExtensions` to verify StringSegment-to-enumerable conversion, reversal, and character-array/list conversion.

## What to build

Create test files in `DotExtensions.Tests/MsExtensions/Primitives/`:

1. **`AsStringSegmentsTests.cs`** — Test `AsStringSegments(this StringSegment)` which splits a StringSegment by whitespace into `IEnumerable<StringSegment>`. Cover:
   - Normal sentence (multiple words)
   - Single word (no split)
   - Empty StringSegment
   - Whitespace-only StringSegment
   - Leading/trailing whitespace

2. **`SegmentReverseTests.cs`** — Test `Reverse(this StringSegment)` which returns a reversed string. Cover:
   - Normal string reversal
   - Single character (reversal is identity)
   - Empty StringSegment
   - Null StringSegment (should throw `ArgumentNullException`)

3. **`SegmentToCharsTests.cs`** — Test `ToCharArray(this StringSegment)` and `ToList(this StringSegment)`. Cover:
   - Conversion of non-empty StringSegment to char array and char list
   - Empty StringSegment
   - Null StringSegment (should throw `ArgumentNullException`)

Follow existing patterns: `[Test]`, `async Task`, `await Assert.That(...)`, `[MethodDataSource]` for parameterized data. Use `using Microsoft.Extensions.Primitives;` and `using DotExtensions.MsExtensions.Primitives;`.

## Acceptance criteria

- [ ] `AsStringSegments` splits correctly for normal, single-word, empty, and whitespace-only segments
- [ ] `Reverse` returns correct reversed string for normal, single-char, and empty segments
- [ ] `Reverse` throws `ArgumentNullException` for null segment
- [ ] `ToCharArray` and `ToList` return correct results for non-empty and empty segments
- [ ] `ToCharArray` and `ToList` throw `ArgumentNullException` for null segment
- [ ] All tests pass on all target frameworks (net8.0, net9.0, net10.0)

## Context pointers

**Files**
- `DotExtensions/MsExtensions/Primitives/AsStringSegmentsExtensions.cs` — source
- `DotExtensions/MsExtensions/Primitives/SegmentReverseExtensions.cs` — source
- `DotExtensions/MsExtensions/Primitives/SegmentToCharsExtensions.cs` — source
- `DotExtensions.Tests/MsExtensions/Primitives/` — target directory
- `DotExtensions.Tests/Strings/EscapeCharacters/EscapeCharacterRemovalTests.cs` — pattern for string manipulation tests

## Dependencies

**Blocked by** — None. Can start immediately.
