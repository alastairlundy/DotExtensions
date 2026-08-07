# Consolidated Implementation Plan — MsExtensions Collapse

## Scope Binding
- **Linked Spec**: `docs/decisions/DECISIONS-DotExtensions-ms-extensions-collapse.md`
- **Decision Ledger**: `docs/decisions/DECISIONS-DotExtensions-ms-extensions-collapse.md`
- **Notice**: This plan is a context pointer valid ONLY for the linked spec.

## Implementation Details

### src/DotExtensions/MsExtensions/StringValuesExtensions.cs
- Migrate all `StringValues` extension methods from legacy sub-namespaces to `DotExtensions.MsExtensions.Primitives` (`docs/decisions/DECISIONS-DotExtensions-ms-extensions-collapse.md#T003`).
- Group methods by functional purpose using `#region` blocks (`docs/decisions/DECISIONS-DotExtensions-ms-extensions-collapse.md#T004`).

### src/DotExtensions/MsExtensions/StringSegmentExtensions.cs
- Migrate all `StringSegment` extension methods from legacy sub-namespaces to `DotExtensions.MsExtensions.Primitives` (`docs/decisions/DECISIONS-DotExtensions-ms-extensions-collapse.md#T003`).
- Group methods by functional purpose using `#region` blocks (`docs/decisions/DECISIONS-DotExtensions-ms-extensions-collapse.md#T004`).
- Enforce readability guardrails: split if lines > 1,000 or regions > 10 (`docs/decisions/DECISIONS-DotExtensions-ms-extensions-collapse.md#T005`).

### src/DotExtensions/MsExtensions/ArgumentExceptionExtensions.cs
- Migrate all `ArgumentException` extension methods from legacy sub-namespaces to `DotExtensions.MsExtensions.Primitives` (`docs/decisions/DECISIONS-DotExtensions-ms-extensions-collapse.md#T003`).
- Group methods by functional purpose using `#region` blocks (`docs/decisions/DECISIONS-DotExtensions-ms-extensions-collapse.md#T004`).

## Ledger Reference
- `docs/decisions/DECISIONS-DotExtensions-ms-extensions-collapse.md#D001`
- `docs/decisions/DECISIONS-DotExtensions-ms-extensions-collapse.md#D002`
- `docs/decisions/DECISIONS-DotExtensions-ms-extensions-collapse.md#D003`
- `docs/decisions/DECISIONS-DotExtensions-ms-extensions-collapse.md#T001`
- `docs/decisions/DECISIONS-DotExtensions-ms-extensions-collapse.md#T002`
- `docs/decisions/DECISIONS-DotExtensions-ms-extensions-collapse.md#T003`
- `docs/decisions/DECISIONS-DotExtensions-ms-extensions-collapse.md#T004`
- `docs/decisions/DECISIONS-DotExtensions-ms-extensions-collapse.md#T005`
