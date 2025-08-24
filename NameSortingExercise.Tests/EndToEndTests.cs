using System.Diagnostics;

namespace NameSortingExercise.Tests;

public class EndToEndTests
{
    [Fact]
    public async Task DotnetRun_Sorts_And_Writes()
    {
        var input = Path.GetTempFileName();
        await File.WriteAllLinesAsync(input, new[]
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
        var psi = new ProcessStartInfo
        {
            FileName = "dotnet",
            Arguments = $"run --project NameSortingExercise.App {input}",
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false
        };

        using var p = Process.Start(psi)!;
        var stdout = await p.StandardOutput.ReadToEndAsync();
        var stderr = await p.StandardError.ReadToEndAsync();
        p.WaitForExit();

        Assert.Equal(0, p.ExitCode);
        Assert.Contains("Marin Alvarez", stdout);

        var outPath = Path.Combine(Directory.GetCurrentDirectory(), "sorted-names-list.txt");
        Assert.True(File.Exists(outPath));

        File.Delete(input);
        File.Delete(outPath);
    }
}