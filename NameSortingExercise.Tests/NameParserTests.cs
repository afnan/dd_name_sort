
using NameSortingExercise.Core.Parsing;

namespace NameSortingExercise.Tests;

public class NameParserTests
{
    private readonly INameParser _parser = new NameParser();
    [Theory(DisplayName = "Parses valid lines with 1–3 given names")]
    [InlineData("Marin Alvarez", 1, "Alvarez")]
    [InlineData("Adonis Julius Archer", 2, "Archer")]
    [InlineData("Hunter Uriah Mathew Clarke", 3, "Clarke")]
    public void TriesParse_Valid_Lines(string line, int givenCount, string surname)
    {
        // Act
        var ok = _parser.TryParse(line, out var person);

        // Assert
        Assert.True(ok);
        AssertPerson(person, givenCount, surname);
    }

    [Theory(DisplayName = "Rejects invalid lines (empty, single token, too many tokens)")]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("SingleName")]
    [InlineData("Too Many names what is going on")]
    public void TriesParse_Invalid_Lines(string line)
    {
        Assert.False(_parser.TryParse(line, out var name));
        Assert.Null(name); // see if it is null
    }

    [Theory(DisplayName = "Trims and normalizes whitespace")]
    [InlineData("  Marin   Alvarez  ", "Marin Alvarez")]
    [InlineData("\tAdonis   Julius   Archer", "Adonis Julius Archer")]
    public void TriesParse_Normalizes_Whitespace(string input, string expectedToString)
    {
        var ok = _parser.TryParse(input, out var person);
        Assert.True(ok);
        Assert.Equal(expectedToString, person!.ToString());
    }

    private static void AssertPerson(NameSortingExercise.Core.Domain.Person? p, int givenCount, string surname)
    {
        Assert.NotNull(p);
        Assert.Equal(givenCount, p!.GivenNames.Count);
        Assert.Equal(surname, p.Surname);
    }


}
