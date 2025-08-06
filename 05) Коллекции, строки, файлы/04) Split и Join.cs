public static string ReplaceIncorrectSeparators(string text)
{
    return text.Replace(" ","\t")
    .Replace(":","\t")
    .Replace(";","\t")
    .Replace("-","\t")
    .Replace(",","\t")
    .Replace("\t\t","\t")
    .Replace("\t\t","\t");
}