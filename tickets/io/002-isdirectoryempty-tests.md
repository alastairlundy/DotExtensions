---
title: IsDirectoryEmpty Tests
classification: Independent
blocked_by: []
parent: TESTS_PRD.md
---

## Goal

Add unit tests for `IsDirectoryEmptyExtensions` covering empty directories, non-empty directories, and non-existent directories, ensuring the extension behaves correctly as documented.

## What to build

Create `DotExtensions.Tests/IO/Directories/IsDirectoryEmptyTests.cs` testing `IsDirectoryEmptyExtensions` (`IsEmpty` and `HasFiles` extension properties on `DirectoryInfo`).

Test scenarios:
- Empty directory — verify `IsEmpty` returns true, `HasFiles` returns false
- Non-empty directory — verify `IsEmpty` returns false, `HasFiles` returns true
- Non-existent directory — verify `DirectoryNotFoundException` is thrown
- Use temporary directories created via `Directory.CreateDirectory` or `Path.Combine(Path.GetTempPath(), ...)` with unique names
- Clean up temporary directories after each test

Follow existing test patterns: `[Test]` attribute, `async Task`, `await Assert.That(...)`. Use `[MethodDataSource]` for parameterized scenarios where appropriate (e.g., different directory states).

## Acceptance criteria

- [ ] Empty directory returns true from `IsEmpty` and false from `HasFiles`
- [ ] Non-empty directory returns false from `IsEmpty` and true from `HasFiles`
- [ ] Non-existent directory throws `DirectoryNotFoundException`
- [ ] Temporary directories are cleaned up after test execution
- [ ] Tests follow TUnit patterns (`[Test]`, `async Task`, `await Assert.That(...)`)
- [ ] Tests pass on all target frameworks (net8.0, net9.0, net10.0)

## Context pointers

**Files**
- `DotExtensions/System/IO/Directories/IsDirectoryEmptyExtensions.cs` — source extension to test
- `DotExtensions.Tests/IO/Directories/` — target directory (exists, empty)
- `DotExtensions.Tests/IO/SafeEnumeration/SafeFileEnumerationTests.cs` — pattern for IO tests involving file system

## Dependencies

**Blocked by** — None. Can start immediately.
