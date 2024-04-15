using WeatherAPI.DAL.Models;

namespace WeatherAPI.DAL.Weather;

public interface IWeatherDAL
{
    Task AddWeatherAsync(WeatherModel model);
    Task<WeatherModel> GetWeatherByIdAsync(long id);
    Task<WeatherModel> GetWeatherByRegionIdAsync(long id);
    Task UpdateWeatherAsync(WeatherModel model);
    Task DeleteWeatherByRegionAsync(long regionId);
    Task DeleteWeatherAsync(long regionId);
}