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

    [Fact(DisplayName = "Sorts by surname then given names")]
    public void Sorts_By_Family_Then_GivenNames()
    {
        // Arrange
        var list = new[]
        {
                N("Janet Parsons"),
                N("Adonis Julius Archer"),
                N("Marin Alvarez"),
                N("Beau Tristan Bentley"),
            }.ToList();
        // Act
        list.Sort(new PersonNameComparer());
        // Assert
        Assert.Equal(new[]
        {
                "Marin Alvarez",
                "Adonis Julius Archer",
                "Beau Tristan Bentley",
                "Janet Parsons"
            }, list.Select(x => x.ToString()).ToArray());
    }

    [Fact(DisplayName = "Shorter given-name list wins when prefix")]
    public void Shorter_GivenNames_Wins_When_Prefix()
    {
        var a = N("John A Smith");
        var b = N("John Adam Smith");
        Assert.True(new PersonNameComparer().Compare(a, b) < 0);
    }

    [Fact(DisplayName = "Comparer is case-insensitive and deterministic")]
    public void Case_Insensitive_And_Deterministic()
    {
        var a = Person.Create(new[] { "vaughn" }, "lewis");
        var b = Person.Create(new[] { "Vaughn" }, "Lewis");

        var cmp = new PersonNameComparer();
        Assert.Equal(0, cmp.Compare(a, b)); 
    }
}
