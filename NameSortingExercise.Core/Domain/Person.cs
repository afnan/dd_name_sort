
namespace NameSortingExercise.Core.Domain
{
    public class Person
    {
        public IReadOnlyList<string> GivenNames { get; }
        public string Surname { get; }

        public Person(IEnumerable<string> givenNames, string surname)
        {
            GivenNames = new List<string>(givenNames);
            Surname = surname;
        }

        public override string ToString() =>
            string.Join(" ", GivenNames) + " " + Surname;
    }
}
