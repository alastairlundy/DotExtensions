# Decision Ledger — DotExtensions performance improvements

## Goal

Drive measurable performance and memory-efficiency improvements across the
DotExtensions library suite, distributed across the v10 and v11 release windows.

## Conventions

- `Dxxx` — functional/governance decision.
- `Txxx` — technical implementation decision (recorded in a follow-up session
  per `code-implementation-grilling` workflow).
- Re-opens use a fresh `Dxxx` and a `Supersedes: Dxxx` line in `Constraints`.

## Records

### [D001] — session goal

- **Driver**: the user wants measurable performance wins in DotExtensions
  while controlling consumer-facing risk per release.
- **Resolved Answer**: "For v10: string optimisations; Span-based usage
  where it's not a breaking change; optimisation of existing Span usage.
  For v11: probably drop .NET Standard 2.0; more aggressively chase
  performance improvements."
- **Normalized Requirement**: v10 shall ship performance improvements that
  are SemVer-compatible (no signature changes, no removals); v11 shall ship
  performance improvements that may include breaking changes, including
  dropping `netstandard2.0` from supported target frameworks.
- **Constraints**:
  - The v10/v11 operational strategy (which categories of changes to make
    in which order) is tracked as a separate branch and is currently open
    (resolved as D003 in this session).
  - "Breaking change" includes signature changes, removals, behavior changes
    visible to consumers, and dropping target frameworks.

### [D002] — package layout (Memory vs main)

- **Driver**: the user wants v10 strictly backwards-compatible and v11 free
  to make breaking changes; the package layout must respect that boundary
  so v10 carries no consumer-facing layout risk.
- **Resolved Answer**: "Keep `DotExtensions.Memory` separate, frozen in v10;
  revisit in v11."
- **Normalized Requirement**: in v10, `DotExtensions` and `DotExtensions.Memory`
  remain two separate NuGet projects with their current shape; any v10
  additive Span/Memory API surface shall land in `DotExtensions.Memory`,
  not the main package. v11 may merge, restructure, or refactor the layout
  with a documented migration story.
- **Constraints**:
  - v10 additive Span/Memory API must go in `DotExtensions.Memory`; the main
    package's v10 scope is internal-refactor-only.
  - v11's `netstandard2.0` drop applies to both packages (or to whichever
    package the v11 strategy chooses); it is *not* a v10 action.
  - If a v11 merge forces a namespace migration, the migration story is part
    of the v11 release.

### [D003] — v10/v11 perf strategy

- **Driver**: the user wants the operational strategy to operationalize
  D001's v10/v11 boundary with the specific perf categories they named
  (string allocations, additive Span, existing Span optimization), while
  respecting D002's package-layout constraint.
- **Resolved Answer**: "Option 3 — Additive v10 (in Memory) + aggressive v11
  (drop netstandard2.0 first)."
- **Normalized Requirement**: v10 shall (a) add new `Span<T>` /
  `ReadOnlySpan<T>` / `Memory<T>` overloads in `DotExtensions.Memory`,
  (b) refactor hot paths in the main package internally (string allocations,
  Span reuse, existing Span usage) without adding new public types, and
  (c) keep the main package at zero new public types. v11 shall drop
  `netstandard2.0` from both packages and use `net8.0+`-only APIs
  (e.g., modern `MemoryExtensions`, hardware-accelerated paths) for
  aggressive perf wins; v11 may also deprecate/remove the v10 additively-
  grown Memory overloads.
- **Constraints**:
  - Per D002, v10 additive Span/Memory API lands in `DotExtensions.Memory`,
    not the main package.
  - v10 main package scope is internal-refactor-only.
  - The v11 `netstandard2.0` drop is a v11 action, not v10.
  - v11's perf wins depend on `net8.0+` APIs that have no `netstandard2.0`
    equivalent; if a future framework target must be added back, the wins
    are not portable.

### [D004] — v10 validation methodology

- **Driver**: the user wants v10 perf wins to be measurable (proof the work
  landed) and the BDN project to produce a captured v10 state that v11 can
  compare against to quantify v11 deltas.
- **Resolved Answer**: "Add benchmarks for the new v10 additive methods and
  for existing v10 methods missing benchmarks." (Option 1 — Cover new +
  backfill existing.)
