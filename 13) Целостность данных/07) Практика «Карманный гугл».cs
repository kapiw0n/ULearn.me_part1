using System.Collections.Generic;
using System.Linq;
namespace PocketGoogle
{
    public class Indexer : IIndexer
    {
        private readonly Dictionary<int, Dictionary<string, List<int>>> documentPositions =
            new Dictionary<int, Dictionary<string, List<int>>>(8);
        private readonly Dictionary<string, HashSet<int>> termInDocuments =
            new Dictionary<string, HashSet<int>>(8);
        private readonly char[] separators = new[] { ' ', '.', ',', '!', '?', ':', '-', '\r', '\n' };


        public void Add(int docId, string textContent)
        {
            documentPositions[docId] = new Dictionary<string, List<int>>(8);
            var splitWords = textContent.Split(separators);
            var totalWords = splitWords.Length;
            var currentPosition = 0;
            for (int index = 0; index < totalWords; index++)
            {
                var currentWord = splitWords[index];

                if (!termInDocuments.ContainsKey(currentWord))
                    termInDocuments[currentWord] = new HashSet<int>(8);

                if (!termInDocuments[currentWord].Contains(docId))
                    termInDocuments[currentWord].Add(docId);

                if (!documentPositions[docId].ContainsKey(currentWord))
                    documentPositions[docId][currentWord] = new List<int>(8);

                documentPositions[docId][currentWord].Add(currentPosition);
                currentPosition += currentWord.Length + 1;
            }
        }

        public List<int> GetIds(string searchTerm)
        {
            HashSet<int> docIdSet;
            termInDocuments.TryGetValue(searchTerm, out docIdSet);
            return docIdSet == null ? new List<int>() : docIdSet.ToList();
        }

        public List<int> GetPositions(int docId, string searchTerm)
        {
            List<int> positionList;
            documentPositions[docId].TryGetValue(searchTerm, out positionList);
            return positionList ?? new List<int>();
        }

        public void Remove(int docId)
        {
            var existingWords = documentPositions[docId].Keys.ToArray();
            for (int index = 0; index < existingWords.Length; index++)
            {
                if (termInDocuments.ContainsKey(existingWords[index]))
                    termInDocuments[existingWords[index]].Remove(docId);
            }
            documentPositions.Remove(docId);
        }
    }
}