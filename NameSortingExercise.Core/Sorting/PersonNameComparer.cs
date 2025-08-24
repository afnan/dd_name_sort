
using NameSortingExercise.Core.Domain;

namespace NameSortingExercise.Core.Sorting
{
    public class PersonNameComparer : IComparer<Person>
    {
        private static readonly StringComparer Cmp = StringComparer.OrdinalIgnoreCase;
        public int Compare(Person? x, Person? y)
        {
            if (ReferenceEquals(x, y)) return 0;
            if (x is null) return -1;
            if (y is null) return 1;

            var byFamily = Cmp.Compare(x.Surname, y.Surname);
            if (byFamily != 0) return byFamily;

            var min = Math.Min(x.GivenNames.Count, y.GivenNames.Count);
            for (int i = 0; i < min; i++)
            {
                var temp = Cmp.Compare(x.GivenNames[i], y.GivenNames[i]);
                if (temp != 0) return temp;
            }
            return x.GivenNames.Count.CompareTo(y.GivenNames.Count);
        }
    }
}
