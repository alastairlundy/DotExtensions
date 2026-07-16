---
title: Strings - StringRemoveExtensions refactor (RemoveAll) + PR description skeleton
classification: Independent
blocked_by: []
parent: docs/decisions/DECISIONS-DotExtensions-performance-improvements.md
---

## Goal

Refactor `StringRemoveExtensions.RemoveAll` to use a single-pass `StringBuilder` scan, eliminating the repeated `string.Remove` allocations, and prepare the PR description skeleton (What/Why/How sections, without benchmark results table).

## What to build

Rewrite `RemoveAll` in `DotExtensions/System/Strings/Removal/StringRemoveExtensions.cs` to use a single-pass scan that appends non-matching runs to a `StringBuilder` and returns `sb.ToString()`. `RemoveFirst` and `RemoveLast` retain their current `string.Remove` allocation and are addressed in a future PR. The PR description includes "What changed", "Why", and "How" sections but does not yet include the BDN benchmark results table (that is added in ticket 2).

## Recommended Workflow

### Step 1 — Read the existing RemoveAll contract

Where: `DotExtensions/System/Strings/Removal/StringRemoveExtensions.cs`

- Read the XML doc comments for `RemoveAll` to understand the documented contract
- Note the exception types and parameter validation

Verify: N/A

### Step 2 — Rewrite RemoveAll with single-pass StringBuilder scan

Where: `DotExtensions/System/Strings/Removal/StringRemoveExtensions.cs`

- Replace the `while` loop + `RemoveFirst` calls with a single-pass scan
- Use a `StringBuilder` to append non-matching runs
- Return `sb.ToString()`

Verify: `dotnet build DotExtensions -c Release` succeeds

### Step 3 — Add minimal invariant tests for RemoveAll

Where: `DotExtensions.Tests/`

- Add tests that assert the documented contract for `RemoveAll`
- Cover null/empty throws, same return values, and basic removal scenarios

Verify: `dotnet test DotExtensions.Tests` passes

### Step 4 — Write PR description skeleton

Where: PR description

- Write "What changed", "Why", and "How" sections
- Note that the benchmark results table is pending (ticket 2)

Verify: N/A

### Step 5 — Verify the existing AOT build passes

Where: `DotExtensions.AotTests/DotExtensions.AotTests.csproj`

- Run `dotnet build DotExtensions.AotTests -c Release`
- Confirm no new AOT warnings are introduced

Verify: AOT build succeeds with no new warnings

## Context pointers

**Files**
- `DotExtensions/System/Strings/Removal/StringRemoveExtensions.cs` — source file to refactor
- `DotExtensions.AotTests/DotExtensions.AotTests.csproj` — AOT build gate

**Ledger records**
- `DECISIONS-DotExtensions-performance-improvements.md#T001` — first v10 strings implementation (this file)
- `DECISIONS-DotExtensions-performance-improvements.md#T002` — refactor approach (RemoveAll rewrite)
- `DECISIONS-DotExtensions-performance-improvements.md#T005` — PR description approach
- `DECISIONS-DotExtensions-performance-improvements.md#D001` — session goal (v10 perf wins)
- `DECISIONS-DotExtensions-performance-improvements.md#D005` — v10 vs v11 scope allocation
- `DECISIONS-DotExtensions-performance-improvements.md#D006` — bug standard for v10 behavior changes
- `DECISIONS-DotExtensions-performance-improvements.md#D007` — v10 sequencing (strings first)
- `DECISIONS-DotExtensions-performance-improvements.md#D009` — v10 test coverage expectations
- `DECISIONS-DotExtensions-performance-improvements.md#D010` — v10 AOT compatibility expectations
- `DECISIONS-DotExtensions-performance-improvements.md#D004` — v10 validation methodology

## Acceptance criteria

- [ ] `RemoveAll` uses a single-pass `StringBuilder` scan (per `DECISIONS-DotExtensions-performance-improvements.md#T002`)
- [ ] `RemoveFirst` and `RemoveLast` are unchanged (per `DECISIONS-DotExtensions-performance-improvements.md#T002`)
- [ ] Observable behavior is preserved: null/empty throws, same return values (per `DECISIONS-DotExtensions-performance-improvements.md#D006`)
- [ ] Existing AOT build passes with no new warnings (per `DECISIONS-DotExtensions-performance-improvements.md#D010`)
- [ ] Minimal invariant tests for `RemoveAll` pass (per `DECISIONS-DotExtensions-performance-improvements.md#D009`)
- [ ] PR description includes "What changed", "Why", and "How" sections (per `DECISIONS-DotExtensions-performance-improvements.md#T005`)

## Dependencies

**Blocked by** — None. Can start immediately.
