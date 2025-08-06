static Array Combine(params Array[] arrays)
    {
        if (arrays == null || arrays.Length == 0)
        {
            return null;
        }

        Type elementType = arrays[0].GetType().GetElementType();
        int totalLength = 0;
        foreach (var array in arrays)
        {
            if (array == null || array.GetType().GetElementType() != elementType)
            {
                return null;
            }
            totalLength += array.Length;
        }

        Array result = Array.CreateInstance(elementType, totalLength);
        if (result == null)
        {
            return null;
        }

        int currentIndex = 0;
        foreach (var array in arrays)
        {
            Array.Copy(array, 0, result, currentIndex, array.Length);
            currentIndex += array.Length;
        }

        return result;
    }