public static void WriteReversed(char[] items, int startIndex = 0)
{
	if (items.Length != 0)
    {
		if (startIndex != items.Length - 1)
        {
			WriteReversed(items, startIndex + 1);
        }
        Console.Write(items[startIndex]);
    }
}