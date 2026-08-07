# Decision Ledger — DotExtensions IO enumeration seam

### [D001] — session goal

- **Driver**: the user wants a single, testable seam for IO enumeration rather than a tangled dependency web across 8 modules.
- **Resolved Answer**: "I want to untangle the IO related extensions and simplify the IO namespace and classes within DotExtensions."
- **Normalized Requirement**: The IO enumeration surface shall be refactored so that enumeration policy (`IgnoreInaccessible`, `EnumerationOptions`) lives in one place and consumer modules depend on a seam, not on each other.
- **Constraints**: None.

### [D002] — seam pattern

- **Driver**: the user wants the IO extensions to have sensible dependencies and a single policy owner for enumeration.
- **Resolved Answer**: "Option 2"
- **Normalized Requirement**: An internal adapter layer shall own `EnumerationOptions` configuration and the `IgnoreInaccessible` policy. All safe-enumeration extension methods delegate to this adapter, not to each other. The adapter is reachable via `InternalsVisibleTo` for testing.
- **Constraints**: No new public API surface. Existing extension method signatures remain unchanged.

### [D003] — adapter granularity

- **Driver**: the user wants to reduce duplication without lumping unrelated code together. File and directory enumeration share the same `EnumerationOptions` pattern; drive enumeration works differently.
- **Resolved Answer**: "Option 3"
- **Normalized Requirement**: Two internal adapter classes: `SafeEnumerator` (file and directory enumeration, owns `EnumerationOptions`) and `DriveEnumerator` (drive enumeration, no `EnumerationOptions`). Extension methods delegate accordingly.
- **Constraints**: `EnumerationOptions` setup lives once, in `SafeEnumerator`. The `ignoreCase` parameter default difference (files: `false`, directories: `true`) must be preserved in the extension wrappers, not buried in the adapter.

### [D004] — consumer module dependency direction

- **Driver**: the user wants consumer modules to be more self-reliant, with fewer dependencies and no code duplicated across them.
- **Resolved Answer**: "Option 1"
- **Normalized Requirement**: Consumer modules (`IsDirectoryEmptyExtensions`, `DrivesIsEmptyExtensions`, `GetRandomIOExtensions`) shall depend on the adapter classes directly, not on extension method modules. The public extension method API remains stable but becomes a thin wrapper layer.
- **Constraints**: The dependency web collapses to a hub-and-spoke: extension methods → adapters, and consumers → adapters. No new external dependencies for consumers beyond the adapter layer.

### [D005] — adapter method shape

- **Driver**: the user picked the option where the adapter takes a `DirectoryInfo` directly.
- **Resolved Answer**: "Option 2"
- **Normalized Requirement**: Adapter methods shall accept a `DirectoryInfo` as their primary input. Consumers instantiate `new DirectoryInfo(path)` once and pass it to the adapter. The overload chain (no args, pattern, pattern + `SearchOption` + `ignoreCase`) is preserved on the adapter.
- **Constraints**: Consumers will be slightly more verbose at call sites (one `new DirectoryInfo(path)` per call), but the adapter is more directly aligned with BCL internals and easier to unit-test. The public extension method API surface stays unchanged (still takes `string`); the extension method body becomes a thin wrapper that constructs the `DirectoryInfo` and calls the adapter.

### [D006] — public extension method file layout

