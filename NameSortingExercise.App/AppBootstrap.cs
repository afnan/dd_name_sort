
using NameSortingExercise.Core.Domain;
using NameSortingExercise.Core.Parsing;
using NameSortingExercise.Infrastructure;

namespace NameSortingExercise.App;

public class AppBootstrap
{
    private readonly INameRepository _repo;
    private readonly INameParser _parser;
    private readonly System.Collections.Generic.IComparer<Person> _comparer;

    public AppBootstrap(INameRepository repo, INameParser parser, System.Collections.Generic.IComparer<Person> comparer)
    {
        _repo = repo;
        _parser = parser;
        _comparer = comparer;
    }

    public async Task<int> RunAsync(string[] args)
    {
        if (args.Length != 1)
        {
            Console.Error.WriteLine("Usage: name-sorter <path-to-unsorted-names-list.txt>");
            return 2;
        }

        var inputPath = args[0];
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return 2;
        }

        var raw = await _repo.ReadAllAsync(inputPath);

        // Parse each line; collect success/failure info for reporting
        var parsed = raw.Select((line, i) => new { line, i })
                        .Select(x => _parser.TryParse(x.line, out var p)
                            ? new { Ok = true, Person = p, x.i, x.line }
                            : new { Ok = false, Person = (Person?)null, x.i, x.line })
                        .ToList();

        // only tell about any invalid lines rather than failing the whole run
        foreach (var bad in parsed.Where(p => !p.Ok))
            Console.Error.WriteLine($"Skipping invalid line {bad.i + 1}: '{bad.line}' (must be 2–4 Given names; last one is the surname)");

        var people = parsed.Where(p => p.Ok && p.Person != null).Select(p => p.Person!).ToList();

        // Sort by surname from (A→Z), and then by given names left-to-right;
        // if all equal so far, fewer given names wins.
        // Comparisons are case-insensitive, ordinal.
        people.Sort(_comparer);

        foreach (var p in people) Console.WriteLine(p);

        // NOTE: File will be overwritten if it exists
        await _repo.WriteAllAsync("sorted-names-list.txt", people.Select(p => p.ToString()));
        return 0;
    }
}

