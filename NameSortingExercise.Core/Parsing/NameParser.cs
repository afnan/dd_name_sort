using NameSortingExercise.Core.Domain;

namespace NameSortingExercise.Core.Parsing
{
    public sealed class NameParser : INameParser
    {
        public bool TryParse(string line, out Person name)
        {
            name = null;
            return false; // stub: will guarantees tests will fail later (red)
        }
    }
}
