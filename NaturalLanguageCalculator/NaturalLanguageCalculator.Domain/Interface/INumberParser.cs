namespace NaturalLanguageCalculator.Domain.Interface
{
    public interface INumberParser : IParser
    {
        int Parse(string input);
    }
}
