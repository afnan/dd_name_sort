
using NameSortingExercise.Core.Domain;
using NameSortingExercise.Core.Parsing;
using NameSortingExercise.Infrastructure;
using System;

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

        var parsed = raw.Select((line, i) => new { line, i })
                        .Select(x => _parser.TryParse(x.line, out var p)
                            ? new { Ok = true, Person = p, x.i, x.line }                            
                            : new { Ok = false, Person = (Person?)null, x.i, x.line })
                        .ToList();

        foreach (var bad in parsed.Where(p => !p.Ok))
            Console.Error.WriteLine($"Skipping invalid line {bad.i + 1}: '{bad.line}' (must be 2–4 Given names; last one is the surname)");

        var people = parsed.Where(p => p.Ok).Select(p => p.Person!).ToList();
        people.Sort(_comparer);

        foreach (var p in people) Console.WriteLine(p);

        await _repo.WriteAllAsync("sorted-names-list.txt", people.Select(p => p.ToString()));
        return 0;
    }
}

