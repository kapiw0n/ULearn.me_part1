private static string GetMinX(int a, int b, int c)
{
    if(a==0 && b ==0) {
		return "0";
	} else if (a == 0){
		 return"Impossible" ;
	} else{
		 return((-1.0 * b) / (2.0 * a)).ToString();
	}
}