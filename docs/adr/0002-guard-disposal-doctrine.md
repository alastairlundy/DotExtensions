# ADR 0002: Guard call sites stay free of disposal requirements

- Status: accepted
- Date: 2026-09-26

## Context

Candidate 5 of the 2026-09-25 architecture survey merges the three `ArgumentExceptionExtensions` modules into one guard module. During grilling (Decision Ledger `docs/decisions/DECISIONS-DotExtensions-v11-surface-reduction.md#T027`), the user set the doctrine that guards never construct IDisposable objects, with this driver: **a Guard call site must never acquire a disposal obligation** — the caller's object must not be placed at risk of leakage by a helper whose only job is to validate it.

The doctrine stands on its own as a caller-ergonomics rule. The verified SecureString defect stack (an undisposed temp SecureString, `SecureStrings.cs:73-78`) is supporting evidence that the materializing alternative fails in practice, but T024 removed the SecureString guards for separate reasons — the obsolete-adjacent type and predicates that either weakened or contradicted the type's threat model. The doctrine is not motivated by that removal.

## Decision

Guards in v11 do not construct disposable state. Predicate evaluation is a pure function of the arguments handed over. If a future predicate appears to require materialization, that need invalidates the member's Guard status, not the doctrine — the record must be reopened rather than the rule silently violated.

## Consequences

**Positive**

- Call sites carry no implicit `using`/dispose duty, including on throw paths — a failing guard cannot orphan a resource mid-validation.
- The guard module keeps a zero-allocation contract that readers can hold without reading bodies.

**Negative**

- Predicates over content hidden behind an IDisposable façade cannot exist as Guards; such members must be reshaped or shaped differently to stay in the module's doctrine.

**Mitigations**

- The merged guard module's documentation states the no-construction rule (implementation-session duty), so the contract is visible without this file.

## Alternatives considered

- **Allow materialization with mandatory try/finally disposal** — rejected: allocation discipline inside throw-helpers is exactly where the defect class historically lives, and the disposal chore lands in every call site's failure path.
- **No doctrine; rely on T024's deletion** — rejected: T024's reasons are type-specific to SecureString; without a doctrine the next materializing guard re-introduces the leak class with no record to appeal to.
