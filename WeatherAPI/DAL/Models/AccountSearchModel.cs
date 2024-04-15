namespace WeatherAPI.DAL.Models;

public class AccountSearchModel
{
    public string firstName { get; set; }
    public string lastName { get; set; }
    public string email { get; set; }
    public int from { get; set; }
    public int size { get; set; }
}