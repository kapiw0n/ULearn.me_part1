using System;
using System.Collections.Generic;
namespace TextAnalysis
{
    static class TextGeneratorTask
    {
        public static string ContinuePhrase(
            Dictionary<string, string> NextWords,
            string PhraseBeginning,
            int WordsCount)
        {
            if (WordsCount <= 0)
                return PhraseBeginning;

            var PhraseParts = new List<string>(PhraseBeginning.Split(' '));
            var CurrentPhrase = PhraseBeginning;

            for (int i = 0; i < WordsCount; i++)
            {
                string NextWord = GetNextWord(NextWords, PhraseParts);
                if (NextWord == null)
                    break;

                CurrentPhrase += " " + NextWord;
                PhraseParts.Add(NextWord);
            }

            return CurrentPhrase;
        }

        private static string GetNextWord(
            Dictionary<string, string> NextWords, 
            List<string> PhraseParts)
        {
            if (PhraseParts.Count >= 2)
			{
    			string Key = $"{PhraseParts[PhraseParts.Count - 2]} {PhraseParts[PhraseParts.Count - 1]}";
   				if (NextWords.ContainsKey(Key))
       				return NextWords[Key];
			}

            if (PhraseParts.Count >= 1)
            {
                string Key = PhraseParts[^1];
                if (NextWords.TryGetValue(Key, out string NextWord))
                    return NextWord;
            }
            return null;
        }
    }
}