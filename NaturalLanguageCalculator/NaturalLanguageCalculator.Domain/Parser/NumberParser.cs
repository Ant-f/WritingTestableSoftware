using NaturalLanguageCalculator.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NaturalLanguageCalculator.Domain.Parser
{
    public class NumberParser : INumberParser
    {
        private readonly Dictionary<string, int> _numberDictionary =
            new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase)
        {
            ["zero"] = 0,
            ["one"] = 1,
            ["two"] = 2,
            ["three"] = 3,
            ["four"] = 4,
            ["five"] = 5,
            ["six"] = 6,
            ["seven"] = 7,
            ["eight"] = 8,
            ["nine"] = 9
        };

        public int Parse(string input)
        {
            var value = _numberDictionary[input];
            return value;
        }

        public IEnumerable<string> GetVocabularyList()
        {
            var vocabulary = _numberDictionary.Keys;
            return vocabulary;
        }
    }
}
