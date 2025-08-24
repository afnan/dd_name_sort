
namespace NameSortingExercise.Core.Domain;

/// Represents one person with 1–3 given names and a surname.
/// Shoud be Immutable once constructed.
/// Using Record and Factory method to enforce invariants.
public sealed record Person
{
    public IReadOnlyList<string> GivenNames { get; }
    public string Surname { get; }
    private Person(IReadOnlyList<string> givenNames, string surname)
            => (GivenNames, Surname) = (givenNames, surname);
    public static Person Create(IEnumerable<string> givenNames, string surname)
    {
        if (string.IsNullOrWhiteSpace(surname))
            throw new ArgumentException("Family name must be provided.", nameof(surname));

        var g = (givenNames ?? throw new ArgumentNullException(nameof(givenNames)))
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .Select(s => s.Trim())
                .ToArray();

        if (g.Length is < 1 or > 3)
            throw new ArgumentException("Given names must be 1–3 names.", nameof(givenNames));

        return new Person(Array.AsReadOnly(g), surname.Trim());
    }

    public override string ToString() =>
        string.Join(" ", GivenNames) + " " + Surname;
}
