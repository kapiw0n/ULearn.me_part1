public static void BubbleSortRange(int[] array, int left, int right)
{
    if (left < 0 || right >= array.Length || left >= right)
        return;
    for (int i = left; i <= right; i++)
    {
        for (int j = left; j < right - (i - left); j++)
        {
            if (array[j] > array[j + 1])
            {
                var t = array[j + 1];
                array[j + 1] = array[j];
                array[j] = t;
            }
        }
    }
}