- **Driver**: the user wants sub-namespaces collapsed and related methods grouped together. Currently the public extension method surface is split across 3 sub-namespaces (`DotExtensions.IO.Files`, `DotExtensions.IO.Directories`, `DotExtensions.IO.Drives`).
- **Resolved Answer**: "Option 1"
- **Normalized Requirement**: The public extension method surface shall be reorganised into the following files, under the `DotExtensions.IO` root namespace (except `PermissionExtensions.cs`, which stays in `DotExtensions.IO.Permissions`): `SafeEnumerationExtensions.cs` (file + directory enumeration wrappers, delegates to `SafeEnumerator`), `SafeDriveEnumerationExtensions.cs` (drive enumeration wrappers, delegates to `DriveEnumerator`), `IsEmptyExtensions.cs` (merges `IsDirectoryEmpty` and `DrivesIsEmpty` into a single file with multiple overloads), `GetRandomIOExtensions.cs` (unchanged), `PermissionExtensions.cs` (unchanged, remains in `DotExtensions.IO.Permissions`). The sub-namespaces `DotExtensions.IO.Files`, `DotExtensions.IO.Directories`, and `DotExtensions.IO.Drives` are removed; `DotExtensions.IO.Permissions` is retained.
- **Constraints**: The four enumeration-related files share the `DotExtensions.IO` namespace. `PermissionExtensions.cs` remains in `DotExtensions.IO.Permissions` (not collapsed to root). Consumers (IsEmpty, GetRandom) depend on adapters directly per D004. IsEmpty methods become overloads of a single `IsEmpty` extension on the appropriate receiver type, not separate method names. The existing public method signatures for `SafelyEnumerateFiles`, `SafelyEnumerateDirectories`, `SafelyGetFiles`, `SafelyGetDirectories`, `SafelyEnumerateLogicalDrives`, and `SafelyGetLogicalDrives` remain unchanged.

### [D007] — adapter namespace

- **Driver**: the user wants internal APIs clearly marked or delineated as internal.
- **Resolved Answer**: "Option 1"
- **Normalized Requirement**: Adapter classes (`SafeEnumerator`, `DriveEnumerator`) shall live in the `DotExtensions.IO.Internal` sub-namespace, in a corresponding `System/IO/Internal/` folder. Internal-ness is signalled by both the namespace name and the `internal` class access modifier on the adapter classes. The public types remain in the `DotExtensions.IO` root namespace.
- **Constraints**: The production assembly exposes the adapters to the test assembly via `InternalsVisibleTo` on the production csproj. Public types do not change namespace. The `Internal` sub-namespace is opt-in for any code that needs the adapter surface.

### [D008] — home of ToEnumerationOptions

- **Driver**: the public extension method `ToEnumerationOptions(this SearchOption, bool ignoreInaccessible)` needs a home after `SafeEnumerationExtensions.cs` becomes a thin wrapper file (D006). The adapter also needs to produce `EnumerationOptions`.
- **Resolved Answer**: "Option 1"
- **Normalized Requirement**: `ToEnumerationOptions` remains a public extension method on `SearchOption` and is placed in `SafeEnumerationExtensions.cs` alongside the file and directory enumeration wrappers. The adapter `SafeEnumerator` contains its own private helper that produces the `EnumerationOptions`; the adapter does not depend on the public extension method.
- **Constraints**: Public API surface is unchanged (per D005). No breaking change. The logic exists in two places — the public extension and the private adapter helper — but the private helper is the single owner of the actual `EnumerationOptions` construction used by the adapter.

### [D009] — test strategy

- **Driver**: the user wants the new adapter seam to be testable (D001). The adapters are `internal` but reachable via `InternalsVisibleTo` (D007). Tests must verify both the policy and the public surface.
- **Resolved Answer**: "Option 3"
- **Normalized Requirement**: Tests shall cover both layers. Direct adapter tests exercise `SafeEnumerator` and `DriveEnumerator` (via `InternalsVisibleTo`) using a `DirectoryInfo` over a temp directory, asserting enumeration results and the `EnumerationOptions` policy (including the `ignoreCase` default-difference for files vs directories). Public-surface integration tests exercise the thin extension method wrappers (e.g., `path.SafelyEnumerateFiles()`) to catch regressions in the existing API.
- **Constraints**: Tests use the TUnit framework per AGENTS.md. Adapter tests rely on `InternalsVisibleTo` to reach the `DotExtensions.IO.Internal` types. No new external test dependencies beyond what the repo already uses.

### [D010] — adapter class structure

