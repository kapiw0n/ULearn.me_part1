public class Student
{
    private string name;

    public string Name
    {
        get { return name; }
        set
        {
            if (value == null)
            {
                throw new ArgumentException("Name cannot be null.");
            }
            name = value;
        }
    }
}