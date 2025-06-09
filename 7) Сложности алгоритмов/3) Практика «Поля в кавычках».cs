using System.Text;
using NUnit.Framework;
namespace TableParser
{
    [TestFixture]
    public class QuotedFieldTaskTests
    {
        [TestCase("''", 0, "", 2)]
		[TestCase("'a'", 0, "a", 3)]
		[TestCase(@"'a\' b'", 0, "a' b", 7)]
		[TestCase("\"abcd", 0, "abcd", 5)]
		[TestCase("\"abcd\"", 0, "abcd", 6)]
		[TestCase("'a'b", 0, "a", 3)]
		[TestCase("a'b'", 1, "b", 3)]
		[TestCase("\"abc\"", 0, "abc", 5)]
		[TestCase("\"'\"", 0, "'", 3)]
		[TestCase("b \"a'\"", 2, "a'", 4)]
		[TestCase(@"some_text ""QF \"""" other_text", 10, "QF \"", 7)]

        public void Test(string Line, int StartIndex, string ExpectedValue, int ExpectedLength)
        {
            var ActualToken = QuotedFieldTask.ReadQuotedField(Line, StartIndex);
            Assert.AreEqual(new Token(ExpectedValue, StartIndex, ExpectedLength), ActualToken);
        }
	}

    class QuotedFieldTask
    {
        public static Token ReadQuotedField(string Line, int StartIndex)
        {
            var StringBuilder = new StringBuilder();
            var Length = 1;
            for (var I = StartIndex + 1; I < Line.Length; I++)
            {
                Length++;
                if (Line[StartIndex] == Line[I] && Line[I - 1] != '\\')
                    break;
                if (Line[I] != '\\')
                    StringBuilder.Append(Line[I]);
            }
            return new Token(StringBuilder.ToString(), StartIndex, Length);
        }
    }
}