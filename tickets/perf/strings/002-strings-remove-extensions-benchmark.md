---
title: Strings - StringRemoveExtensions BDN benchmark (3x3) + finalize PR description
classification: Independent
blocked_by: [1]
parent: docs/decisions/DECISIONS-DotExtensions-performance-improvements.md
---

## Goal

Add a BDN benchmark covering all three `StringRemoveExtensions` methods across short (16 chars), medium (256 chars), and long (4096 chars) inputs, producing a 3x3 matrix, and finalize the PR description with the benchmark results table.

## What to build

Add a BDN benchmark class in `DotExtensions.Benchmarking` that covers `RemoveAll`, `RemoveFirst`, and `RemoveLast` across short (16 chars), medium (256 chars), and long (4096 chars) inputs, producing a 3x3 matrix of benchmark methods. Run the benchmark and add the results table (method x size x mean x allocated) to the PR description from ticket 1.

## Recommended Workflow

### Step 1 — Read the existing BDN benchmark project structure

Where: `DotExtensions.Benchmarking/DotExtensions.Benchmarking.csproj`

- Read existing benchmark classes to understand conventions
- Note the BDN configuration and output format

Verify: N/A

### Step 2 — Create a new benchmark class for StringRemoveExtensions

Where: `DotExtensions.Benchmarking/`

- Create a benchmark class with 9 benchmark methods (3 methods x 3 sizes)
- Use ASCII-only inputs to avoid culture-specific flakiness
- Configure BDN to measure allocation

Verify: `dotnet build DotExtensions.Benchmarking -c Release` succeeds

### Step 3 — Run the BDN benchmark and capture the results

Where: N/A

- Run the benchmark using BDN
- Capture the results table (method x size x mean x allocated)

Verify: Benchmark produces a 3x3 matrix of results

### Step 4 — Update the PR description from ticket 1 with the benchmark results table

Where: PR description

- Add the benchmark results table (method x size x mean x allocated) to the PR description
- Exclude BDN memory columns (analyzed in the BDN HTML report, not the PR)

Verify: PR description includes the benchmark results table

### Step 5 — Verify the benchmark runs successfully

Where: N/A

- Confirm the benchmark runs without errors
- Confirm the results are reasonable (no NaN or zero values)

Verify: Benchmark output is valid

## Context pointers

**Files**
- `DotExtensions.Benchmarking/DotExtensions.Benchmarking.csproj` — BDN benchmark project
- `DotExtensions/System/Strings/Removal/StringRemoveExtensions.cs` — source file being benchmarked

**Ledger records**
- `DECISIONS-DotExtensions-performance-improvements.md#T001` — first v10 strings implementation
- `DECISIONS-DotExtensions-performance-improvements.md#T004` — BDN benchmark approach (3x3 matrix)
- `DECISIONS-DotExtensions-performance-improvements.md#T005` — PR description approach
- `DECISIONS-DotExtensions-performance-improvements.md#D004` — v10 validation methodology
- `DECISIONS-DotExtensions-performance-improvements.md#D007` — v10 sequencing

## Acceptance criteria

- [ ] BDN benchmark class covers all three methods across short (16), medium (256), and long (4096) inputs (per `DECISIONS-DotExtensions-performance-improvements.md#T004`)
- [ ] Benchmark produces a 3x3 matrix of results (per `DECISIONS-DotExtensions-performance-improvements.md#T004`)
- [ ] PR description includes the benchmark results table (method x size x mean x allocated) (per `DECISIONS-DotExtensions-performance-improvements.md#T005`)
- [ ] BDN memory columns are excluded from the PR table (per `DECISIONS-DotExtensions-performance-improvements.md#T005`)
- [ ] Inputs are ASCII-only to avoid culture-specific flakiness (per `DECISIONS-DotExtensions-performance-improvements.md#T004`)

## Dependencies

**Blocked by** — 1. Requires the refactored `RemoveAll` from ticket 1.