- **Normalized Requirement**: `DotExtensions.Benchmarking` shall be extended
  with new benchmarks for the v10 additive Span/Memory methods in
  `DotExtensions.Memory`, and backfilled with benchmarks for existing v10
  methods in the main package that currently have no benchmarks. The v10
  final state (post-implementation, with full benchmark coverage) becomes
  the v11 baseline.
- **Constraints**:
  - v9 is out of scope; benchmarks and baselines reference v10 and v11 only.
  - v10 release is gated on benchmark coverage of the changed surface and
    the backfilled existing surface.
  - Backfill benchmarks for unchanged methods may surface existing perf
    characteristics that aren't related to the v10 work; the v10→v11
    comparison narrative must account for this.

### [D005] — v10 vs v11 scope allocation

- **Driver**: the user wants v10 to ship the categories that are both
  high-impact ("big enough change") and low-risk ("low hanging fruit")
  within a backwards-compatible release, and v11 to take the aggressive
  pass on the rest.
- **Resolved Answer**: "Option 2 but every change has to retain existing
  behaviour unless the existing behaviour has bugs."
- **Normalized Requirement**: v10 shall cover (a) string manipulation
  allocations, (b) Span/Memory additive API in `DotExtensions.Memory`,
  (c) optimization of existing Span usage, and (d) the low-hanging
  internal-refactor items from LINQ/enumerator allocations,
  collection/array allocations, boxing/conversion issues, and async/await
  internal optimizations. v11 shall take algorithmic inefficiencies,
  exception-as-control-flow, and the aggressive perf pass with `net8.0+`
  APIs. Every v10 change shall preserve existing observable behavior
  unless the existing behavior is a bug as defined in D006.
- **Constraints**:
  - v10 main package is internal-refactor-only; additive API lands in
    `DotExtensions.Memory` per D002.
  - v10 behavior changes are permitted only to fix bugs (D006).
  - "Internal-refactor" is a judgment call; a v10 change that is actually
    behavior-changing surfaces as a v10 regression, not a v11 opportunity.

### [D006] — bug standard for v10 behavior changes

- **Driver**: the user wants v10 behavior changes permitted only when the
  existing behavior is a bug, but "bug" is subjective without a standard.
- **Resolved Answer**: "Option 1 — Documented behavior is the contract;
  deviation is a bug."
- **Normalized Requirement**: a behavior change in v10 is permitted only
  when (a) the new behavior matches the method's XML doc comments / public
  documentation, and (b) the current implementation deviates from that
  documented behavior. Undocumented behavior is not protected by this
  standard; a v10 change to undocumented behavior is a breaking change
  and must be deferred to v11.
- **Constraints**:
  - The standard applies to v10 only; v11 behavior changes are governed
    by the v11 breaking-change window, not D006.
  - A v10 PR that changes behavior must cite the relevant XML doc comment
    and demonstrate the deviation in the PR description.
  - Undocumented correct behavior that gets "fixed" in v10 is a
    forward risk; the fix is technically allowed because there's no doc
    to defend the old behavior.

### [D007] — v10 sequencing

- **Driver**: the user wants v10 categories addressed in an order that
  maximizes early consumer-visible wins while keeping the additive Span
  API and internal-refactor categories from consuming v10 capacity first.
- **Resolved Answer**: "Option 1 — Impact-first (strings → existing Span →
  Span additive → internal-refactor categories)."
- **Normalized Requirement**: v10 shall address the categories in this
  order: (1) string manipulation allocations, (2) optimization of existing
  Span usage, (3) additive Span/Memory API in `DotExtensions.Memory`,
  (4) internal-refactor items from LINQ/enumerator allocations,
  collection/array allocations, boxing/conversion issues, and async/await
  internal optimizations. Each category is a discrete phase; within-
  category finding selection is a separate branch.
- **Constraints**:
  - The additive Span API lands late in v10; if a v10 capacity crunch
    hits, the new public surface is the first thing cut.
  - Internal refactors must preserve observable behavior per D006.
  - Per D004, benchmarks for each category land alongside the category
    implementation, not after.

### [D008] — v10 polyfill strategy for the Memory package

