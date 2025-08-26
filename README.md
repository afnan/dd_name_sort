# Name Sorter

Sort names by **surname** then **given names** (1–3). Prints to console and writes `sorted-names-list.txt`.

## Design Principles & Patterns

- **SOLID Principles:** The program uses SOLID principles to keep components small, testable, and maintainable.
- **Factory Pattern (Person):** `Person` instances are constructed via a factory. Reason is that the factory centralizes validation and creation logic so domain invariants are enforced in one place and calling code remains simple. Also gives benifit of doing validation in create rather than constructor.
- **Sealed Records & Sealed Classes:** I have used `sealed records` and `sealed` classes to ensure immutability and to prevent unintended inheritance. This helps preserve invariants and makes reasoning about state easier.

## Architecture

Following are the layers of the system.

- Layers:

  - `App` — composition root and orchestrator (wires dependencies and runs the workflow).
  - `Core` — domain types (`Person`), parsing (`INameParser` / `NameParser`), and sorting (`PersonNameComparer`).
  - `Infrastructure` — adapters for I/O file handling (`INameRepository` / `FileNameRepository`).

- `AppBootstrap` (Why did i use this):

  - This acts as the application orchestrator / composition root for the small CLI: it accepts the repository, parser and comparer and coordinates parsing, sorting and writing. Basically stitches everything togehter while keeping main program clean.
  - Keeps `Program.cs` thin — `Program.cs` only registers services and invokes `AppBootstrap.RunAsync(args)` which improves single-responsibility and testability.
  - Makes integration and end-to-end testing easy: tests construct `AppBootstrap` with real or fake implementations (see `NameSortingExercise.Tests.EndToEndTests`).

- Exit codes:

  - `0` — success.
  - `2` — invalid usage or missing input file.

- Testing notes / extensibility:
  - Because dependencies are directly injected, you can replace `INameRepository` with an in-memory implementation for fast tests, or add logging and configuration later by introducing a host.

## Quick Start

```bash
# from repo root
dotnet build
dotnet run --project NameSortingExercise.App ./unsorted-names-list.txt
```

## Test

```bash
dotnet test
```

Unit tests: cover parsing (NameParser), sorting (PersonNameComparer), and domain invariants (Person).
End-to-end test: runs the whole workflow in a temp directory with real file I/O.

## Test Results & Coverage (CI)

When the GitHub Actions workflow runs, it will:

- Execute all tests with coverage enabled.
- Upload test results (`.trx`) and coverage reports (`coverage.cobertura.xml`) as build artifacts.

### How can results be downloaded?

(not required but nice to have may be.)

1. Go to the **Actions** tab in this GitHub repository.
2. Open the latest workflow run for your branch (e.g., `main`).
3. Scroll to the **Artifacts** section at the bottom of the run summary.
4. Download `test-results-<os>.zip` — it contains:
   - `test-results.trx` → Detailed test results.
   - `coverage.cobertura.xml` → Code coverage report (Cobertura format).

You can open `.trx` files with Visual Studio.
