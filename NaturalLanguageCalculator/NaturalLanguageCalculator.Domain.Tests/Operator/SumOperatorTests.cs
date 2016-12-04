using NaturalLanguageCalculator.Domain.Operator;
using NUnit.Framework;

namespace NaturalLanguageCalculator.Domain.Tests.Operator
{
    [TestFixture]
    public class SumOperatorTests
    {
        [TestCase(3, 4, 7)]
        [TestCase(5, 6, 11)]
        public void EvaluateAddsOperands(
            int operand1,
            int operand2,
            int expectedResult)
        {
            // Arrange

            var operatorFunction = new SumOperator();

            // Act

            var result = operatorFunction.Evaluate(operand1, operand2);

            // Assert

            Assert.That(result, Is.EqualTo(expectedResult));
        }
    }
}
