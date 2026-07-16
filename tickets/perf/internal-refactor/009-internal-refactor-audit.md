---
title: Internal-refactor - collection/array + boxing/conversion audit
classification: Independent
blocked_by: []
parent: docs/decisions/DECISIONS-DotExtensions-performance-improvements.md
---

## Goal

Audit the main `DotExtensions` package for collection/array allocation hotspots and boxing/conversion issues, producing a documented list of findings with per-finding PR scope for v10 internal-refactor work.

## What to build

Read the main `DotExtensions` package source files and identify: (a) collection/array allocation hotspots (e.g., unnecessary `ToList()`, `ToArray()`, `new List<T>()` where capacity is known, repeated allocations in loops), and (b) boxing/conversion issues (e.g., implicit boxing of value types, unnecessary `Convert` calls, `object` parameters where generics would avoid boxing). Document each finding with file path, line number, current behavior, proposed fix, and estimated impact. Each finding becomes a separate v10 PR per T008.

## Recommended Workflow

### Step 1 — Scan the main DotExtensions package for collection/array allocation hotspots

Where: `DotExtensions/DotExtensions/System/`, `DotExtensions/DotExtensions/MsExtensions/`

- Read source files across the main package
- Identify collection/array allocation hotspots (unnecessary `ToList()`, `ToArray()`, `new List<T>()` without capacity, repeated allocations in loops)

Verify: N/A

### Step 2 — Scan the main DotExtensions package for boxing/conversion issues

Where: `DotExtensions/DotExtensions/System/`, `DotExtensions/DotExtensions/MsExtensions/`

- Read source files across the main package
- Identify boxing/conversion issues (implicit boxing of value types, unnecessary `Convert` calls, `object` parameters where generics would avoid boxing)

Verify: N/A

### Step 3 — Document each finding with file path, line number, current behavior, proposed fix, and estimated impact

Where: `docs/decisions/INTERNAL-REFACTOR-AUDIT-v10.md`

- Create a markdown file documenting the audit results
- For each finding, record: file path, line number, current behavior, proposed fix, estimated impact

Verify: Audit document is created

### Step 4 — Exclude LINQ/enumerator allocations and async/await internal optimizations from scope

Where: N/A

- Confirm LINQ/enumerator allocations and async/await internal optimizations are excluded from v10 scope (deferred to v11 per T008)
- Note them as out-of-scope in the audit document

Verify: N/A

## Context pointers

**Files**
- `DotExtensions/DotExtensions/System/` — main package source files
- `DotExtensions/DotExtensions/MsExtensions/` — main package source files

**Ledger records**
- `DECISIONS-DotExtensions-performance-improvements.md#T008` — v10 internal-refactor category scope
- `DECISIONS-DotExtensions-performance-improvements.md#D005` — v10 vs v11 scope allocation
- `DECISIONS-DotExtensions-performance-improvements.md#D006` — bug standard for v10 behavior changes
- `DECISIONS-DotExtensions-performance-improvements.md#D007` — v10 sequencing

## Acceptance criteria

- [ ] Collection/array allocation hotspots are identified and documented (per `DECISIONS-DotExtensions-performance-improvements.md#T008`)
- [ ] Boxing/conversion issues are identified and documented (per `DECISIONS-DotExtensions-performance-improvements.md#T008`)
- [ ] Each finding includes file path, line number, current behavior, proposed fix, and estimated impact (per `DECISIONS-DotExtensions-performance-improvements.md#T008`)
- [ ] LINQ/enumerator allocations and async/await internal optimizations are excluded from v10 scope (per `DECISIONS-DotExtensions-performance-improvements.md#T008`)
- [ ] No public API surface is changed (per `DECISIONS-DotExtensions-performance-improvements.md#D005`)

## Dependencies

**Blocked by** — None. Can start immediately.
