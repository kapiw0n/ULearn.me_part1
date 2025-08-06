static string GetLastHalf(string text)
{
   int str = text.Length/2;
   return (text.Substring(str)).Replace(" ", string.Empty);
}
