# Implementation Blueprint — DotExtensions v10 Performance Improvements

## Scope Binding

**Linked Spec**: This blueprint is a context pointer for the DotExtensions
v10 performance improvement work. The functional "what" is captured in
the Decision Ledger at
`docs/decisions/DECISIONS-DotExtensions-performance-improvements.md`
(records D001–D011), supplemented by the prior internal performance and
memory audit (no standalone spec file exists; the audit is referenced
from the handoff document at
`C:\Users\alast\AppData\Local\Temp\opencode\DotExtensions-performance-handoff.md`).

**Decision Ledger**: `docs/decisions/DECISIONS-DotExtensions-performance-improvements.md`

**Cross-spec warning**: This blueprint is valid ONLY for the DotExtensions
v10 performance improvement work linked above. It must not be applied to
other specifications, libraries, or release windows without explicit
authorization. The ledger records (Dxxx, Txxx) are specific to this
spec and may not transfer to other work.

## Ledger Reference

Records cited in this blueprint (in order of citation):
- `DECISIONS-DotExtensions-performance-improvements.md#D001` — session goal
- `DECISIONS-DotExtensions-performance-improvements.md#D002` — package layout
- `DECISIONS-DotExtensions-performance-improvements.md#D003` — v10/v11 perf strategy
- `DECISIONS-DotExtensions-performance-improvements.md#D004` — v10 validation methodology
- `DECISIONS-DotExtensions-performance-improvements.md#D005` — v10 vs v11 scope allocation
- `DECISIONS-DotExtensions-performance-improvements.md#D006` — bug standard for v10 behavior changes
- `DECISIONS-DotExtensions-performance-improvements.md#D007` — v10 sequencing
- `DECISIONS-DotExtensions-performance-improvements.md#D008` — v10 polyfill strategy
- `DECISIONS-DotExtensions-performance-improvements.md#D009` — v10 test coverage expectations
- `DECISIONS-DotExtensions-performance-improvements.md#D010` — v10 AOT compatibility expectations
- `DECISIONS-DotExtensions-performance-improvements.md#D011` — session closure
- `DECISIONS-DotExtensions-performance-improvements.md#T001` — first v10 strings implementation
- `DECISIONS-DotExtensions-performance-improvements.md#T002` — StringRemoveExtensions refactor approach
- `DECISIONS-DotExtensions-performance-improvements.md#T003` — test approach for StringRemoveExtensions refactor
- `DECISIONS-DotExtensions-performance-improvements.md#T004` — BDN benchmark approach for StringRemoveExtensions
- `DECISIONS-DotExtensions-performance-improvements.md#T005` — PR description approach for StringRemoveExtensions refactor
- `DECISIONS-DotExtensions-performance-improvements.md#T006` — existing Span optimization approach
- `DECISIONS-DotExtensions-performance-improvements.md#T007` — additive Span API surface
- `DECISIONS-DotExtensions-performance-improvements.md#T008` — v10 internal-refactor category scope
- `DECISIONS-DotExtensions-performance-improvements.md#T009` — polyfill content audit
- `DECISIONS-DotExtensions-performance-improvements.md#T010` — Span<char>.RemoveAll type design

## v10 Sequence (per D007)

The v10 work follows the sequence: strings → existing Span → additive Span API → internal-refactor categories.

---

## Category 1: Strings (D005, D007)

### First deliverable: StringRemoveExtensions (T001)

The first v10 strings PR is `DotExtensions/System/Strings/Removal/StringRemoveExtensions.cs`
[T001]. The other string extension files wait for subsequent TDPs.

### Refactor approach (T002)

`RemoveAll` is rewritten with a single-pass scan that appends non-matching
runs to a `StringBuilder` and returns `sb.ToString()`. `RemoveFirst` and
`RemoveLast` retain their current `string.Remove` allocation and are
addressed in a future PR [T002].

**Constraints**:
- Behavior preservation per D006 [T002]: null/empty throws, same return values.
- The `StringBuilder` scan uses a manual loop, not `string.Replace` (no culture-aware overload on `netstandard2.0`).
- `RemoveFirst` and `RemoveLast` allocations are deferred; tracked as a follow-up TDP.

### Test approach (T003)

Tests assert the documented contract for each method (per XML doc comments),
cover standard edge cases, exercise each `StringComparison` value, and use
TUnit `[Arguments]` to parameterize inputs [T003]. Culture-specific results
are pinned to ASCII or stable Unicode to avoid flakiness. Allocation count
assertions are excluded (the BDN benchmark per D004 [T004] covers allocation
regression).

### Benchmark approach (T004)

A BDN benchmark in `DotExtensions.Benchmarking` covers all three methods
across short (16 chars), medium (256 chars), and long (4096 chars) inputs,
producing a 3 × 3 matrix [T004]. Per D004, this benchmark is the v11
baseline for `StringRemoveExtensions`. Culture-specific comparison
benchmarks are deferred.

### PR description (T005)

The PR description includes "What changed", "Why", "How" sections plus a
BDN benchmark results table (method × size × mean × allocated) [T005].
Per D006, this PR does not change behavior, so the D006 contract citation
is not required. BDN memory columns are excluded from the PR table.

---

## Category 2: Existing Span (D005, D007)

### Optimization approach (T006)

The existing Span code in `DotExtensions.Memory/Spans/` is optimized by
[T006]:
- Consolidating the `ArgumentOutOfRangeException.ThrowIf*` calls into a
  shared private static helper used by all `CopyTo`/`OptimisticCopy`
  overloads.
