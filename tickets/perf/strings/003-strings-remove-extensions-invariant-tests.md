---
title: Strings - StringRemoveExtensions comprehensive invariant tests
classification: Independent
blocked_by: [1]
parent: docs/decisions/DECISIONS-DotExtensions-performance-improvements.md
---

## Goal

Add a comprehensive parameterized test suite for all three `StringRemoveExtensions` methods using TUnit `[Arguments]`, asserting the documented contract per XML doc comments and covering standard edge cases.

## What to build

Add a test class in `DotExtensions.Tests` that asserts the documented contract for `RemoveAll`, `RemoveFirst`, and `RemoveLast` per their XML doc comments, covers standard edge cases (null/empty inputs, value not found, value longer than input, value equals entire input, single occurrence, multiple occurrences), exercises each `StringComparison` value and asserts the result matches the comparison's semantics, and uses TUnit `[Arguments]` to parameterize inputs across short/medium/long strings, value positions, overlapping values, and Unicode.

## Recommended Workflow

### Step 1 — Read the XML doc comments for each method

Where: `DotExtensions/System/Strings/Removal/StringRemoveExtensions.cs`

- Read the XML doc comments for `RemoveAll`, `RemoveFirst`, and `RemoveLast`
- Note the documented contract for each method

Verify: N/A

### Step 2 — Create a test class with parameterized tests using TUnit [Arguments]

Where: `DotExtensions.Tests/`

- Create a test class for `StringRemoveExtensions`
- Use TUnit `[Arguments]` to parameterize inputs

Verify: `dotnet build DotExtensions.Tests -c Release` succeeds

### Step 3 — Add tests for standard edge cases

Where: `DotExtensions.Tests/`

- Add tests for null/empty inputs
- Add tests for value not found
- Add tests for value longer than input
- Add tests for value equals entire input
- Add tests for single occurrence
- Add tests for multiple occurrences

Verify: `dotnet test DotExtensions.Tests` passes

### Step 4 — Add tests for each StringComparison value

Where: `DotExtensions.Tests/`

- Add tests for each `StringComparison` value
- Assert the result matches the comparison's semantics

Verify: `dotnet test DotExtensions.Tests` passes

### Step 5 — Verify all tests pass

Where: N/A

- Run the full test suite
- Confirm all tests pass

Verify: `dotnet test DotExtensions.Tests` passes

## Context pointers

**Files**
- `DotExtensions.Tests/DotExtensions.Tests.csproj` — test project
- `DotExtensions/System/Strings/Removal/StringRemoveExtensions.cs` — source file being tested

**Ledger records**
- `DECISIONS-DotExtensions-performance-improvements.md#T001` — first v10 strings implementation
- `DECISIONS-DotExtensions-performance-improvements.md#T003` — test approach (parameterized tests)
- `DECISIONS-DotExtensions-performance-improvements.md#D006` — bug standard for v10 behavior changes
- `DECISIONS-DotExtensions-performance-improvements.md#D009` — v10 test coverage expectations
- `DECISIONS-DotExtensions-performance-improvements.md#D004` — v10 validation methodology

## Acceptance criteria

- [ ] Tests assert the documented contract per XML doc comments (per `DECISIONS-DotExtensions-performance-improvements.md#D006`)
- [ ] Standard edge cases are covered: null/empty inputs, value not found, value longer than input, value equals entire input, single occurrence, multiple occurrences (per `DECISIONS-DotExtensions-performance-improvements.md#T003`)
- [ ] Each `StringComparison` value is exercised and the result matches the comparison's semantics (per `DECISIONS-DotExtensions-performance-improvements.md#T003`)
- [ ] TUnit `[Arguments]` is used to parameterize inputs across short/medium/long strings, value positions, overlapping values, and Unicode (per `DECISIONS-DotExtensions-performance-improvements.md#T003`)
- [ ] Test inputs are pinned to ASCII or stable Unicode to avoid flakiness (per `DECISIONS-DotExtensions-performance-improvements.md#T003`)
- [ ] Allocation count assertions are excluded (the BDN benchmark covers allocation regression per `DECISIONS-DotExtensions-performance-improvements.md#D004`)

## Dependencies

**Blocked by** — 1. Requires the refactored `RemoveAll` from ticket 1 to verify tests pass against the new implementation.
