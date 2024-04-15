namespace WeatherAPI.DAL.Models;

public class AccountModel
{
    public long id { get; set; }
    public string firstName { get; set; }
    public string lastName { get; set; }
    public string email { get; set; }
    public string password { get; set; }
    public string? salt { get; set; }
    
}