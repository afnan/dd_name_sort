using NameSortingExercise.Core.Domain;

namespace NameSortingExercise.Core.Parsing
{
    public interface INameParser
    {
        bool TryParse(string line, out Person? name);
    }
}
