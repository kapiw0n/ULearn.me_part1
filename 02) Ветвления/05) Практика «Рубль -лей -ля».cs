public static class PluralizeTask
{
    public static string PluralizeRubles(int Сount)
    {
        if (Сount % 10 == 1 && Сount % 100 != 11)
            return "рубль";
        else if ((Сount % 100 < 10 || Сount % 100 >= 20) && Сount % 10 >= 2 && Сount % 10 <= 4)
            return "рубля";
        else
            return "рублей";
    }
}