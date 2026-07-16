---
title: IO Test Structure Refactoring
classification: Independent
blocked_by: []
parent: TESTS_PRD.md
---

## Goal

Move existing IO test files into their target subdirectories to match the PRD's prescribed directory structure, keeping the test suite organized as the IO test surface grows.

## What to build

Relocate two existing test files and update their namespaces:

1. Move `DotExtensions.Tests/IO/FileSizeExtensionTests.cs` to `DotExtensions.Tests/IO/FileSize/FileSizeExtensionTests.cs` and update its namespace from `DotExtensions.Tests.IO` to `DotExtensions.Tests.IO.FileSize`.

2. Move `DotExtensions.Tests/IO/UnixFileModeExtensionTests.cs` to `DotExtensions.Tests/IO/UnixFileMode/UnixFileModeExtensionTests.cs` and update its namespace from `DotExtensions.Tests.IO` to `DotExtensions.Tests.IO.UnixFileMode`.

The target subdirectories (`IO/FileSize/`, `IO/UnixFileMode/`) already exist and are empty. Do not modify test logic or assertions — only paths and namespaces.

## Acceptance criteria

- [ ] `FileSizeExtensionTests.cs` is in `IO/FileSize/` with namespace `DotExtensions.Tests.IO.FileSize`
- [ ] `UnixFileModeExtensionTests.cs` is in `IO/UnixFileMode/` with namespace `DotExtensions.Tests.IO.UnixFileMode`
- [ ] All tests still pass after the move
- [ ] No test logic or assertions were modified

## Context pointers

**Files**
- `DotExtensions.Tests/IO/FileSizeExtensionTests.cs` — source file to move
- `DotExtensions.Tests/IO/UnixFileModeExtensionTests.cs` — source file to move
- `DotExtensions.Tests/IO/FileSize/` — target directory (exists, empty)
- `DotExtensions.Tests/IO/UnixFileMode/` — target directory (exists, empty)

## Dependencies

**Blocked by** — None. Can start immediately.
