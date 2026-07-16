---
title: Additive Span - Span<char>.RemoveAll tests (public API + AOT runtime)
classification: Independent
blocked_by: [6]
parent: docs/decisions/DECISIONS-DotExtensions-performance-improvements.md
---

## Goal

Add a public API test for `Span<char>.RemoveAll` that exercises the API as a consumer would, and an AOT runtime test in `DotExtensions.AotTests` that exercises the public API under NativeAOT.

## What to build

Add a public API test in `DotExtensions.Tests` that exercises `Span<char>.RemoveAll` as a consumer would, asserting the documented contract. Add an AOT runtime test in `DotExtensions.AotTests` that exercises the public API under NativeAOT.

## Recommended Workflow

### Step 1 — Read the Span<char>.RemoveAll XML doc comments

Where: `DotExtensions.Memory/`

- Read the XML doc comments for `Span<char>.RemoveAll` to understand the documented contract
- Note the exception types and parameter validation

Verify: N/A

### Step 2 — Create a public API test in DotExtensions.Tests

Where: `DotExtensions.Tests/`

- Create a public API test that exercises `Span<char>.RemoveAll` as a consumer would
- Assert the documented contract

Verify: `dotnet test DotExtensions.Tests` passes

### Step 3 — Create an AOT runtime test in DotExtensions.AotTests

Where: `DotExtensions.AotTests/`

- Create an AOT runtime test that exercises the public API under NativeAOT
- Mirror the public API test

Verify: `dotnet test DotExtensions.AotTests` passes

### Step 4 — Verify both tests pass

Where: N/A

- Run the full test suite
- Confirm both tests pass

Verify: `dotnet test` passes

## Context pointers

**Files**
- `DotExtensions.Tests/DotExtensions.Tests.csproj` — test project
- `DotExtensions.AotTests/DotExtensions.AotTests.csproj` — AOT test project
- `DotExtensions.Memory/DotExtensions.Memory.csproj` — Memory package project

**Ledger records**
- `DECISIONS-DotExtensions-performance-improvements.md#T007` — additive Span API surface
- `DECISIONS-DotExtensions-performance-improvements.md#T010` — Span<char>.RemoveAll type design
- `DECISIONS-DotExtensions-performance-improvements.md#D004` — v10 validation methodology
- `DECISIONS-DotExtensions-performance-improvements.md#D009` — v10 test coverage expectations
- `DECISIONS-DotExtensions-performance-improvements.md#D010` — v10 AOT compatibility expectations

## Acceptance criteria

- [ ] Public API test exercises `Span<char>.RemoveAll` as a consumer would, asserting the documented contract (per `DECISIONS-DotExtensions-performance-improvements.md#D009`)
- [ ] AOT runtime test exercises the public API under NativeAOT (per `DECISIONS-DotExtensions-performance-improvements.md#D010`)
- [ ] Both tests pass (per `DECISIONS-DotExtensions-performance-improvements.md#D004`)

## Dependencies

**Blocked by** — 6. Requires the `Span<char>.RemoveAll` method from ticket 6.
