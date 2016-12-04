using NaturalLanguageCalculator.Domain.Interface;

namespace NaturalLanguageCalculator.Domain
{
    public class Calculator : ICalculator
    {
        private const char Separator = ' ';

        private readonly INumberNamer _numberNamer;
        private readonly INumberParser _numberParser;
        private readonly IOperatorParser _operatorParser;

        public Calculator(
            INumberNamer numberNamer,
            INumberParser numberParser,
            IOperatorParser operatorParser)
        {
            _numberNamer = numberNamer;
            _numberParser = numberParser;
            _operatorParser = operatorParser;
        }

        public string Evaluate(string input)
        {
            var tokens = input.Split(Separator);

            var operand1Name = tokens[0];
            var operatorFunctionName = tokens[1];
            var operand2Name = tokens[2];

            var operand1 = _numberParser.Parse(operand1Name);
            var operatorFunction = _operatorParser.Parse(operatorFunctionName);
            var operand2 = _numberParser.Parse(operand2Name);

            var result = operatorFunction.Evaluate(
                operand1,
                operand2);

            var output = _numberNamer.GetName(result);

            return output;
        }
    }
}
