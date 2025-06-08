public static int[] GetPoweredArray(int[] arr, int power)
{   
	int[] result = new int[arr.Length];
	for (int i = 0; i < arr.Length; i++)
	{
    	result[i] = (int)(Math.Pow(arr[i], power));
	}
	return result;
}