# ADR 0001: Safe Enumeration propagates some faults and not others

- Status: accepted
- Date: 2026-09-26

## Context

The v11 architecture survey found the safe-enumeration public interface shallow: 25 exported members over a 6-method internal implementation, split case-matching defaults between the instance and static families (case-sensitive vs case-insensitive), one doc comment contradicting its code, and an undefined contract — the `Safely*` name hid silently divergent behaviour. In v10, inaccessible entries were skipped via the BCL's `EnumerationOptions.IgnoreInaccessible`, while a bespoke `MoveNext` loop (`Internal/SafeEnumerator.cs:51-66`) swallowed `UnauthorizedAccessException`/`IOException` mid-run, stopping enumeration and yielding unannounced partial results.

Session grilling established the partial-propagation shape the user corrected the initial framing to: propagation is not total and not absent — it is *selective*, and the distinction is the contract (Decision Ledger `docs/decisions/DECISIONS-DotExtensions-v11-surface-reduction.md#D003`, `#T001`).

## Decision

v11 defines **Safe Enumeration** (per `GLOSSARY.md`) as: enumeration that skips inaccessible file-system entries by design and propagates enumeration faults instead of silently stopping mid-run.

Concretely:

- Inaccessible entries are skipped, not treated as faults: BCL `IgnoreInaccessible = true` for file/directory traversal, and catch-and-skip readiness checks for drive enumeration.
- Enumeration faults (what the underlying enumerator itself throws mid-run) propagate to the caller; the bespoke swallow loop is deleted — the BCL propagates natively (T001, T008).
- Case matching is expressed only through the BCL `MatchCasing` enum, default `CaseInsensitive`; no `ignoreCase` bool remains on the public surface (T002).

## Consequences

**Positive**

- Results are honest: a returned sequence that completes is complete; faults are signalled, not hidden.
- One internal options seam owns the `SearchOption` + `MatchCasing` → `EnumerationOptions` mapping; the public interface shrinks to 10 members (T003, T004, T007).
- The knowledge burden per call site drops: self-describing enum, unified defaults.

**Negative**

- Breaking vs v10: code relying on silent mid-run stops now observes thrown exceptions after partial consumption.
- The contract is subtle — two different kinds of "inaccessible" (skipped entry vs faulting traversal) — so incidental readers may over- or under-apply it.

**Mitigations**

- The contract is named in `GLOSSARY.md` and cited on the internal mapper's documentation (T008, T010) rather than re-tested.
- Interface shape and the removal of the swallow loop are guarded by the PublicAPI.txt-style baseline and the build/test gate (T005, T009).

## Alternatives considered

- **Keep silent partial results (v10 behaviour)** — rejected: partial results masquerade as complete; the hidden behaviour change remains one version away.
- **Completeness flag or wrapper return type** — rejected: honesty at the cost of regrowing the interface this version exists to shrink.
- **Propagate on any inaccessible entry** — rejected: defeats the module's purpose; the BCL `IgnoreInaccessible` machinery already implements the intended skip semantics.
- **Keep `ignoreCase` bool with unified default** — rejected: the internal `!ignoreCase` inversion and the ignoreCase/matchCasing twin remain as caller-facing invariants (T002).
