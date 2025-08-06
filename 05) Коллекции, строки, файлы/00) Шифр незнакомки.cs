private static string DecodeMessage(string[] lines)
{
	string decodedMessage = "";
	for(int i = lines.Length - 1; i >= 0; i--) {
		string word = lines[i];
		string[] words = word.Split(' ');
		
		for(int k = words.Length - 1; k >= 0; k--) 
		{
			string res = words[k];
			if((res.Length != 0) && (char.IsUpper(res[0]))) 
			{
				decodedMessage = decodedMessage + res + " ";
			}
		}
	}
	return decodedMessage;
}