using WeatherAPI.DAL.Models;

namespace WeatherAPI.BLL.Weather;

public interface IWeatherBLL
{
    Task AddWeatherAsync(WeatherModel model);
    Task<WeatherModel> GetWeatherByIdAsync(long id);
    Task<WeatherModel> GetWeatherByRegionIdAsync(long id);
    Task UpdateWeatherAsync(WeatherModel model);
    Task DeleteWeatherByRegionAsync(long regionId);
    Task DeleteWeatherAsync(long id);
}