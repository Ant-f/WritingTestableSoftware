namespace NaturalLanguageCalculator.Domain.Interface
{
    public interface IOperatorParser : IParser
    {
        IOperator Parse(string input);
    }
}
