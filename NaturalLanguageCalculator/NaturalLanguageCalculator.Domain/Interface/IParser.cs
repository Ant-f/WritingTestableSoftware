using System.Collections.Generic;

namespace NaturalLanguageCalculator.Domain.Interface
{
    public interface IParser
    {
        IEnumerable<string> GetVocabularyList();
    }
}
