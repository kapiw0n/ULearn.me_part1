static void MakeSubsets(char[] subset, int position = 0)
{
	if (position == subset.Length)
	{
		Console.WriteLine(new string(subset));
		return;
	}
	subset[position] = 'a';
	MakeSubsets(subset, position + 1);
	subset[position] = 'b';
	MakeSubsets(subset, position + 1);
	subset[position] = 'c';
	MakeSubsets(subset, position + 1);
}