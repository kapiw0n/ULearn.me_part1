public static int Decode(string str)
{
 	return int.Parse(str.Replace(".", "")) % 1024;
}