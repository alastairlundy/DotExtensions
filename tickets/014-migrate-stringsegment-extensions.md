---
id: 014
title: Migrate StringSegment extensions
status: ready
Depends on: []
---

## Goal

Consolidate all extension methods for `StringSegment` into a single, discoverable file to reduce cognitive load for consumers and maintainers.

## What to build

Create a new file `DotExtensions/MsExtensions/Primitives/StringSegmentExtensions.cs` and migrate all existing `StringSegment` extension methods from the `DotExtensions.MsExtensions.Primitives` hierarchy into it.

The following files should be emptied and deleted after their content is migrated:
- `DotExtensions/MsExtensions/Primitives/AsStringSegmentsExtensions.cs`
- `DotExtensions/MsExtensions/Primitives/SegmentIsNullExtensions.cs`
- `DotExtensions/MsExtensions/Primitives/SegmentReverseExtensions.cs`
- `DotExtensions/MsExtensions/Primitives/SegmentToCharsExtensions.cs`
- `DotExtensions/MsExtensions/Primitives/Cases/SegmentCapitalizationExtensions.cs`
- `DotExtensions/MsExtensions/Primitives/Cases/SegmentCaseExtensions.cs`
- `DotExtensions/MsExtensions/Primitives/Collections/SegmentEnumerableToStringExtensions.cs`
- `DotExtensions/MsExtensions/Primitives/Contains/SegmentContainsExtensions.cs`
- `DotExtensions/MsExtensions/Primitives/Contains/SegmentContainsSubsegmentsExtensions.cs`
- `DotExtensions/MsExtensions/Primitives/Removal/SegmentRemoveAndReplaceExtensions.cs`

All migrated methods must be placed in the `DotExtensions.MsExtensions.Primitives` namespace and grouped by functional purpose using `#region` blocks.

The total file line count must not exceed 1,000 lines and the number of functional `#region` blocks must not exceed 10. If these thresholds are crossed, the file must be split (as per the readability guardrail).

## Recommended Workflow

### Step 1 — Create consolidation file
Where: `DotExtensions/MsExtensions/Primitives/StringSegmentExtensions.cs`
- Create the file with the `DotExtensions.MsExtensions.Primitives` namespace.
Verify: File exists and compiles.

### Step 2 — Migrate extension methods
Where: `DotExtensions/MsExtensions/Primitives/StringSegmentExtensions.cs`
- Move methods from all identified legacy `StringSegment` files.
Verify: All methods are present in the new file.

### Step 3 — Organize with regions
Where: `DotExtensions/MsExtensions/Primitives/StringSegmentExtensions.cs`
- Wrap related methods in `#region` blocks based on functional purpose (e.g., Case manipulation, Collection operations, Containment checks).
Verify: Code is logically grouped.

### Step 4 — Apply readability guardrails
Where: `DotExtensions/MsExtensions/Primitives/StringSegmentExtensions.cs`
- Count total lines and total `#region` blocks.
- Verify that lines <= 1,000 and regions <= 10.
Verify: Guardrail criteria are met.

### Step 5 — Cleanup legacy files
Where: N/A
- Delete all identified legacy `StringSegment` files and their empty parent directories if applicable.
Verify: Files are removed from the filesystem.

### Step 6 — Final Validation
Where: N/A
- Run `dotnet build` to ensure no breaking changes were introduced.
Verify: Build succeeds.

## Context pointers

**Files** - `DotExtensions/MsExtensions/Primitives/StringSegmentExtensions.cs` (Target)
**Ledger records** - `DECISIONS-DotExtensions-ms-extensions-collapse.md#D002` (Grouping strategy), `DECISIONS-DotExtensions-ms-extensions-collapse.md#T003` (Namespace migration), `DECISIONS-DotExtensions-ms-extensions-collapse.md#T004` (File aggregation), `DECISIONS-DotExtensions-ms-extensions-collapse.md#T005` (Readability guardrail)

## Acceptance criteria

- [ ] All `StringSegment` extension methods are located in `DotExtensions/MsExtensions/Primitives/StringSegmentExtensions.cs`
- [ ] All methods use the `DotExtensions.MsExtensions.Primitives` namespace
- [ ] Methods are organized into functional `#region` blocks
- [ ] File line count is <= 1,000 and region count is <= 10
- [ ] Legacy files for `StringSegment` extensions are deleted
- [ ] Project builds successfully
