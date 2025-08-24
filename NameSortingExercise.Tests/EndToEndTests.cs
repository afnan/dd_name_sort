using NameSortingExercise.App;
using NameSortingExercise.Core.Parsing;
using NameSortingExercise.Core.Sorting;
using NameSortingExercise.Infrastructure;
using System.Diagnostics;

namespace NameSortingExercise.Tests;

public class EndToEndTests
{
    [Fact]
    public async Task DotnetRun_Sorts_And_Writes()
    {
        var workDir = Directory.CreateTempSubdirectory();
        var prevCwd = Directory.GetCurrentDirectory();
        Environment.CurrentDirectory = workDir.FullName;
        try
        {


            var inputPath = Path.Combine(workDir.FullName, "unsorted-names-list.txt");

            await File.WriteAllLinesAsync(inputPath, new[]
                   {
                    "Janet Parsons",
                    "Vaughn Lewis",
                    "Adonis Julius Archer",
                    "Shelby Nathan Yoder",
                    "Marin Alvarez",
                    "London Lindsey",
                    "Beau Tristan Bentley",
                    "Leo Gardner",
                    "Hunter Uriah Mathew Clarke",
                    "Mikayla Lopez",
                    "Frankie Conner Ritter",
                });
            var runner = new AppBootstrap(
                                new FileNameRepository(),
                                new NameParser(),
                                new PersonNameComparer()
                            );


            var exit = await runner.RunAsync(new[] { inputPath });


            Assert.Equal(0, exit);

            var outPath = Path.Combine(workDir.FullName, "sorted-names-list.txt");
            Assert.True(File.Exists(outPath), $"Expected output at {outPath}");

            var outText = await File.ReadAllTextAsync(outPath);
            Assert.Contains("Marin Alvarez", outText);
            Assert.Contains("Shelby Nathan Yoder", outText);
        }
        finally
        {
            Environment.CurrentDirectory = prevCwd;
            try { workDir.Delete(recursive: true); } catch { /* ignore */ }
        }
    }
}