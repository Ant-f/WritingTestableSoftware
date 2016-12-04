using NaturalLanguageCalculator.Domain.Parser;
using NUnit.Framework;

namespace NaturalLanguageCalculator.Domain.Tests.Parser
{
    [TestFixture]
    public class NumberParserTests
    {
        [TestCase("zero", 0)]
        [TestCase("one", 1)]
        [TestCase("two", 2)]
        [TestCase("three", 3)]
        [TestCase("four", 4)]
        [TestCase("five", 5)]
        [TestCase("six", 6)]
        [TestCase("seven", 7)]
        [TestCase("eight", 8)]
        [TestCase("nine", 9)]
        public void LowerCaseIsParsed(string input, int expected)
        {
            // Arrange

            var parser = new NumberParser();

            // Act

            var result = parser.Parse(input);

            // Assert

            Assert.That(result, Is.EqualTo(expected));
        }

        [TestCase("ZERO", 0)]
        [TestCase("ONE", 1)]
        [TestCase("TWO", 2)]
        [TestCase("THREE", 3)]
        [TestCase("FOUR", 4)]
        [TestCase("FIVE", 5)]
        [TestCase("SIX", 6)]
        [TestCase("SEVEN", 7)]
        [TestCase("EIGHT", 8)]
        [TestCase("NINE", 9)]
        public void UpperCaseIsParsed(string input, int expected)
        {
            // Arrange

            var parser = new NumberParser();

            // Act

            var result = parser.Parse(input);

            // Assert

            Assert.That(result, Is.EqualTo(expected));
        }
    }
}
