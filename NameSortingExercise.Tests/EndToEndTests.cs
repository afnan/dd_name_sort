using System.Diagnostics;

namespace NameSortingExercise.Tests
{
    public class EndToEndTests
    {
        [Fact]
        public async Task Sorts_File_And_Writes_Output()
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

            // run the console app: dotnet run --project src/... <input>
            var psi = new ProcessStartInfo
            {
                FileName = "dotnet",
                Arguments = $"run --project src/NameSortingExercise.App {input}",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false
            };

            using var p = Process.Start(psi)!;
            var stdout = await p.StandardOutput.ReadToEndAsync();
            var stderr = await p.StandardError.ReadToEndAsync();
            p.WaitForExit();

            Assert.Equal(0, p.ExitCode); // expect success

            var outputPath = Path.Combine(Directory.GetCurrentDirectory(), "sorted-names-list.txt");
            Assert.True(File.Exists(outputPath));

            // quick sanity check: first and last lines
            var lines = await File.ReadAllLinesAsync(outputPath);
            Assert.StartsWith("Marin Alvarez", lines[0]);
            Assert.EndsWith("Shelby Nathan Yoder", lines[^1]);
        }
    }
}
