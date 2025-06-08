public static int GetElementCount(int[] items, int itemToFind)
{
	int amount=0;

	for (int i = 0; i < items.Length; i++)
	{
    	if (items[i] == itemToFind)
    		amount++;
	}
    
	return amount;
}  