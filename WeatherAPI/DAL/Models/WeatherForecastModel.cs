namespace WeatherAPI.DAL.Models;

public class WeatherForecastModel
{
    public long id { get; set; }
    public DateTime dateTime { get; set; }
    public float temperature { get; set; }
    public string weatherCondition { get; set; }
    public long regionId { get; set; }
}