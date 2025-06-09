private static string ApplyCommands(string[] commands)
{
    var res = new StringBuilder();
    foreach (var i in commands)
    {
        string keyWord = i.Substring(0, i.IndexOf(' '));
        string l = i.Remove(0, keyWord.Length + 1);
        if (keyWord == "push") res.Append(l);
        else if (keyWord == "pop")
        {
            int c = int.Parse(l);
            res.Remove(res.Length - c, c);
        }
    }
    return res.ToString();
}