- **Driver**: the user wants the v10 additive Span/Memory API in
  `DotExtensions.Memory` to work on `netstandard2.0` without expanding the
  polyfill surface or adding new polyfill dependencies.
- **Resolved Answer**: "To the extent Option 1 is necessary we use Option 1
  but I don't expect any new/additional polyfilling packages will be needed
  for v10."
- **Normalized Requirement**: the v10 additive Span/Memory API in
  `DotExtensions.Memory` shall rely on the existing `Polyfill` NuGet
  package reference for any `netstandard2.0` polyfills. No new polyfill
  packages shall be added in v10. Custom polyfills in
  `DotExtensions.Memory/Polyfills/` are written only if the `Polyfill`
  package cannot cover a required API gap; the user expects no such gap
  for the v10 additive Span API.
- **Constraints**:
  - No new polyfill package dependencies in v10.
  - If a `netstandard2.0` API gap surfaces for the v10 additive Span API
    that the `Polyfill` package does not cover, the gap is either worked
    around in the additive API design or escalated (not silently polyfilled
    with a new dependency).
  - v11 drops `netstandard2.0` from `DotExtensions.Memory` per D003; v11
    removes the `Polyfill` package reference and any custom polyfills in
    `DotExtensions.Memory/Polyfills/` in one coordinated change.

### [D009] — v10 test coverage expectations

- **Driver**: the user wants v10 internal refactors to be verifiably
  behavior-preserving per D006, and the v10 additive Span API in
  `DotExtensions.Memory` to be regression-protected for consumers.
- **Resolved Answer**: "Option 4 — All of the above (maintain + increase
  for changed + add public API tests)."
- **Normalized Requirement**: v10 changes shall (a) maintain existing test
  coverage line/branch percentages at least constant — refactors that drop
  coverage are rejected; (b) add invariant-specific tests for every
  refactored method that exercise the contract the refactor relies on;
  (c) add consumer-perspective public API tests for every new additive
  Span/Memory overload in `DotExtensions.Memory`.
- **Constraints**:
  - Invariant-specific tests must assert the *documented contract* per D006,
    not the refactor's implementation, to avoid tautological assertions.
  - Public API tests for the additive Span overloads must use the API as
    a consumer would, not couple to internal implementation details.
  - The existing test framework is TUnit (`await Assert.That(...)`) per
    `AGENTS.md`; no framework change in v10.

### [D010] — v10 AOT compatibility expectations

- **Driver**: the user wants the v10 additive Span API in
  `DotExtensions.Memory` to be verified to work under NativeAOT, not just
  compile clean; the existing AOT build gate is necessary but not
  sufficient for new public surface.
- **Resolved Answer**: "Option 2 — Add AOT-specific runtime tests for the
  v10 additive Span API in Memory."
- **Normalized Requirement**: every new additive Span/Memory overload in
  `DotExtensions.Memory` shall have a runtime test in
  `DotExtensions.AotTests` that exercises the public API under NativeAOT.
  The existing `DotExtensions.AotTests` AOT build remains the compile-time
  gate for the entire library. v10 internal refactors in the main package
  are verified by the existing AOT build only; no explicit AOT runtime
  tests are required for refactored methods unless a specific AOT risk
  is identified.
- **Constraints**:
  - The existing `DotExtensions.AotTests` AOT build (`<PublishAot>true`,
    `<TreatWarningsAsErrors>true</TreatWarningsAsErrors>`,
    `<EnableAotAnalyzer>true</EnableAotAnalyzer>`) is the compile-time
    gate; v10 changes that introduce AOT warnings fail the build.
  - AOT runtime tests for the additive Span API must use the API as a
    consumer would, mirroring the public API tests in D009.
  - AOT runtime tests may not catch all AOT-specific paths (e.g., JIT vs
    AOT codegen differences); the AOT build gate is the primary safety
    net for compile-time AOT safety.

### [D011] — session closure

- **Driver**: the user wants to close the D-level governance session and
  move to T-level implementation grilling; the remaining open work is
  implementation-level (specific files, API signatures, CI wiring) and is
  better made in the PR process.
- **Resolved Answer**: "Option 1 — Close the session; move to
  implementation planning with D001–D010 as the governance baseline.
  Let's move to code implementation grilling."
