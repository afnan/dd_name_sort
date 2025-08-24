using NameSortingExercise.Core.Domain;

namespace NameSortingExercise.Core.Parsing;

public interface INameParser
{
    /// Parses a line into a Person object if valid; returns false otherwise.
    bool TryParse(string line, out Person? name);
}
