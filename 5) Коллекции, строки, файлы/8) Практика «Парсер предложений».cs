using System.Collections.Generic;
using System.Text.RegularExpressions;
namespace TextAnalysis
{
    static class SentencesParserTask
    {
        public static List<List<string>> ParseSentences(string Text)
        {
            var SentencesList = new List<List<string>>();

            var Sentences = Regex.Split(Text, @"(?<=[.!?;:()])\s*");

            foreach (var Sentence in Sentences)
            {
                var Words = Regex.Matches(Sentence, @"[a-zA-Z']+");

                if (Words.Count > 0)
                {
                    var WordList = new List<string>();
                    foreach (Match Word in Words)
                    {
                        WordList.Add(Word.Value.ToLower());
                    }
                    SentencesList.Add(WordList);
                }
            }
            return SentencesList;
        }
    }
}