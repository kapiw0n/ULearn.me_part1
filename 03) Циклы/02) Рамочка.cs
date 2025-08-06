private static void WriteTextWithBorder(string text)
{
	string line="";
	string midLine="| "+text+" |";
	for (int i = 0; i < text.Length + 4; i++)
	{
		if (i == 0 || i == text.Length+3)
		{
			line = line + '+';
		}
		else
		{
			line = line + '-';
		}
	}
	Console.WriteLine(line);
	Console.WriteLine(midLine);
	Console.WriteLine(line);
}