- **Driver**: the adapters hold no mutable state and only construct `EnumerationOptions` and delegate to BCL enumeration. The question is whether they need an instance lifecycle.
- **Resolved Answer**: "Option 1"
- **Normalized Requirement**: `SafeEnumerator` and `DriveEnumerator` shall be `static` classes with `static` methods. No instance lifecycle, no DI, no internal interface. Direct tests call the static methods directly via `InternalsVisibleTo`.
- **Constraints**: No new internal interface is introduced (D002 specified a class layer, not an interface). The adapters remain concrete static types in `DotExtensions.IO.Internal`.

### [D011] — adapter method names

- **Driver**: the user picked the option that drops the "Safely" prefix from adapter method names. The adapter methods take `DirectoryInfo`/`DriveInfo` and return `FileInfo`/`DirectoryInfo`/`DriveInfo` sequences or arrays.
- **Resolved Answer**: "Option 1"
- **Normalized Requirement**: Adapter methods shall drop the "Safely" prefix. `SafeEnumerator` exposes `EnumerateFiles(DirectoryInfo, string pattern, SearchOption, bool ignoreCase)` → `IEnumerable<FileInfo>`, `GetFiles(...)` → `FileInfo[]`, `EnumerateDirectories(...)` → `IEnumerable<DirectoryInfo>`, `GetDirectories(...)` → `DirectoryInfo[]`. `DriveEnumerator` exposes `EnumerateLogicalDrives()` → `IEnumerable<DriveInfo>`, `GetLogicalDrives()` → `DriveInfo[]`. `Enumerate*` returns `IEnumerable<>`, `Get*` returns arrays. The public extension wrappers retain the "Safely" prefix (`SafelyEnumerateFiles`, etc.).
- **Constraints**: Public method names are unchanged (D002/D005). The safety signal lives on the adapter type name (`SafeEnumerator`/`DriveEnumerator`), not on each method.

## Convergence & Implementation Plan

### Decision recap (D001–D011)

| ID | Decision |
|----|----------|
| D001 | Goal: untangle IO extensions and simplify the IO namespace/classes into a single testable seam. |
| D002 | An **internal adapter layer** owns `EnumerationOptions` config and `IgnoreInaccessible` policy; extension methods delegate to it. `InternalsVisibleTo` for tests. No new public API. |
| D003 | **Two adapters**: `SafeEnumerator` (file + directory, owns `EnumerationOptions`) and `DriveEnumerator` (drives, no `EnumerationOptions`). |
| D004 | Consumer modules (`IsEmpty`, `GetRandom`) depend on the adapters **directly**, not on extension-method modules. Hub-and-spoke. |
| D005 | Adapters take a `DirectoryInfo`/`DriveInfo` directly. Public extension API stays `string`-based (static `Directory` extensions build the `DirectoryInfo`). |
| D006 | Public surface collapses to `DotExtensions.IO` root (4 enumeration files); sub-namespaces `Files`/`Directories`/`Drives` removed; `Permissions` retained as its own sub-namespace. |
| D007 | Adapters live in `DotExtensions.IO.Internal` (folder `System/IO/Internal/`). Internal-ness signalled by namespace + `internal` modifier. |
| D008 | `ToEnumerationOptions(SearchOption, bool)` stays a **public** extension on `SearchOption`, in `SafeEnumerationExtensions.cs`. Adapter has its own private helper. |
| D009 | Tests: **both** direct adapter tests (via `InternalsVisibleTo`, temp dir) and public-surface integration tests. TUnit. |
| D010 | Adapters are **static** classes with static methods. No interface, no DI. |
| D011 | Adapter methods drop "Safely": `EnumerateFiles`/`GetFiles`/`EnumerateDirectories`/`GetDirectories` + `EnumerateLogicalDrives`/`GetLogicalDrives`. Public wrappers keep "Safely". |

### Target structure

