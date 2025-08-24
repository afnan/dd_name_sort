using NameSortingExercise.Core.Domain;
using NameSortingExercise.Core.Sorting;

namespace NameSortingExercise.Tests;

public class PersonComparerTests
{
    private static Person N(string s)
    {
        var parts = s.Split(' ');
        return Person.Create(parts[..^1], parts[^1]);
    }

    [Fact]
    public void Sorts_By_Family_Then_GivenNames()
    {
        var list = new[]
        {
                N("Janet Parsons"),
                N("Adonis Julius Archer"),
                N("Marin Alvarez"),
                N("Beau Tristan Bentley"),
            }.ToList();

        list.Sort(new PersonNameComparer());
        Assert.Equal(new[]
        {
                "Marin Alvarez",
                "Adonis Julius Archer",
                "Beau Tristan Bentley",
                "Janet Parsons"
            }, list.Select(x => x.ToString()).ToArray());
    }

    [Fact]
    public void Shorter_GivenNames_Wins_When_Prefix()
    {
        var a = N("John A Smith");
        var b = N("John Adam Smith");
        Assert.True(new PersonNameComparer().Compare(a, b) < 0);
    }
}
