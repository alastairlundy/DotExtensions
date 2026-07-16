---
title: Additive Span - polyfill content audit
classification: Independent
blocked_by: []
parent: docs/decisions/DECISIONS-DotExtensions-performance-improvements.md
---

## Goal

Document an audit of the `Polyfill` NuGet package coverage for each API the v10 additive Span API needs on `netstandard2.0`, recording each API as covered, not covered, or partially covered, and escalating any gap that would require a new polyfill package.

## What to build

Read the `Polyfill` NuGet package documentation and verify coverage of each API the additive Span API (ticket 6) needs on `netstandard2.0`. The audit records each API as: covered (no action), not covered (work around in API design per D008), or partially covered (verify the specific overload). Any gap requiring a new polyfill package is escalated per D008.

## Recommended Workflow

### Step 1 — Read the Polyfill NuGet package documentation

Where: N/A

- Read the `Polyfill` NuGet package documentation to understand the API surface it covers
- Note which APIs are polyfilled for `netstandard2.0`

Verify: N/A

### Step 2 — List each API the additive Span API needs on netstandard2.0

Where: N/A

- List each API the additive Span API (ticket 6) needs on `netstandard2.0`
- Include `GC.AllocateUninitializedArray` and any other modern APIs

Verify: N/A

### Step 3 — For each API, verify coverage

Where: N/A

- For each API, verify coverage: covered, not covered, or partially covered
- Note any gaps

Verify: N/A

### Step 4 — Document the audit results in a markdown file

Where: `docs/decisions/POLYFILL-AUDIT-v10.md`

- Create a markdown file documenting the audit results
- Record each API as covered, not covered, or partially covered

Verify: Audit document is created

### Step 5 — Escalate any gap that would require a new polyfill package per D008

Where: N/A

- If any gap requires a new polyfill package, escalate per D008
- Document the escalation in the audit file

Verify: Gaps are escalated or documented

## Context pointers

**Files**
- `DotExtensions.Memory/DotExtensions.Memory.csproj` — Memory package project
- `DotExtensions.Memory/Polyfills/ReadOnlyMemorySort.cs` — existing polyfill file

**Ledger records**
- `DECISIONS-DotExtensions-performance-improvements.md#T007` — additive Span API surface
- `DECISIONS-DotExtensions-performance-improvements.md#T009` — polyfill content audit
- `DECISIONS-DotExtensions-performance-improvements.md#D008` — v10 polyfill strategy

## Acceptance criteria

- [ ] Each API the additive Span API needs on `netstandard2.0` is recorded as covered, not covered, or partially covered (per `DECISIONS-DotExtensions-performance-improvements.md#T009`)
- [ ] No new polyfill package dependencies are added in v10 (per `DECISIONS-DotExtensions-performance-improvements.md#D008`)
- [ ] Any gap requiring a new polyfill package is escalated per D008 (per `DECISIONS-DotExtensions-performance-improvements.md#D008`)
- [ ] The audit results are documented in a markdown file (per `DECISIONS-DotExtensions-performance-improvements.md#T009`)

## Dependencies

**Blocked by** — None. Can start immediately.
