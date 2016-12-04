using Moq;
using NaturalLanguageCalculator.Domain.Interface;
using NaturalLanguageCalculator.Domain.Operator;
using NaturalLanguageCalculator.Domain.Parser;
using NUnit.Framework;
using System.Collections.Generic;

namespace NaturalLanguageCalculator.Domain.Tests
{
    [TestFixture]
    public class CalculatorTests
    {
        /// <summary>
        /// Demonstrate setting mock default value
        /// </summary>
        [Test]
        public void Operand1TokenIsExtractedAndParsed()
        {
            // Arrange

            const string operand1 = "operand1";
            string input = $"{operand1} operatorFunction operand2";

            // Create new INumberParser mock
            var numberParserMock = new Mock<INumberParser>();

            // Create new IOperatorParser mock
            var operatorParserMock = new Mock<IOperatorParser>
            {
                // Unless specified, calls to mocks will return default values;
                // the default value for reference type is null. The output of
                // the operator parser is used in code and a null reference
                // exception will be thrown, so we change the default return
                // from a mock to be a new mock instead of null. This is ok
                // because this particular test doesn't expect anything/make
                // any assertions on the operator function.
                DefaultValue = DefaultValue.Mock
            };

            var numberNamerMock = new Mock<INumberNamer>();

            var calculator = new Calculator(
                numberNamerMock.Object,
                numberParserMock.Object,
                operatorParserMock.Object);

            // Act

            var result = calculator.Evaluate(input);

            // Assert

            // We want to to know that Parse was called on our number parser
            // with operand1 as the argument exactly once
            numberParserMock
                .Verify(o => o.Parse(operand1),
                    Times.Once);
        }

        /// <summary>
        /// Demonstrate creating mocks and specifying behaviour using Setup/Returns
        /// </summary>
        [Test]
        public void Operand2TokenIsExtractedAndParsed()
        {
            // Arrange

            const string operand2 = "operand2";
            string input = $"operand1 operatorFunction {operand2}";

            // Create new INumberParser
            var numberParserMock = new Mock<INumberParser>();

            // Create new IOperatorParser mock
            var operatorParserMock = new Mock<IOperatorParser>();

            // Again, the only concern that this test has with the operator
            // parser is that a null reference exception isn't thrown. This time
            // we explicitly set up the operator parser mock.
            operatorParserMock
                .Setup(o => o.Parse(It.IsAny<string>()))
                .Returns(new Mock<IOperator>().Object);

            var numberNamerMock = new Mock<INumberNamer>();

            var calculator = new Calculator(
                numberNamerMock.Object,
                numberParserMock.Object,
                operatorParserMock.Object);

            // Act

            var result = calculator.Evaluate(input);

            // Assert

            numberParserMock
                .Verify(o => o.Parse(operand2),
                    Times.Once);
        }

        /// <summary>
        /// Demonstrate creating mocks and specifying behaviour with Linq to Mocks
        /// </summary>
        [Test]
        public void OperatorTokenIsExtractedAndParsed()
        {
            // Arrange

            const string operatorFunction = "operatorFunction";
            string input = $"operand1 {operatorFunction} operand2";

            // Create and setup new mock using Linq to Mocks
            var operatorParser = Mock.Of<IOperatorParser>(o =>
                o.Parse(It.IsAny<string>()) == Mock.Of<IOperator>());

            var calculator = new Calculator(
                Mock.Of<INumberNamer>(),
                Mock.Of<INumberParser>(),
                operatorParser);

            // Act

            var result = calculator.Evaluate(input);

            // Assert

            Mock.Get(operatorParser)
                .Verify(o => o.Parse(operatorFunction));
        }

        /// <summary>
        /// Further demonstrates Linq to Mocks, nested mock setup
        /// </summary>
        [Test]
        public void InputIsParsedAndProcessed()
        {
            // Arrange

            const string operand1 = "operand1";
            const string operand2 = "operand2";
            const string operatorFunction = "operatorFunction";
            const string numberNamerOutput = "NumberNamerOutput";
            const int operand1Value = 3;
            const int operand2Value = 5;
            const int operatorOutput = 7;
            string input = $"{operand1} {operatorFunction} {operand2}";

            var numberParser = Mock.Of<INumberParser>(n =>
                n.Parse(operand1) == operand1Value &&
                n.Parse(operand2) == operand2Value);

            // Nested mocks
            var operatorParser = Mock.Of<IOperatorParser>(parser =>
                parser.Parse(operatorFunction) == Mock.Of<IOperator>(o =>
                    o.Evaluate(operand1Value, operand2Value) == operatorOutput));

            var numberNamer = Mock.Of<INumberNamer>(n =>
                n.GetName(operatorOutput) == numberNamerOutput);

            var calculator = new Calculator(
                numberNamer,
                numberParser,
                operatorParser);

            // Act

            var result = calculator.Evaluate(input);

            // Assert

            Mock.Get(numberParser).Verify(n =>
                n.Parse(operand1));

            Mock.Get(numberParser).Verify(n =>
                n.Parse(operand2));

            Mock.Get(operatorParser).Verify(o =>
                o.Parse(operatorFunction));

            Mock.Get(numberNamer).Verify(n =>
                n.GetName(operatorOutput));

            Assert.That(result, Is.EqualTo(numberNamerOutput));
        }

        /// <summary>
        /// Test operation with real instances of each class
        /// </summary>
        [TestCase("two plus five", "seven")]
        [TestCase("nine minus six", "three")]
        public void CalculationIsCorrect(string input, string expectedOutput)
        {
            // Arrange

            var operators = new List<IOperator>
            {
                new DifferenceOperator(),
                new SumOperator()
            };

            var calculator = new Calculator(
                new NumberNamer(),
                new NumberParser(),
                new OperatorParser(operators));

            // Act

            var result = calculator.Evaluate(input);

            // Assert

            Assert.That(result, Is.EqualTo(expectedOutput));
        }
    }
}