**`DotExtensions.IO` (root)**
- `SafeEnumerationExtensions.cs` — public wrappers: instance `DirectoryInfo` extensions (`SafelyEnumerateFiles`, `SafelyGetFiles`, `SafelyEnumerateDirectories`, `SafelyGetDirectories`) + static `Directory` extensions (same names, `string path`) + `ToEnumerationOptions` on `SearchOption`. Delegates to `SafeEnumerator`.
- `SafeDriveEnumerationExtensions.cs` — public wrappers: `DriveInfo` extensions (`SafelyEnumerateLogicalDrives`, `SafelyGetLogicalDrives`). Delegates to `DriveEnumerator`.
- `IsEmptyExtensions.cs` — merged `IsDirectoryEmpty` + `DrivesIsEmpty`: `IsEmpty`/`HasFiles`/`HasDirectories` properties on `DirectoryInfo` and `DriveInfo`. Calls adapters directly (D004).
- `GetRandomIOExtensions.cs` — public surface unchanged; consumer bodies rewritten to call adapters directly (D004).
- `PermissionExtensions.cs` — stays in `DotExtensions.IO.Permissions` (not collapsed to root; tangential to enumeration).

**`DotExtensions.IO.Internal` (folder `System/IO/Internal/`)**
- `SafeEnumerator.cs` — `internal static`: `EnumerateFiles`, `GetFiles`, `EnumerateDirectories`, `GetDirectories` (take `DirectoryInfo` + pattern/`SearchOption`/`ignoreCase`). Owns `EnumerationOptions` construction. Preserves `ignoreCase` default: files `false`, dirs `true` (in wrappers).
- `DriveEnumerator.cs` — `internal static`: `EnumerateLogicalDrives`, `GetLogicalDrives` (filter `IsReady && TotalSize > 0`). No `EnumerationOptions`.

### Implementation order

1. Add `DotExtensions/System/IO/Internal/SafeEnumerator.cs` (static methods + private `EnumerationOptions` helper mirroring current `ToEnumerationOptions`).
2. Add `DotExtensions/System/IO/Internal/DriveEnumerator.cs`.
3. Add `InternalsVisibleTo("<test assembly>")` to the production csproj.
4. Rewrite `SafeEnumerationExtensions.cs` (root) as thin wrappers → `SafeEnumerator`; keep `ToEnumerationOptions` public.
5. Rewrite `SafeDriveEnumerationExtensions.cs` (root) as thin wrappers → `DriveEnumerator`.
6. Merge `IsDirectoryEmptyExtensions.cs` + `DrivesIsEmptyExtensions.cs` → `IsEmptyExtensions.cs`; bodies call adapters directly.
7. Rewrite `GetRandomIOExtensions.cs` consumer bodies to call adapters directly.
8. Leave `PermissionExtensions.cs` in `DotExtensions.IO.Permissions` (retained as sub-namespace per decision); no move.
9. Delete old sub-namespace folders: `System/IO/Files/`, `System/IO/Directories/`, `System/IO/Drives/`.
10. Add tests per D009 (direct adapter + public-surface integration).
11. Run `dotnet build -c Release` + `dotnet test` + `dotnet format --verify-no-changes` (AGENTS.md).

### Open questions / notes

- **`PermissionExtensions` placement (RESOLVED)**: stays in `DotExtensions.IO.Permissions`; not collapsed to root (user decision — permissions are tangential to the enumeration seam).
- **Folder/namespace mismatch**: existing files live under `DotExtensions/System/IO/...` while namespaces are `DotExtensions.IO...` (pre-existing). This plan preserves that pattern (Internal folder = `System/IO/Internal/`); it does not rename the folder root.
- **Tangled `using`**: `GetRandomIOExtensions` currently calls `SafelyEnumerateDirectories` without an explicit `using DotExtensions.IO.Directories`; it only works via transitive/implicit usings. After the namespace collapse to root, this resolves cleanly.
- **No new public API**: all existing public signatures (`SafelyEnumerateFiles`, `SafelyGetFiles`, `SafelyEnumerateDirectories`, `SafelyGetDirectories`, `SafelyEnumerateLogicalDrives`, `SafelyGetLogicalDrives`, `IsEmpty`/`HasFiles`/`HasDirectories`, `ToEnumerationOptions`) are preserved.
