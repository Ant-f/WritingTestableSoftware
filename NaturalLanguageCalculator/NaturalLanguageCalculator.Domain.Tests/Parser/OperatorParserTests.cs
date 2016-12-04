using NaturalLanguageCalculator.Domain.Interface;
using NaturalLanguageCalculator.Domain.Operator;
using NaturalLanguageCalculator.Domain.Parser;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace NaturalLanguageCalculator.Domain.Tests.Parser
{
    [TestFixture]
    public class OperatorParserTests
    {
        [TestCase("add", typeof(SumOperator))]
        [TestCase("plus", typeof(SumOperator))]
        [TestCase("minus", typeof(DifferenceOperator))]
        [TestCase("subtract", typeof(DifferenceOperator))]
        [TestCase("takeaway", typeof(DifferenceOperator))]
        public void CorrectOperatorIsReturned(string input, Type expectedType)
        {
            // Arrange

            var operators = new List<IOperator>
            {
                new SumOperator(),
                new DifferenceOperator()
            };

            var parser = new OperatorParser(operators);

            // Act

            var result = parser.Parse(input);

            // Assert

            Assert.That(result, Is.TypeOf(expectedType));
        }
    }
}
