using NUnit.Framework;

namespace NaturalLanguageCalculator.Domain.Tests
{
    [TestFixture]
    public class NumberNamerTests
    {
        [TestCase(0, "zero")]
        [TestCase(1, "one")]
        [TestCase(2, "two")]
        [TestCase(3, "three")]
        [TestCase(4, "four")]
        [TestCase(5, "five")]
        [TestCase(6, "six")]
        [TestCase(7, "seven")]
        [TestCase(8, "eight")]
        [TestCase(9, "nine")]
        [TestCase(10, "ten")]
        [TestCase(11, "eleven")]
        [TestCase(12, "twelve")]
        [TestCase(13, "thirteen")]
        [TestCase(14, "fourteen")]
        [TestCase(15, "fifteen")]
        [TestCase(16, "sixteen")]
        [TestCase(17, "seventeen")]
        [TestCase(18, "eighteen")]
        [TestCase(19, "nineteen")]
        [TestCase(20, "twenty")]
        public void NumbersAreNamedCorrectly(int input, string expectedOutput)
        {
            // Arrange

            var namer = new NumberNamer();

            // Act

            var result = namer.GetName(input);

            // Assert

            Assert.That(result, Is.EqualTo(expectedOutput));
        }
    }
}
