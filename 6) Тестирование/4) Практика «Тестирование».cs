[TestCase("text", new[] { "text" })]
[TestCase("hello world", new[] { "hello", "world" })]
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