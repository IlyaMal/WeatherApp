namespace WeatherAPI.QueryModel;

public class QueryRegionModel
{
    public string name { get; set; }
    public string parentRegion { get; set; }
    public long regionType { get; set; } 
    public double latitude { get; set; }
    public double longitude { get; set; }
}