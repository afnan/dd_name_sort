
using NameSortingExercise.Core.Parsing;

namespace NameSortingExercise.Tests
{
    public class NameParserTests
    {
        private readonly INameParser _parser = new NameParser();
        [Theory]
        [InlineData("Marin Alvarez", 1, "Alvarez")]
        [InlineData("Adonis Julius Archer", 2, "Archer")]
        [InlineData("Hunter Uriah Mathew Clarke", 3, "Clarke")]
        public void TriesParse_Valid_Lines(string line, int givenCount, string surname)
        {
            Assert.True(_parser.TryParse(line, out var name));
            Assert.NotNull(name); //see if it is not null
            Assert.Equal(givenCount, name.GivenNames.Count); // see if it matches the given count
            Assert.Equal(surname, name.Surname); // see if it matches the surname
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("SingleName")]
        [InlineData("Too Many names what is going on")]
        public void TriesParse_Invalid_Lines(string line)
        {
            Assert.False(_parser.TryParse(line, out var name));
            Assert.Null(name); // see if it is null
        }


    }
}
