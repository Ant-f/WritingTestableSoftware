using NaturalLanguageCalculator.Domain.Interface;

namespace NaturalLanguageCalculator.Domain.Operator
{
    public class DifferenceOperator : IOperator
    {
        public int Evaluate(int operand1, int operand2)
        {
            var result = operand1 - operand2;
            return result;
        }
    }
}
