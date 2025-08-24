
namespace NameSortingExercise.Core.Domain
{
    public class Person
    {
        public IReadOnlyList<string> GivenNames { get; }
        public string Surname { get; }

        public Person(IEnumerable<string> givenNames, string surname)
        {
            if (string.IsNullOrWhiteSpace(surname))
                throw new ArgumentException("Family name must be provided.", nameof(surname));

            var g = (givenNames ?? throw new ArgumentNullException(nameof(givenNames)))
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .Select(s => s.Trim())
                .ToArray();

            if (g.Length < 1 || g.Length > 3)
                throw new ArgumentException("Given names must be 1–3 names.", nameof(givenNames));

            GivenNames = Array.AsReadOnly(g);
            Surname = surname.Trim();
        }

        public override string ToString() =>
            string.Join(" ", GivenNames) + " " + Surname;
    }
}
