using NameSortingExercise.Core.Domain;

namespace NameSortingExercise.Core.Parsing;

public sealed class NameParser : INameParser
{
    public bool TryParse(string line, out Person? name)
    {
        name = null;
        if (string.IsNullOrWhiteSpace(line)) return false;
        var possibleNames = line.Trim()
                         .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                         .Select(t => t.Trim())
                         .ToArray();
        if (possibleNames.Length < 2 || possibleNames.Length > 4) return false;
        var family = possibleNames[^1];
        var given = possibleNames.Take(possibleNames.Length - 1);

        try
        {
            name = new Person(given, family);
            return true;
        }
        catch
        {
            return false;
        }
    }
}
