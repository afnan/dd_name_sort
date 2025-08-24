
using NameSortingExercise.Core.Domain;

namespace NameSortingExercise.Core.Sorting
{
    public class PersonNameComparer : IComparer<Person>
    {
        public int Compare(Person? x, Person? y)
        {
            return 0; // stub so tests compile and then fail (red)
        }
    }
}
