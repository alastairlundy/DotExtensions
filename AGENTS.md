# Repository overview

Single C# extension-method library (NuGet: `DotExtensions`). Pure extension-methods library - do not add new interfaces or implementations (stated in README.md Roadmap).

Project layout is **flat at repo root**, not `src/`/`tests/` (those exist only as virtual solution folders in `DotExtensions.sln`):

- `DotExtensions/` - the one and only library project (formerly multiple packages; `DotExtensions.Memory` content now lives in its `DotExtensions/Memory/` namespace)
- `DotExtensions.Tests/` - unit tests
- `DotExtensions.Benchmarking/` - BenchmarkDotNet suite (see `DotExtensions.Benchmarking/README.md`)
- `DotExtensions.AotTests/` - NativeAOT consumption console (see its README)
- `tickets/` - numbered implementation tickets currently driving the v11 surface-reduction work

## Toolchain (verified, differs from generic C# defaults)

- **Single TFM: `net10.0` only** across all projects. Do not add multi-TFM guards or `netstandard` fallbacks; they were removed deliberately (see PackageReleaseNotes in `DotExtensions/DotExtensions.csproj`).
- `LangVersion 14` required already by the library - do not lower it (README: consumers from v9.0 on also need C# 14).
- `ImplicitUsings` disabled; `Nullable` enabled.
- `global.json` does **not** pin an SDK version (`rollForward: latestPatch`, prerelease allowed). It sets `test.runner` to Microsoft.Testing.Platform (MTP).
- Package versions are centralized in `Directory.Packages.props` (CPM) - bump versions there, not in csproj files.
- `.editorconfig` lives at `DotExtensions/.editorconfig`, not repo root, and is minimal.

## Build / test commands (exactly what CI runs: `.github/workflows/build-test.yml`)

```sh
dotnet restore
dotnet build -c Release --no-restore
dotnet test -c Release --no-build
```

- Tests use **TUnit** through Microsoft.Testing.Platform - not xUnit/NUnit/VSTest. Assertions: `await Assert.That(...)`. For filter/trait syntax to run a single test, use the `run-tests` skill rather than guessing MTP flags.
- **Formatting is not enforced in CI** (`build-test.yml` says so explicitly). Run `dotnet format --verify-no-changes` locally as a pre-merge gate.
- There is no CodeQL/format workflow step to copy locally; the main CI only restores, builds, and tests.

## Packaging / release gotchas

- `DotExtensions.csproj` has `GeneratePackageOnBuild=true` - every Release build of the library emits a `.nupkg` in `bin/`.
- The `<Version>` and `<PackageReleaseNotes>` live **inside `DotExtensions.csproj`** and must both be updated on release, along with `CHANGELOG.md` (see `docs/Building.md` "Building for Release").
- `ExposeInternalsToFirstPartyProjects` defaults to **true** so `DotExtensions.Tests` / `DotExtensions.Benchmarking` get `InternalsVisibleTo`. Release/publish builds set `-p:ExposeInternalsToFirstPartyProjects=false` (see `publish-nuget.yml`) so those attributes don't ship in the unsigned public assembly.
- Publish (`publish-nuget.yml`) is manual-trigger only, OIDC NuGet login, builds just `DotExtensions/DotExtensions.csproj` Release.
- Versioning rules (pre-release suffix format like `11.0.0-alpha.2`, when to bump Build/Minor/Major) are in `docs/Building.md`.

## Coding constraints

- The library enables **AOT and trim analyzers** (`IsAotCompatible`, `EnableAotAnalyzer`, `IsTrimmable`, `EnableTrimAnalyzer`). Avoid reflection/dynamic patterns that raise IL warnings; `DotExtensions.AotTests` exists to validate NativeAOT compatibility:
  ```sh
  dotnet publish DotExtensions.AotTests -c Release -r win-x64 -p:PublishAot=true --self-contained true
  ```
- Localization strings come from resx-generated resources in `DotExtensions/Internal/Localizations/`.

## Domain / docs pointers

- `docs/agents/` - issue-tracker (GitHub Issues via `gh`), triage-labels (`needs-triage`, `ready-for-agent`, etc.), and domain-doc rules.
- `docs/adr/` - architectural decision records (e.g. safe-enumeration contract, guard disposal doctrine).
- `GLOSSARY.md` - domain terminology; consult before naming anything new.
- PRs must follow `.github/PULL_REQUEST_TEMPLATE.md`, including the **AI usage disclosure** section. One branch per unrelated change (see CONTRIBUTING.md).

## Benchmarking

`DotExtensions.Benchmarking` targets `net10.0`. Default runs use the executing SDK's TFM; run with a specific TFM: `dotnet run -c Release -f net10.0`.

- **`--quick`**: development / verify-no-regression runs. Only `"Short"`-classified benchmarks (StringRemove, VersionParse, RandomFileRetrieval, SafeFileEnumeration); completes in under 30 seconds.
- **Full run** (no `--quick`): before merging, includes `"Medium"` benchmarks (DigitCounting at 1M params).
- Filter by class/method with BDN glob syntax, composable with `--quick`:
  ```sh
  dotnet run -c Release --filter *ClassName*
  dotnet run -c Release --filter *ClassName*MethodName*
  dotnet run -c Release --quick --filter *DigitCounting*
  ```
- Running all configured TFMs: `dotnet run -c Release --tfm all`.
