using NaturalLanguageCalculator.Domain.Operator;
using NUnit.Framework;

namespace NaturalLanguageCalculator.Domain.Tests.Operator
{
    [TestFixture]
    public class DifferenceOperatorTests
    {
        [TestCase(7, 4, 3)]
        [TestCase(9, 5, 4)]
        public void EvaluateSubtractsOperands(
            int operand1,
            int operand2,
            int expectedResult)
        {
            // Arrange

            var operatorFunction = new DifferenceOperator();

            // Act

            var result = operatorFunction.Evaluate(operand1, operand2);

            // Assert

            Assert.That(result, Is.EqualTo(expectedResult));
        }
    }
}
