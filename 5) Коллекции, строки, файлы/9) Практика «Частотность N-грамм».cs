using System.Collections.Generic;
using System.Linq;

namespace TextAnalysis
{
    static class FrequencyAnalysisTask
    {
        public static Dictionary<string, string> GetMostFrequentNextWords(List<List<string>> Text)
        {
            var Bigrams = BuildNGrams(Text, 2);
            var Trigrams = BuildNGrams(Text, 3);
            var Result = new Dictionary<string, string>();

            AddMostFrequentNextWords(Bigrams, Result);
            AddMostFrequentNextWords(Trigrams, Result);

            return Result;
        }

        private static Dictionary<string, Dictionary<string, int>> BuildNGrams(List<List<string>> Text, int n)
        {
            var NGrams = new Dictionary<string, Dictionary<string, int>>();

            foreach (var Sentence in Text)
            {
                for (int I = 0; I <= Sentence.Count - n; I++)
                {
                    var NGramKey = string.Join(" ", Sentence.Skip(I).Take(n - 1)).ToLower();
                    var NextWord = Sentence[I + n - 1].ToLower();

                    if (!NGrams.ContainsKey(NGramKey))
                        NGrams[NGramKey] = new Dictionary<string, int>();

                    if (!NGrams[NGramKey].ContainsKey(NextWord))
                        NGrams[NGramKey][NextWord] = 0;

                    NGrams[NGramKey][NextWord]++;
                }
            }

            return NGrams;
        }

        private static void AddMostFrequentNextWords(Dictionary<string, Dictionary<string, int>> NGrams,
                                                      Dictionary<string, string> Result)
        {
            foreach (var NGram in NGrams)
            {
                var MostFrequentNextWord = NGram.Value
                    .OrderByDescending(X => X.Value)
                    .ThenBy(X => X.Key)
                    .First();

                Result[NGram.Key] = MostFrequentNextWord.Key;
            }
        }
    }
}