using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
namespace TableParser
{
    [TestFixture]
    public class FieldParserTaskTests
    {
        public static void Test(string Input, string[] ExpectedResult)
        {
            var ActualResult = FieldsParserTask.ParseLine(Input);
            Assert.AreEqual(ExpectedResult.Length, ActualResult.Count);
            for (int I = 0; I < ExpectedResult.Length; ++I)
            {
                Assert.AreEqual(ExpectedResult[I], ActualResult[I].Value);
            }
        }

        [TestCase("text", new[] { "text" })]
        [TestCase("", new string[0])]
        [TestCase("\"\\\"text\\\"\"", new[] { "\"text\"" })]
        [TestCase("'\\\'text\\\''", new[] { "'text'" })]
        [TestCase("'\"text\"", new[] { "\"text\"" })]
        [TestCase("hello  world ", new[] { "hello", "world" })]
        [TestCase("\"'hello' world\"", new[] { "'hello' world" })]
        [TestCase("\"hello\"world", new[] { "hello", "world" })]
        [TestCase(@"""\\""", new[] { "\\" })]
        [TestCase("hello\"world\"", new[] { "hello", "world" })]
        [TestCase("' ", new[] { " " })]
        [TestCase("\'\'", new[] { "" })]
        public static void RunTests(string Input, string[] ExpectedOutput)
        {
            Test(Input, ExpectedOutput);
        }
    }

    public class FieldsParserTask
    {
        public static List<Token> ParseLine(string Line)
        {
            var List = new List<Token>();
            for (var I = 0; I < Line.Length; I++)
            {
                if (Line[I] == ' ')
                    continue;
                var Token = TakeToken(Line, I);
                List.Add(Token);
                I = Token.GetIndexNextToToken() - 1;
            }
            return List;
        }

        public static Token TakeToken(string Line, int I)
        {
            if (Line[I] == '\'' || Line[I] == '\"')
                return ReadQuotedField(Line, I);
            return ReadField(Line, I);
        }

        private static Token ReadField(string Line, int StartIndex)
        {
            var StringBuilder = new StringBuilder();
            for (var I = StartIndex; I < Line.Length; I++)
            {
                if (Line[I] == '\'' || Line[I] == '\"' || Line[I] == ' ')
                    break;
                StringBuilder.Append(Line[I]);
            }
            return new Token(StringBuilder.ToString(), StartIndex, StringBuilder.Length);
        }

        public static Token ReadQuotedField(string Line, int StartIndex)
        {
            return QuotedFieldTask.ReadQuotedField(Line, StartIndex);
        }
    }
}