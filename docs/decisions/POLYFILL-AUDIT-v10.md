# Polyfill Content Audit — v10 Additive Span API

## Purpose

Audit of the `Polyfill` NuGet package (v10.11.2) coverage for each API the v10
additive Span API (T007) needs on `netstandard2.0`. Each API is recorded as:
**covered** (no action), **not covered** (work around in API design per D008),
or **partially covered** (verify the specific overload).

## Scope

The additive Span API (T007) adds `Span<char>` and `ReadOnlySpan<char>` overloads
for string extension methods in `DotExtensions.Memory`. The APIs required on
`netstandard2.0` are listed below.

## Audit Results

| API | Coverage | Notes |
|-----|----------|-------|
| `GC.AllocateUninitializedArray<T>(int, bool)` | **Covered** | Confirmed in Polyfill API list (line 419 of api_list.include.md). |
| `ArgumentOutOfRangeException.ThrowIfNegative<T>(T)` | **Covered** | Confirmed in Polyfill API list under ArgumentOutOfRangeException. |
| `ArgumentOutOfRangeException.ThrowIfGreaterThan<T>(T, T)` | **Covered** | Confirmed in Polyfill API list under ArgumentOutOfRangeException. |
| `ArgumentException.ThrowIfNullOrEmpty(string?)` | **Covered** | Confirmed in Polyfill API list under ArgumentException. Requires `<PolyArgumentExceptions>true</PolyArgumentExceptions>` (already set in DotExtensions.Memory.csproj). |
| `Span<T>` / `ReadOnlySpan<T>` types | **Covered** | Provided by `System.Memory` package (already referenced for `netstandard2.0` in DotExtensions.Memory.csproj). |
| `MemoryExtensions.IndexOf<T>(ReadOnlySpan<T>, ReadOnlySpan<T>)` | **Covered** | Provided by `System.Memory` package. |
| `MemoryExtensions.AsSpan<T>(T[])` | **Covered** | Provided by `System.Memory` package. |
| `string.AsSpan()` | **Covered** | Provided by `System.Memory` package. |
| `StringBuilder` | **Covered** | Built-in to `netstandard2.0`. No polyfill needed. |
| `string.IndexOf(string, int, StringComparison)` | **Covered** | Built-in to `netstandard2.0`. No polyfill needed. |
| `string.Contains(string, StringComparison)` | **Covered** | Built-in to `netstandard2.0`. No polyfill needed. |

## Escalations

**None.** All APIs required by the v10 additive Span API are covered by the
existing `Polyfill` package (v10.11.2) and/or the existing `System.Memory`
package reference. No new polyfill package dependencies are required.

## Out of Scope

The following are explicitly out of scope for this audit per T009:

- LINQ/enumerator allocation optimizations (deferred to v11 per T008).
- async/await internal optimizations (deferred to v11 per T008).
- Non-`char` Span types (e.g., `Span<byte>`) — not added in v10 per T007.
- `ReadOnlyMemory<char>` overloads — not added in v10 per T007.

## References

- Polyfill package API list: https://github.com/SimonCropp/Polyfill/blob/main/api_list.include.md
- Polyfill NuGet: https://www.nuget.org/packages/Polyfill/10.11.2
- Decision ledger: `DECISIONS-DotExtensions-performance-improvements.md#D008`
- Decision ledger: `DECISIONS-DotExtensions-performance-improvements.md#T009`