- Replacing `T[] outputArray = new T[length]` in
  `ReadOnlySpan<T>.OptimisticCopy` with
  `GC.AllocateUninitializedArray<T>(length)`.

**Constraints**:
- Per D006, the `try/catch` safety net in `TryCopyTo` and the allocation
  in `Resize` are deliberate behavior and stay in v10.
- Per D008, `GC.AllocateUninitializedArray` is `net8.0+`; for
  `netstandard2.0`, a conditional compilation or polyfill is required.
  The existing `Polyfill` package reference is verified per T009.
- The shared validation helper preserves the exact exception types and
  `paramName` values of the current inline checks (D006).

---

## Category 3: Additive Span API (D005, D007)

### API surface (T007)

The v10 additive API in `DotExtensions.Memory` adds `Span<char>` and
`ReadOnlySpan<char>` overloads for the string extension methods in the
strings category: `RemoveAll`, `RemoveFirst`, `RemoveLast` (from
`Removal/StringRemoveExtensions.cs`), `StringInsertExtensions`,
`StringReverseExtensions`, and the other `Removal/*` files
(`StringRemoveRangeExtensions`, `StringReplaceLastExtensions`) [T007].

**Constraints**:
- Per D008, no new polyfill packages; the existing `Polyfill` package
  reference covers `netstandard2.0` (verified per T009).
- Per D009, each new overload gets a public API test (consumer-perspective).
- Per D010, each new overload gets an AOT runtime test in
  `DotExtensions.AotTests`.
- Per D004, each new overload gets a BDN benchmark in
  `DotExtensions.Benchmarking`.
- Non-`char` types and `ReadOnlyMemory<char>` overloads are not in v10;
  they can be expanded in v11.

### Type design: Span<char>.RemoveAll (T010)

```csharp
extension(ref Span<char> source)
{
    public int RemoveAll(
        ReadOnlySpan<char> value,
        StringComparison stringComparison = StringComparison.CurrentCulture);
}
```

Returns the new length after removal; caller slices with
`source = source[..result]`. Behavior mirrors `string.RemoveAll` per
D006. The other two types in the partial loop
(`Span<char>.RemoveFirst`, `Span<char>.RemoveLast`) are deferred to
implementation; their signatures follow the same `ref Span<char>` +
`int` return pattern.

---

## Category 4: Internal-refactor (D005, D007)

### Scope (T008)

v10 internal-refactor work covers collection/array allocations and
boxing/conversion issues only [T008]. LINQ/enumerator and async/await
internal optimizations are deferred to v11. v10 updates are scoped to
remain manageable in size; each finding is a separate PR, not batched
into a single large update.

**Constraints**:
- Per D006, every v10 internal-refactor change preserves observable
  behavior; the documented-contract standard applies.
- Per D005, v10 is backwards-compatible; collection/array and
  boxing/conversion changes are internal refactors (no new public API
  surface).

---

## Cross-cutting Concerns

### Polyfill strategy (D008, T009)

No new polyfill package dependencies in v10 [D008]. The additive Span
API in T007 is verified against the existing `Polyfill` package via
document audit [T009]. Any gap is worked around in the additive API
design or escalated, not silently polyfilled with a new dependency.

### Test coverage (D009)

Test coverage bar: maintain existing + add invariant tests for changed
methods + add public API tests for new additive API [D009]. Invariant
tests assert the documented contract (per D006), not the refactor's
implementation, to avoid tautological assertions.

### AOT compatibility (D010)

The existing `DotExtensions.AotTests` AOT build (`<PublishAot>true`,
`<TreatWarningsAsErrors>true</TreatWarningsAsErrors>`,
`<EnableAotAnalyzer>true</EnableAotAnalyzer>`) is the compile-time gate
[D010]. Each new additive Span overload in T007 gets an AOT runtime
test in `DotExtensions.AotTests`. v10 internal refactors in the main
package are verified by the existing AOT build only.

### Validation methodology (D004)

The BDN benchmark project at `DotExtensions.Benchmarking` is extended
with new benchmarks for the v10 additive Span methods in
`DotExtensions.Memory` and backfilled with benchmarks for existing v10
methods in the main package that currently have no benchmarks [D004].
The v10 final state (post-implementation, with full benchmark coverage)
becomes the v11 baseline.

---

## v11 (per D003)

v11 drops `netstandard2.0` from `DotExtensions` and
`DotExtensions.Memory` and uses `net8.0+`-only APIs (modern
`MemoryExtensions`, hardware-accelerated paths) for aggressive perf
wins [D003]. v11 also takes the deferred work from v10: LINQ/enumerator
allocations, async/await internal optimizations, algorithmic
inefficiencies, and exception-as-control-flow anti-patterns. v11 may
also restructure the package layout (currently frozen per D002).

## Glossary

- **BDN** — BenchmarkDotNet, the .NET microbenchmarking library used in
  `DotExtensions.Benchmarking`.
- **Polyfill** — the NuGet package referenced by `DotExtensions.Memory`
  that provides `netstandard2.0` polyfills for modern .NET APIs.
- **TFM** — Target Framework Moniker (e.g., `netstandard2.0`, `net8.0`).
- **TDP** — Technical Decision Point, a technical choice extracted from
  the spec during the `code-implementation-grilling` workflow.
