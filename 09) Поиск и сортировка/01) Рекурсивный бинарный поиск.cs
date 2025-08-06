public static int BinSearchLeftBorder(long[] array, long value, int left, int right)
{
    if (left + 1 == right)
        return left;
    var m = (left + right) / 2;
    if (array[m] < value)
        return BinSearchLeftBorder(array, value, m, right);
    else
        return BinSearchLeftBorder(array, value, left, m);
}