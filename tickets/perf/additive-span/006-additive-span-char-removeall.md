---
title: Additive Span - Span<char>.RemoveAll additive overload
classification: Independent
blocked_by: [5]
parent: docs/decisions/DECISIONS-DotExtensions-performance-improvements.md
---

## Goal

Add a `ref Span<char>.RemoveAll` extension method in `DotExtensions.Memory` that returns the new length after removal, mirroring the documented contract of the main package's `string.RemoveAll`.

## What to build

Add a C# 14 extension method on `ref Span<char>` with signature `(ReadOnlySpan<char> value, StringComparison stringComparison = StringComparison.CurrentCulture)` returning `int` (the new length of the span after removal). It throws `ArgumentException` when `value` is empty. The behavior mirrors the documented contract of the main package's `string.RemoveAll` per D006.

## Recommended Workflow

### Step 1 — Read the polyfill audit results from ticket 5

Where: `docs/decisions/POLYFILL-AUDIT-v10.md`

- Read the audit results to confirm no gaps
- Note any workarounds needed

Verify: N/A

### Step 2 — Read the string.RemoveAll XML doc comments

Where: `DotExtensions/System/Strings/Removal/StringRemoveExtensions.cs`

- Read the XML doc comments for `string.RemoveAll` to understand the documented contract
- Note the exception types and parameter validation

Verify: N/A

### Step 3 — Create a new file in DotExtensions.Memory for the Span<char>.RemoveAll extension method

Where: `DotExtensions.Memory/`

- Create a new file for the `Span<char>.RemoveAll` extension method
- Use the C# 14 extension method syntax

Verify: `dotnet build DotExtensions.Memory -c Release` succeeds

### Step 4 — Implement the method with signature (ReadOnlySpan<char> value, StringComparison stringComparison = StringComparison.CurrentCulture) returning int

Where: `DotExtensions.Memory/`

- Implement the method with the specified signature
- Throw `ArgumentException` when `value` is empty
- Mirror the documented contract of `string.RemoveAll`

Verify: `dotnet build DotExtensions.Memory -c Release` succeeds

### Step 5 — Verify the existing AOT build passes

Where: `DotExtensions.AotTests/DotExtensions.AotTests.csproj`

- Run `dotnet build DotExtensions.AotTests -c Release`
- Confirm no new AOT warnings are introduced

Verify: AOT build succeeds with no new warnings

## Context pointers

**Files**
- `DotExtensions.Memory/DotExtensions.Memory.csproj` — Memory package project
- `DotExtensions/System/Strings/Removal/StringRemoveExtensions.cs` — source file for contract reference

**Ledger records**
- `DECISIONS-DotExtensions-performance-improvements.md#T007` — additive Span API surface
- `DECISIONS-DotExtensions-performance-improvements.md#T010` — Span<char>.RemoveAll type design
- `DECISIONS-DotExtensions-performance-improvements.md#D002` — package layout (Memory separate)
- `DECISIONS-DotExtensions-performance-improvements.md#D003` — v10/v11 perf strategy
- `DECISIONS-DotExtensions-performance-improvements.md#D005` — v10 vs v11 scope allocation
- `DECISIONS-DotExtensions-performance-improvements.md#D006` — bug standard for v10 behavior changes
- `DECISIONS-DotExtensions-performance-improvements.md#D007` — v10 sequencing
- `DECISIONS-DotExtensions-performance-improvements.md#D008` — v10 polyfill strategy
- `DECISIONS-DotExtensions-performance-improvements.md#D009` — v10 test coverage expectations
- `DECISIONS-DotExtensions-performance-improvements.md#D010` — v10 AOT compatibility expectations
- `DECISIONS-DotExtensions-performance-improvements.md#D004` — v10 validation methodology

## Acceptance criteria

- [ ] The method is a C# 14 extension on `ref Span<char>` with signature `(ReadOnlySpan<char> value, StringComparison stringComparison = StringComparison.CurrentCulture)` returning `int` (per `DECISIONS-DotExtensions-performance-improvements.md#T010`)
- [ ] The method throws `ArgumentException` when `value` is empty (per `DECISIONS-DotExtensions-performance-improvements.md#T010`)
- [ ] The behavior mirrors the documented contract of `string.RemoveAll` per D006 (per `DECISIONS-DotExtensions-performance-improvements.md#D006`)
- [ ] The method is in `DotExtensions.Memory`, not the main package (per `DECISIONS-DotExtensions-performance-improvements.md#D002`)
- [ ] No new polyfill package dependencies are added (per `DECISIONS-DotExtensions-performance-improvements.md#D008`)
- [ ] Existing AOT build passes with no new warnings (per `DECISIONS-DotExtensions-performance-improvements.md#D010`)

## Dependencies

**Blocked by** — 5. Requires the polyfill audit from ticket 5 to confirm no gaps.
