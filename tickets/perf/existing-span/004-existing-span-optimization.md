---
title: Existing Span - SpanCopyExtensions optimization
classification: Independent
blocked_by: []
parent: docs/decisions/DECISIONS-DotExtensions-performance-improvements.md
---

## Goal

Optimize the existing Span code in `DotExtensions.Memory/Spans/` by consolidating the `ArgumentOutOfRangeException.ThrowIf*` calls into a shared private static helper and replacing `T[] outputArray = new T[length]` in `ReadOnlySpan<T>.OptimisticCopy` with `GC.AllocateUninitializedArray<T>(length)`.

## What to build

(a) Consolidate the `ArgumentOutOfRangeException.ThrowIf*` calls into a shared private static helper used by all `CopyTo`/`OptimisticCopy` overloads. (b) Replace `T[] outputArray = new T[length]` in `ReadOnlySpan<T>.OptimisticCopy` with `GC.AllocateUninitializedArray<T>(length)`. For `netstandard2.0`, verify the existing `Polyfill` package reference covers `GC.AllocateUninitializedArray` before relying on it; if not, use conditional compilation.

## Recommended Workflow

### Step 1 — Read SpanCopyExtensions.cs and identify all ArgumentOutOfRangeException.ThrowIf* calls

Where: `DotExtensions.Memory/Spans/SpanCopyExtensions.cs`

- Read the file to understand the current validation pattern
- Identify all `ArgumentOutOfRangeException.ThrowIf*` calls

Verify: N/A

### Step 2 — Design a shared private static validation helper

Where: `DotExtensions.Memory/Spans/SpanCopyExtensions.cs`

- Design a helper that preserves the exact exception types and `paramName` values
- Ensure the helper matches the current inline checks

Verify: N/A

### Step 3 — Refactor each callsite to use the shared helper

Where: `DotExtensions.Memory/Spans/SpanCopyExtensions.cs`

- Replace each inline `ArgumentOutOfRangeException.ThrowIf*` call with the shared helper
- Verify the exception types and `paramName` values are preserved

Verify: `dotnet build DotExtensions.Memory -c Release` succeeds

### Step 4 — Verify the existing Polyfill package covers GC.AllocateUninitializedArray for netstandard2.0

Where: `DotExtensions.Memory/DotExtensions.Memory.csproj`

- Read the `Polyfill` NuGet package documentation
- Verify coverage of `GC.AllocateUninitializedArray` for `netstandard2.0`

Verify: Polyfill package covers the API, or conditional compilation is needed

### Step 5 — Replace the allocation in ReadOnlySpan<T>.OptimisticCopy with GC.AllocateUninitializedArray<T>(length)

Where: `DotExtensions.Memory/Spans/SpanCopyExtensions.cs`

- Replace `T[] outputArray = new T[length]` with `GC.AllocateUninitializedArray<T>(length)`
- Use conditional compilation if the Polyfill package does not cover the API

Verify: `dotnet build DotExtensions.Memory -c Release` succeeds

### Step 6 — Verify the existing AOT build passes

Where: `DotExtensions.AotTests/DotExtensions.AotTests.csproj`

- Run `dotnet build DotExtensions.AotTests -c Release`
- Confirm no new AOT warnings are introduced

Verify: AOT build succeeds with no new warnings

## Context pointers

**Files**
- `DotExtensions.Memory/Spans/SpanCopyExtensions.cs` — source file to optimize
- `DotExtensions.Memory/DotExtensions.Memory.csproj` — Memory package project

**Ledger records**
- `DECISIONS-DotExtensions-performance-improvements.md#T006` — existing Span optimization approach
- `DECISIONS-DotExtensions-performance-improvements.md#D005` — v10 vs v11 scope allocation
- `DECISIONS-DotExtensions-performance-improvements.md#D006` — bug standard for v10 behavior changes
- `DECISIONS-DotExtensions-performance-improvements.md#D007` — v10 sequencing
- `DECISIONS-DotExtensions-performance-improvements.md#D008` — v10 polyfill strategy

## Acceptance criteria

- [ ] All `ArgumentOutOfRangeException.ThrowIf*` calls are consolidated into a shared private static helper (per `DECISIONS-DotExtensions-performance-improvements.md#T006`)
- [ ] The shared helper preserves the exact exception types and `paramName` values of the current inline checks (per `DECISIONS-DotExtensions-performance-improvements.md#D006`)
- [ ] `ReadOnlySpan<T>.OptimisticCopy` uses `GC.AllocateUninitializedArray<T>(length)` instead of `new T[length]` (per `DECISIONS-DotExtensions-performance-improvements.md#T006`)
- [ ] The `try/catch` safety net in `TryCopyTo` and the allocation in `Resize` are unchanged (per `DECISIONS-DotExtensions-performance-improvements.md#D006`)
- [ ] The existing `Polyfill` package reference covers `GC.AllocateUninitializedArray` for `netstandard2.0`, or conditional compilation is used (per `DECISIONS-DotExtensions-performance-improvements.md#D008`)
- [ ] Existing AOT build passes with no new warnings (per `DECISIONS-DotExtensions-performance-improvements.md#D010`)

## Dependencies

**Blocked by** — None. Can start immediately.