- **Normalized Requirement**: the D-level governance session is closed;
  D001–D010 are the governance baseline for the v10 work. T-level
  (technical implementation) decisions are tracked as `Txxx` records in
  this same ledger, beginning with T001 for the first v10 strings
  implementation.
- **Constraints**:
  - Implementation-level decisions (specific files, API signatures, CI
    wiring) are made during the work, not in this grilling session.
  - Any contradiction with D001–D010 that surfaces during implementation
    is a `Supersedes: Dxxx` re-open, not a silent override.
  - The v10 sequence in D007 (strings → existing Span → Span additive →
    internal-refactor categories) governs the order of T-level work.

## T-level records (implementation)

### [T001] — first v10 strings implementation

- **Resolved Answer**: "Option 1 — `StringRemoveExtensions.cs` first, single file."
- **Normalized Requirement**: v10 strings work shall begin with
  `DotExtensions/System/Strings/Removal/StringRemoveExtensions.cs`; the
  verified allocation issues in that file are addressed in a single PR.
  The other string extension files in `DotExtensions/System/Strings/`
  wait for subsequent `Txxx` records.
- **Constraints**:
  - The refactor must preserve observable behavior per D006; tests must
    assert the documented contract, not the refactor's implementation.
  - Test coverage per D009: existing coverage is maintained, invariant-
    specific tests are added for the refactored methods, and public API
    tests are added if the refactor introduces new public surface.
  - AOT compatibility per D010: the existing `DotExtensions.AotTests`
    AOT build is the compile-time gate; this file is in the main package
    (not Memory), so no explicit AOT runtime tests are required unless a
    specific AOT risk is identified.
  - Benchmarks per D004: a BDN benchmark for `StringRemoveExtensions`
    shall be added in `DotExtensions.Benchmarking` alongside the refactor.
  - The pattern from `StringRemoveExtensions` may not transfer cleanly to
    other string files with different allocation profiles; later files
    need their own TDP.
- **Cites**: D005, D006, D007, D009, D010, D004.

### [T002] — StringRemoveExtensions refactor approach

- **Resolved Answer**: "Option 1 with a note to address RemoveFirst and
  RemoveLast in a future PR."
- **Normalized Requirement**: v10 `StringRemoveExtensions` refactor shall
  rewrite `RemoveAll` with a single-pass scan that appends non-matching
  runs to a `StringBuilder` and returns `sb.ToString()`. `RemoveFirst`
  and `RemoveLast` retain their current `string.Remove` allocation and
  are addressed in a future PR (tracked as a follow-up `Txxx`).
- **Constraints**:
  - Per D006, observable behavior is preserved: null/empty throws,
    "not found" throws for `RemoveFirst`/`RemoveLast` (those methods
    are unchanged), same return values for all three methods.
  - The `StringBuilder` scan must use a manual loop, not
    `string.Replace` (which has no culture-aware overload on
    `netstandard2.0`).
  - Per D009, invariant tests assert the documented contract (per the
    XML doc comments), not the refactor's implementation.
  - Per D004, a BDN benchmark for `RemoveAll` (and ideally the unchanged
    methods) is added in `DotExtensions.Benchmarking` alongside the
    refactor.
  - `RemoveFirst` and `RemoveLast` allocations are deferred; a follow-up
    TDP tracks their treatment in a later PR.
- **Cites**: D005, D006, D007, D009, D004.

### [T003] — test approach for StringRemoveExtensions refactor

- **Resolved Answer**: "Option 3 — Contract tests + edge case tests +
  comparison tests + parameterized inputs (TUnit `[Arguments]`)."
- **Normalized Requirement**: v10 `StringRemoveExtensions` tests shall
  (a) assert the documented contract for `RemoveAll`, `RemoveFirst`, and
  `RemoveLast` per their XML doc comments, (b) cover the standard edge
  cases (null/empty inputs, value not found, value longer than input,
  value equals entire input, single occurrence, multiple occurrences),
  (c) exercise each `StringComparison` value and assert the result
  matches the comparison's semantics, and (d) use TUnit `[Arguments]` to
  parameterize inputs across short/medium/long strings, value positions,
  overlapping values, and Unicode.
