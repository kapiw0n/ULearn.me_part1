public static int[] GetFirstEvenNumbers(int count)
{
    int[] array = new int[count];
    
    for (int k = 1; k <= count; k++)
        array[k - 1] = k * 2;

    return array;
}