namespace WeatherAPI.BLL.Account;

public interface IEncrypt
{
    string HashPassword(string password, string salt);
}