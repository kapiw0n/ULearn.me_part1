static bool ContainsAtIndex(int[] array, int[] subArray, int index)
{
    for (int j = 0; j < subArray.Length; j++)
    {
        if (array[index + j] != subArray[j])
        {
            return false;
        }
    }

    return true;
}