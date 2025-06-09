public static bool isDigit(string word)
{
	string digits = "1234567890";
	return digits.Contains(word[0]);
}
public static int[] GetBenfordStatistics(string text)
{
	string[] substings = text.Split(' ');
	List<String> onlyDigits = new List<String>();
	foreach (string s in substings)
	{
		if(isDigit(s))
		{
		    onlyDigits.Add(s);
		}
	}
	var statistics = new int[10];
	foreach (string s in onlyDigits)
	{
		int a = int.Parse(s[0].ToString());
		statistics[a]++;
	}
	return statistics;
}