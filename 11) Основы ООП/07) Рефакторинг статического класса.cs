public class SuperBeautyImageFilter
{
    public string ImageName { get; set; }
    public double GaussianParameter { get; set; }

    public void Run()
    {
        Console.WriteLine("Processing {0} with parameter {1}", 
            ImageName, 
            GaussianParameter.ToString(CultureInfo.InvariantCulture));
    }
}