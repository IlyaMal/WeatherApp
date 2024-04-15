namespace WeatherAPI.DAL.Models;

public class RegionModel
{
    public long id { get; set; }
    public long regionType { get; set; }
    public long accountId { get; set; }
    public string name { get; set; }
    public string parentRegion { get; set; }
    public double latitude { get; set; }
    public double longitude { get; set; }
}