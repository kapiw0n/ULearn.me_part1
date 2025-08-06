public static void Print(params object[] args)
{
    for (var i = 0; i < args.Length; i++)
    {
        if (i > 0) 
            Console.Write(", ");
        Console.Write(args[i]);
    }
    Console.WriteLine();
}