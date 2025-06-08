public static double Calculate(string input)
{
    var values = input.Split(' ');
	var principal = double.Parse(values[0]);
	var interestRate = double.Parse(values[1]) / 1200;
	var timePeriod = double.Parse(values[2]);

	return principal * Math.Pow(1 + interestRate, timePeriod);
}