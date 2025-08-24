# Name Sorter

Sort names by **surname** then **given names** (1–3). Prints to console and writes `sorted-names-list.txt`.

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

1. Go to the **Actions** tab in your GitHub repository.
2. Open the latest workflow run for your branch (e.g., `main`).
3. Scroll to the **Artifacts** section at the bottom of the run summary.
4. Download `test-results-<os>.zip` — it contains:
   - `test-results.trx` → Detailed test results.
   - `coverage.cobertura.xml` → Code coverage report (Cobertura format).

You can open `.trx` files with Visual Studio.
