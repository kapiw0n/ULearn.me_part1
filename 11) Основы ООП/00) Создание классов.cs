public class City
{
    public string Name { get; set; }
    public GeoLocation Location { get; set; }
}

public class GeoLocation
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}