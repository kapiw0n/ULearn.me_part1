private static string GetGreetingMessage(string name, double salary)
{
	int salaryy = (int)Math.Ceiling(salary);
	string s1 = "Hello, ";
	string s2 = (string)name;
	string s3 = ", your salary is ";
	string s4 = salaryy.ToString();
	return s1+s2+s3+s4;
}
