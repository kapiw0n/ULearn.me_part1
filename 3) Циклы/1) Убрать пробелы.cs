public static string RemoveStartSpaces(string text)
{
	while (text.Length > 0 && char.IsWhiteSpace(text[0]))
	{
		text = text.Remove(0,1);
	}
	return text;
}