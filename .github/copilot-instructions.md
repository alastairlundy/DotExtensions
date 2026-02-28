# Copilot agent onboarding for alastairlundy/DotExtensions

Repository summary
- Purpose: A small C# class-library of extension methods and utilities for common CLR types (strings, collections, tasks, etc.). Intended to be consumed as a library (NuGet or project reference).
- Language / stack: 100% C# (class libraries). Typical layout is one or more .csproj projects (library) and likely an accompanying test project. Builds with the .NET SDK (dotnet CLI).

High-level repo information (what an agent needs to know upfront)
- Typical important files to check first:
  - global.json — pins SDK version (if present). Always respect it.
  - *.sln — solution file(s).
  - src/  — library project(s), e.g. src/DotExtensions/*.csproj
  - tests/ or test/ — unit test project(s), e.g. tests/DotExtensions.Tests/*.csproj
  - .editorconfig — code-style and formatting rules.
  - .github/workflows/*.yml — CI steps and validators used by the repo.
  - README.md, LICENSE, CHANGELOG.md — human-facing docs.
- Expected toolchain: dotnet CLI (SDK). The repository will usually build with a modern .NET SDK (9.x or 10.x as of 2024-2026). Always check global.json first to determine exact SDK version to use.
- Size and complexity: small to medium (library-only). Builds and tests are expected to be quick (seconds to a few minutes)
- Tests use TUnit (not xUnit/NUnit). Test assertions use await ``Assert.That(...)`` syntax.

How to build and validate changes (always follow this checklist)
1. Confirm SDK and environment
   - Run: `dotnet --info`
   - If `global.json` exists at repo root, use the SDK version it specifies. If that SDK is not installed in the environment, install it or select a runner that has it.
   - Always use the dotnet CLI in PATH. Do not assume Visual Studio-only features.

2. Bootstrap / restore (always run before build)
   - Command: `dotnet restore`
   - Purpose: restore NuGet packages and SDK workload references.
   - If restore fails: inspect `nuget.config` or private feeds; set `NUGET_PACKAGES` or add credentials if private feeds are required.

3. Build
   - Command: `dotnet build --configuration Release`
   - Alternate (to be explicit): `dotnet build <sln-or-csproj> -c Release`
   - If you see compiler errors referencing analyzers or code style, run a full restore then rebuild. If Directory.Build.* pins SDK behavior, respect those settings.

4. Tests
   - Command: `dotnet test --no-build --verbosity normal`
   - If you modify build settings, run `dotnet test` (which will build as needed).
   - If test discovery hangs or tests time out, re-run with increased verbosity:
     `dotnet test --no-build --logger "trx;LogFileName=test_results.trx" --verbosity detailed`
   - If tests use xunit or NUnit and require an adapter, make sure the test project has the right package references (these are usually in the csproj).

5. Formatting and lint
   - Check for `.editorconfig`. If present, run:
     - `dotnet format --verify-no-changes` to assert code is formatted (requires dotnet-format tool; if not available, install with `dotnet tool install -g dotnet-format` or use the version specified in CI).
   - If the repository uses analyzers (StyleCop, Roslyn analyzers), they will run during `dotnet build`. Fix analyzer errors or warnings as required by CI.

6. Local CI / Workflow replication
   - Inspect `.github/workflows/*.yml` to learn the exact commands the CI runs (SDK version, matrix OS, extra steps like `dotnet pack`, `dotnet format`, `tools/install.sh`, etc.).
   - Replicate those commands locally in the same order. For actions that run on Linux runners, run locally on the matching OS or use a compatible container.

Common pitfalls and recommended mitigations
- Always check `global.json` — failing to match SDK may produce different compiler diagnostics or test behavior.
- Always run `dotnet restore` before `dotnet build` or `dotnet test`. Some CI jobs rely on `dotnet restore --use-lock-file` or custom sources.
- If CI fails with analyzers or formatting errors, run `dotnet format` and `dotnet build` locally to reproduce and fix.
- If you add new NuGet dependencies, ensure there are no private feed requirements and that package versions are compatible with the targeted framework(s) in the csproj(s).
- If a solution contains multiple target frameworks (netstandard, net6.0, net7.0, net8.0), run builds for each target when replicating CI.

Project layout and places to change code with minimal searching
- Root-level: look for solution files (*.sln), global.json, Directory.Build.props, .editorconfig, README.md, .github/
- src/ or similar: library projects (.csproj). These contain the implementation files (extension methods).
- tests/ or similar: unit test projects (.csproj). Tests validate behavior for changes — always run these after edits.
- .github/workflows/*.yml: CI validations to mirror locally.

Checks run in CI (what to replicate)
- Typical sequence in GitHub Actions for a C# library:
  1. Checkout
  2. Setup .NET SDK (uses global.json or specified SDK)
  3. `dotnet restore`
  4. `dotnet build -c Release` (with warnings-as-errors possibly enabled)
  5. `dotnet test -c Release`
  6. `dotnet format --verify-no-changes` (optional)
  7. Pack/publish steps (optional)
- Before proposing a PR, reproduce the same steps locally and ensure they succeed.

Explicit validation steps your PR must satisfy
- All unit tests pass: `dotnet test`
- No formatting or analyzer violations (run `dotnet format` and `dotnet build` to confirm)
- Build succeeds for all targeted frameworks in the csproj(s)
- If CI enforces warning-as-errors, ensure the build shows zero warnings

Developer workflow suggestions (what the agent should do)
- Always:
  - Read `global.json`, `.editorconfig`, Directory.Build.* before making changes.
  - Run `dotnet restore`, then `dotnet build -c Release`, then `dotnet test`.
  - Run `dotnet format` if formatting changes are required; keep formatting-only changes in a separate commit when possible.
  - Check `.github/workflows` to replicate CI commands and versions.
- Prefer small, focused changes per PR with tests that show the intended behavior.
- If adding public API surface, include tests and update README / changelog as appropriate.

When you need to search the repo
- Trust these instructions first. Only search the repository when:
  - global.json, Directory.Build.props, or workflows are not present or contradict these instructions
  - CI failures reference a file or command not documented here
  - you need to find relevant test files to extend or update
- Useful quick checks:
  - `ls -la` at repo root to find solution / global.json
  - `git grep -n "TODO\|HACK\|FIXME"` to locate known workarounds
  - `git grep -n "DotExtensions\|namespace"` to find project namespaces and the main files to edit

If something fails locally but CI passes (or vice versa)
- Compare SDK versions (`dotnet --info`) and OS; match CI runner via global.json or a container.
- Inspect the CI workflow file to see exact steps (actions/checkout version, setup-dotnet version).
- Re-run locally with the exact dotnet version and same arguments used in workflow.

Final guidance to the agent
- These instructions are authoritative for this repo: follow them first and only perform searches when they are incomplete or produce contradictions with repo files you find.
- Before creating a PR: run the same sequence the CI uses (restore → build → format check → test) and include the reproduction steps as part of your PR description.
- When in doubt about SDK or CI behavior, inspect `.github/workflows/*.yml` and `global.json` — those two files determine build/runtime invariants for the repo.

(If you need more precise, file-level guidance, inspect these locations in the repository in this order: global.json, *.sln, .github/workflows/*.yml, .editorconfig, src/*/*.csproj, tests/*/*.csproj.)