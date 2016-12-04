using NaturalLanguageCalculator.Domain.Interface;
using NaturalLanguageCalculator.Domain.Operator;
using System.Collections.Generic;
using System.Linq;

namespace NaturalLanguageCalculator.Domain.Parser
{
    public class OperatorParser : IOperatorParser
    {
        private readonly Dictionary<string, IOperator> _operatorDictionary;

        public OperatorParser(IList<IOperator> operators)
        {
            _operatorDictionary = new Dictionary<string, IOperator>
            {
                ["add"] = operators.OfType<SumOperator>().Single(),
                ["plus"] = operators.OfType<SumOperator>().Single(),
                ["minus"] = operators.OfType<DifferenceOperator>().Single(),
                ["subtract"] = operators.OfType<DifferenceOperator>().Single(),
                ["takeaway"] = operators.OfType<DifferenceOperator>().Single()
            };
        }

        public IOperator Parse(string input)
        {
            return _operatorDictionary[input];
        }

        public IEnumerable<string> GetVocabularyList()
        {
            var vocabulary = _operatorDictionary.Keys;
            return vocabulary;
        }
    }
}
