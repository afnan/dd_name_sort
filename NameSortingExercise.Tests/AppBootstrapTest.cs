using NameSortingExercise.App;
using NameSortingExercise.Core.Parsing;
using NameSortingExercise.Core.Sorting;
using NameSortingExercise.Infrastructure;

namespace NameSortingExercise.Tests;

public class AppBootstrapTest
{
    [Fact(DisplayName = "Return usage code when invoked without arguments")]
    public async Task RunAsync_NoArgs_ReturnsUsageCode()
    {
        var runner = new AppBootstrap(new FileNameRepository(), new NameParser(), new PersonNameComparer());
        var exit = await runner.RunAsync(Array.Empty<string>());
        Assert.Equal(2, exit);
    }

    [Fact(DisplayName = "Reads from repository, sorts and writes output")]
    public async Task RunAsync_UsesRepository_SortsAndWrites()
    {
        // create a temp file so AppBootstrap's File.Exists check passes
        var temp = Path.GetTempFileName();
        try
        {
            File.WriteAllText(temp, string.Empty);

            var repo = new InMemoryRepository(new[]
            {
                "Janet Parsons",
                "Marin Alvarez",
                "Adonis Julius Archer",
            });

            var runner = new AppBootstrap(repo, new NameParser(), new PersonNameComparer());
            var exit = await runner.RunAsync(new[] { temp });

            Assert.Equal(0, exit);

            // verify something was written and that it is sorted by surname
            Assert.NotEmpty(repo.WrittenLines);
            Assert.Equal("Marin Alvarez", repo.WrittenLines.First());
        }
        finally
        {
            try { File.Delete(temp); } catch { }
        }
    }

    private class InMemoryRepository : INameRepository
    {
        //Do the fake thing here no matter the path
        private readonly IReadOnlyList<string> _lines;
        public List<string> WrittenLines { get; } = new();
        public InMemoryRepository(IEnumerable<string> lines)
        {
            _lines = lines.ToList();
        }
        public Task<IReadOnlyList<string>> ReadAllAsync(string path) => Task.FromResult(_lines);

        public Task WriteAllAsync(string path, IEnumerable<string> lines)
        {
            WrittenLines.AddRange(lines);
            return Task.CompletedTask;
        }
    }
}

