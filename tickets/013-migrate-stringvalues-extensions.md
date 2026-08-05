---
id: 013
title: Migrate StringValues extensions
status: ready
Depends on: []
---

## Goal

Consolidate all extension methods for `StringValues` into a single, discoverable file to reduce cognitive load for consumers and maintainers.

## What to build

Create a new file `DotExtensions/MsExtensions/Primitives/StringValuesExtensions.cs` and migrate all existing `StringValues` extension methods from the `DotExtensions.MsExtensions.Primitives` hierarchy into it.

The following files should be emptied and deleted after their content is migrated:
- `DotExtensions/MsExtensions/Primitives/StringValuesIsNullExtensions.cs`
- `DotExtensions/MsExtensions/Primitives/StringValuesLengthExtensions.cs`
- `DotExtensions/MsExtensions/Primitives/StringValuesToStringExtensions.cs`

All migrated methods must be placed in the `DotExtensions.MsExtensions.Primitives` namespace and grouped by functional purpose using `#region` blocks.

## Recommended Workflow

### Step 1 — Create consolidation file
Where: `DotExtensions/MsExtensions/Primitives/StringValuesExtensions.cs`
- Create the file with the `DotExtensions.MsExtensions.Primitives` namespace.
Verify: File exists and compiles.

### Step 2 — Migrate extension methods
Where: `DotExtensions/MsExtensions/Primitives/StringValuesExtensions.cs`
- Move methods from `StringValuesIsNullExtensions.cs`
- Move methods from `StringValuesLengthExtensions.cs`
- Move methods from `StringValuesToStringExtensions.cs`
Verify: All methods are present in the new file.

### Step 3 — Organize with regions
Where: `DotExtensions/MsExtensions/Primitives/StringValuesExtensions.cs`
- Wrap related methods in `#region` blocks based on functional purpose (e.g., Null checks, Length/Counting, String conversion).
Verify: Code is logically grouped.

### Step 4 — Cleanup legacy files
Where: N/A
- Delete `StringValuesIsNullExtensions.cs`
- Delete `StringValuesLengthExtensions.cs`
- Delete `StringValuesToStringExtensions.cs`
Verify: Files are removed from the filesystem.

### Step 5 — Final Validation
Where: N/A
- Run `dotnet build` to ensure no breaking changes were introduced by the move.
Verify: Build succeeds.

## Context pointers

**Files** - `DotExtensions/MsExtensions/Primitives/StringValuesExtensions.cs` (Target)
**Ledger records** - `DECISIONS-DotExtensions-ms-extensions-collapse.md#D002` (Grouping strategy), `DECISIONS-DotExtensions-ms-extensions-collapse.md#T003` (Namespace migration), `DECISIONS-DotExtensions-ms-extensions-collapse.md#T004` (File aggregation)

## Acceptance criteria

- [ ] All `StringValues` extension methods are located in `DotExtensions/MsExtensions/Primitives/StringValuesExtensions.cs`
- [ ] All methods use the `DotExtensions.MsExtensions.Primitives` namespace
- [ ] Methods are organized into functional `#region` blocks
- [ ] Legacy files for `StringValues` extensions are deleted
- [ ] Project builds successfully
