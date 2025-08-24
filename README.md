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
