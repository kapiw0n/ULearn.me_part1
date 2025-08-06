static T Min<T>(T[] array) where T : IComparable<T>
{
    if (array == null || array.Length == 0)
    {
        throw new ArgumentException("Array cannot be null or empty.");
    }

    T minValue = array[0];
    foreach (var item in array)
    {
        if (item.CompareTo(minValue) < 0)
        {
            minValue = item;
        }
    }
    return minValue;
}