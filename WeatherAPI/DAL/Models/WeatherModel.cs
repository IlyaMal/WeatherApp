namespace WeatherAPI.DAL.Models;

public class WeatherModel
{
    public long id { get; set; }
    public string regionName { get; set; }
    public float temperature { get; set; }
    public float humidity { get; set; }
    public float windSpeed { get; set; }
    public string weatherCondition { get; set; }
    public float precipitationAmount { get; set; }
    public DateTime measurementDateTime { get; set; }
    public List<long> weatherForecast { get; set; }
    public long regionId { get; set; }
    

}