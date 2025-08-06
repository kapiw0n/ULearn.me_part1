public static void WriteAllNumbersFromText(string text)
{
	int pos = 0;
	string finalText = "";
	while (pos < text.Length)
	{
		SkipSpaces(text, ref pos);
		finalText += ReadNumber(text, ref pos) + " ";
	}
	Console.WriteLine(finalText);
}