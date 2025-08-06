using System;
using System.Collections.Generic;

namespace Autocomplete
{
    public class AutocompleteTests
    {
        public static void RunTests()
        {
            GetCountByPrefixTest();
            GetCountByPrefixEmptyTest();
            GetTopByPrefixTest();
            GetTopByPrefixTestLessThanCount();
            GetTopByPrefixTestEmpty();
        }

        private static bool GetCountByPrefixTest()
        {
            var Phrases = new List<string> { "a", "ab", "abc", "b" };
            var Prefix = "a";
            var ExpectedResult = 3;
            var Result = AutocompleteTask.GetCountByPrefix(Phrases, Prefix);
            return AssertEqual(ExpectedResult, Result);
        }

        private static bool GetCountByPrefixEmptyTest()
        {
            var Phrases = new List<string> { "a", "ab", "abc", "b" };
            var Prefix = "c";
            var ExpectedResult = 0;
            var Result = AutocompleteTask.GetCountByPrefix(Phrases, Prefix);
            return AssertEqual(ExpectedResult, Result);
        }

        private static bool GetTopByPrefixTest()
        {
            var Phrases = new List<string> { "a", "ab", "abc", "b" };
            var Prefix = "a";
            var Count = 2;
            var ExpectedResult = new[] { "a", "ab" };
            var Result = AutocompleteTask.GetTopByPrefix(Phrases, Prefix, Count);
            return AssertEqual(ExpectedResult, Result);
        }

        private static bool GetTopByPrefixTestLessThanCount()
        {
            var Phrases = new List<string> { "a", "ab", "abc", "b" };
            var Prefix = "a";
            var Count = 4;
            var ExpectedResult = new[] { "a", "ab", "abc" };
            var Result = AutocompleteTask.GetTopByPrefix(Phrases, Prefix, Count);
            return AssertEqual(ExpectedResult, Result);
        }

        private static bool GetTopByPrefixTestEmpty()
        {
            var Phrases = new List<string> { "a", "ab", "abc", "b" };
            var Prefix = "c";
            var Count = 2;
            var ExpectedResult = new string[0];
            var Result = AutocompleteTask.GetTopByPrefix(Phrases, Prefix, Count);
            return AssertEqual(ExpectedResult, Result);
        }

        private static bool AssertEqual<T>(T expected, T actual)
        {
            if (!EqualityComparer<T>.Default.Equals(expected, actual))
            {
                throw new Exception($"Test failed: expected {expected}, but got {actual}");
            }
            return true;
        }
    }

    internal class AutocompleteTask
    {
        public static string FindFirstByPrefix(IReadOnlyList<string> Phrases, string Prefix)
        {
            var Index = LeftBorderTask.GetLeftBorderIndex(Phrases, Prefix, -1, Phrases.Count) + 1;
            if (Index < Phrases.Count && Phrases[Index].StartsWith(Prefix, StringComparison.OrdinalIgnoreCase))
                return Phrases[Index];
            else
                return null;
        }

        public static string[] GetTopByPrefix(IReadOnlyList<string> Phrases, string Prefix, int Count)
        {
            int ActualCount = Math.Min(GetCountByPrefix(Phrases, Prefix), Count);
            int StartIndex = LeftBorderTask.GetLeftBorderIndex(Phrases, Prefix, -1, Phrases.Count) + 1;
            List<string> Result = new List<string>();

            for (int i = 0; i < ActualCount; i++)
            {
                Result.Add(Phrases[StartIndex + i]);
            }

            return Result.ToArray();
        }

        public static int GetCountByPrefix(IReadOnlyList<string> Phrases, string Prefix)
        {
            int Left = LeftBorderTask.GetLeftBorderIndex(Phrases, Prefix, -1, Phrases.Count);
            int Right = RightBorderTask.GetRightBorderIndex(Phrases, Prefix, -1, Phrases.Count);
            return Right - Left - 1;
        }
    }
}