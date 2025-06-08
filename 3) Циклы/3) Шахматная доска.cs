private static void WriteBoard(int size)
{
	string row;
	for (int i = 0; i < size; i++)
	{
		row = "";
		for (int j = 0; j < size; j++)
		{
			row += (j+i)%2==0 ? "#" : ".";
		}
		Console.WriteLine(row);
	}
	Console.WriteLine();
}