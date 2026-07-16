---
title: Additive Span - Span<char>.RemoveAll BDN benchmark
classification: Independent
blocked_by: [6]
parent: docs/decisions/DECISIONS-DotExtensions-performance-improvements.md
---

## Goal

Add a BDN benchmark for `Span<char>.RemoveAll` in `DotExtensions.Benchmarking` to establish the v10 baseline for this method.

## What to build

Add a BDN benchmark class in `DotExtensions.Benchmarking` that covers `Span<char>.RemoveAll` across short (16 chars), medium (256 chars), and long (4096 chars) inputs.

## Recommended Workflow

### Step 1 — Read the existing BDN benchmark project structure

Where: `DotExtensions.Benchmarking/DotExtensions.Benchmarking.csproj`

- Read existing benchmark classes to understand conventions
- Note the BDN configuration and output format

Verify: N/A

### Step 2 — Create a new benchmark class for Span<char>.RemoveAll

Where: `DotExtensions.Benchmarking/`

- Create a benchmark class with 3 benchmark methods (short, medium, long)
- Use ASCII-only inputs to avoid culture-specific flakiness
- Configure BDN to measure allocation

Verify: `dotnet build DotExtensions.Benchmarking -c Release` succeeds

### Step 3 — Run the BDN benchmark and verify it produces the expected output

Where: N/A

- Run the benchmark using BDN
- Confirm the benchmark runs without errors
- Confirm the results are reasonable (no NaN or zero values)

Verify: Benchmark output is valid

## Context pointers

**Files**
- `DotExtensions.Benchmarking/DotExtensions.Benchmarking.csproj` — BDN benchmark project
- `DotExtensions.Memory/DotExtensions.Memory.csproj` — Memory package project

**Ledger records**
- `DECISIONS-DotExtensions-performance-improvements.md#T007` — additive Span API surface
- `DECISIONS-DotExtensions-performance-improvements.md#T010` — Span<char>.RemoveAll type design
- `DECISIONS-DotExtensions-performance-improvements.md#D004` — v10 validation methodology
- `DECISIONS-DotExtensions-performance-improvements.md#D007` — v10 sequencing

## Acceptance criteria

- [ ] BDN benchmark class covers `Span<char>.RemoveAll` across short (16), medium (256), and long (4096) inputs (per `DECISIONS-DotExtensions-performance-improvements.md#D004`)
- [ ] Benchmark runs successfully and produces the expected output (per `DECISIONS-DotExtensions-performance-improvements.md#D007`)

## Dependencies

**Blocked by** — 6. Requires the `Span<char>.RemoveAll` method from ticket 6.