- **Constraints**:
  - Per D009, tests assert the documented contract, not the refactor's
    implementation, to avoid tautological assertions.
  - Per D006, the contract source is the XML doc comments; any behavior
    not covered by the doc comments is out of scope for the invariant
    tests.
  - Culture-specific results can vary by runtime/ICU version; test
    inputs are pinned to ASCII or stable Unicode to avoid flakiness.
  - Allocation count assertions are the BDN benchmark's job per D004,
    not the unit test's.
- **Cites**: D006, D009, D004.

### [T004] — BDN benchmark approach for StringRemoveExtensions

- **Resolved Answer**: "Option 3 — All three methods + multiple input
  sizes (short, medium, long)."
- **Normalized Requirement**: v10 `StringRemoveExtensions` BDN benchmark
  in `DotExtensions.Benchmarking` shall cover all three methods
  (`RemoveAll`, `RemoveFirst`, `RemoveLast`) across short (16 chars),
  medium (256 chars), and long (4096 chars) inputs, producing a 3 × 3
  matrix of benchmark methods.
- **Constraints**:
  - Per D004, the v10 final state (this benchmark included) becomes
    the v11 baseline; the benchmark is the regression net for v11
    changes to these methods.
  - Inputs are kept simple (ASCII, no culture-specific characters) to
    keep BDN memory columns low and avoid culture-specific flakiness.
  - Culture-specific comparison benchmarks are deferred; if a v11
    change is comparison-specific, comparison benchmarks are added
    then.
  - The benchmark class is large (9 benchmark methods); CI time
    grows accordingly.
- **Cites**: D004, D007.

### [T005] — PR description approach for StringRemoveExtensions refactor

- **Resolved Answer**: "Option 2 — Standard PR description + benchmark
  results table."
- **Normalized Requirement**: v10 `StringRemoveExtensions` refactor PR
  description shall include a "What changed", "Why", and "How" section,
  plus a BDN benchmark results table (method × size × mean × allocated)
  for the 3 × 3 matrix from T004. The table is generated by running BDN
  and pasting the output.
- **Constraints**:
  - Per D006, this PR does not change behavior, so the D006 contract
    citation is not required; the PR description states that the
    refactor preserves the documented contract.
  - The benchmark results table may become stale if benchmark inputs
    change; the PR description is updated as part of the PR checklist
    whenever the benchmark is rerun.
  - BDN memory columns are excluded from the PR table; they are
    analyzed in the BDN HTML report, not in the PR.
- **Cites**: D006, D004.

### [T006] — existing Span optimization approach

- **Resolved Answer**: "Option 3 — Both: consolidate validation + use
  `GC.AllocateUninitializedArray`."
- **Normalized Requirement**: v10 existing Span optimization in
  `DotExtensions.Memory/Spans/` shall (a) consolidate the
  `ArgumentOutOfRangeException.ThrowIf*` calls into a shared private
  static helper used by all `CopyTo`/`OptimisticCopy` overloads, and
  (b) replace `T[] outputArray = new T[length]` in
  `ReadOnlySpan<T>.OptimisticCopy` with
  `GC.AllocateUninitializedArray<T>(length)`.
- **Constraints**:
  - Per D006, the `try/catch` safety net in `TryCopyTo` and the
    allocation in `Resize` are deliberate behavior and stay in v10.
  - Per D008, `GC.AllocateUninitializedArray` is `net8.0+`; for
    `netstandard2.0`, a conditional compilation or polyfill is
    required. Verify the existing `Polyfill` package reference covers
    it before relying on it.
  - The shared validation helper must preserve the exact exception
    types and `paramName` values of the current inline checks to
    satisfy D006's documented-contract standard.
- **Cites**: D005, D006, D007, D008.

### [T007] — additive Span API surface

- **Resolved Answer**: "Option 1 — String-focused: `Span<char>` /
  `ReadOnlySpan<char>` overloads for the strings category."
- **Normalized Requirement**: v10 additive API in `DotExtensions.Memory`
  shall add `Span<char>` and `ReadOnlySpan<char>` overloads for the
  string extension methods in the strings category: `RemoveAll`,
  `RemoveFirst`, `RemoveLast` (from `Removal/StringRemoveExtensions.cs`),
  `StringInsertExtensions`, `StringReverseExtensions`, and the other
  `Removal/*` files (`StringRemoveRangeExtensions`,
  `StringReplaceLastExtensions`).
