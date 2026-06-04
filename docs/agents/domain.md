# Domain Documentation Configuration

This repository follows a single-context documentation layout:

- Primary context file: `CONTEXT.md` (located at the repository root)
- Architectural Decision Records: stored in `docs/adr/` directory (relative to repository root)

## Documentation Consumption Rules

Skills that require domain context (such as `improve-codebase-architecture`, `diagnose`, and `tdd`) will:

1. Look for `CONTEXT.md` at the repository root for domain language and project context
2. Check `docs/adr/` for architectural decision records when making implementation recommendations
3. Expect ADRs to follow standard Markdown format with decision context and consequences

If `CONTEXT.md` does not exist, skills will proceed with limited domain context.
If the `docs/adr/` directory does not exist or is empty, skills will not incorporate architectural decisions into their analysis.