- **Constraints**:
  - Per D008, no new polyfill packages; the existing `Polyfill` package
    reference covers `netstandard2.0`.
  - Per D009, each new overload gets a public API test (consumer-
    perspective).
  - Per D010, each new overload gets an AOT runtime test in
    `DotExtensions.AotTests`.
  - Per D004, each new overload gets a BDN benchmark in
    `DotExtensions.Benchmarking`.
  - Non-`char` types (e.g., `Span<byte>` for IO) are not added in v10;
    they can be expanded in v11 if demand exists.
  - `ReadOnlyMemory<char>` overloads are not added in v10; they can be
    added in v11.
- **Cites**: D003, D005, D007, D008, D009, D010, D004.

### [T008] — v10 internal-refactor category scope

- **Resolved Answer**: "Option 3 — I don't want the v10 update(s) to
  become unwieldy in size."
- **Normalized Requirement**: v10 internal-refactor work shall cover
  collection/array allocations and boxing/conversion issues only. LINQ/
  enumerator allocations and async/await internal optimizations are
  deferred to v11. v10 updates are scoped to remain manageable in size;
  the user has explicitly named update size as a first-class concern.
- **Constraints**:
  - Per D006, every v10 internal-refactor change must preserve
    observable behavior; the documented-contract standard applies.
  - Per D005, v10 is backwards-compatible; collection/array and
    boxing/conversion changes are internal refactors (no new public
    API surface).
  - The v10 update size concern constrains per-PR scope: each
    collection/array or boxing/conversion finding is a separate PR,
    not batched into a single large update.
  - LINQ/enumerator and async/await work is deferred to v11; v11's
    aggressive pass per D003 covers these.
- **Cites**: D005, D006, D007.

### [T009] — polyfill content audit

- **Resolved Answer**: "Option 1 — Document audit."
- **Normalized Requirement**: v10 polyfill audit shall read the `Polyfill`
  NuGet package documentation and verify coverage of each API the
  additive Span API (T007) needs on `netstandard2.0`. The audit records
  each API as: covered (no action), not covered (work around in API
  design per D008), or partially covered (verify the specific overload).
  Any gap requiring a new polyfill package is escalated per D008.
- **Constraints**:
  - Per D008, no new polyfill packages in v10; gaps are worked around
    in the additive API design or escalated, not silently polyfilled
    with a new dependency.
  - The audit is a verification step, not a design step; the result is
    a recorded list of polyfill decisions before implementation.
  - The audit's confidence is bounded by the `Polyfill` package's
    documentation; if the docs are ambiguous, a prototype (T009 Option 2)
    is the fallback.
- **Cites**: D008, T007.

### [T010] — Span<char>.RemoveAll type design

- **Resolved Answer**: "Ready to move on." (Type 1 of 3 in the partial
  type loop; `Span<char>.RemoveFirst` and `Span<char>.RemoveLast`
  deferred to implementation.)
- **Normalized Requirement**: v10 `Span<char>.RemoveAll` shall be a
  C# 14 extension method on `ref Span<char>` with signature
  `(ReadOnlySpan<char> value,
    StringComparison stringComparison = StringComparison.CurrentCulture)`
  returning `int` (the new length of the span after removal). It throws
  `ArgumentException` when `value` is empty. The behavior mirrors the
  documented contract of the main package's `string.RemoveAll` per D006.
- **Constraints**:
  - The `ref Span<char>` receiver is required because removing shortens
    the span; the `int` return communicates the new length without
    forcing a reallocation.
  - Caller usage: `int newLength = source.RemoveAll(value);
    source = source[..newLength];`
  - Per D006, observable behavior matches `string.RemoveAll`: null/empty
    `value` throws, the result equals the input with all occurrences
    removed under the specified comparison.
  - The other two types in the partial loop (`Span<char>.RemoveFirst`,
    `Span<char>.RemoveLast`) are deferred to implementation; their
    signatures follow the same `ref Span<char>` + `int` return pattern
    unless implementation reveals a reason to deviate.
- **Cites**: D003, D005, D006, D007, T007, T002.
