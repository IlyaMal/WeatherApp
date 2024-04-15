using WeatherAPI.DAL.Models;

namespace WeatherAPI.DAL.Weather;

public interface IWeatherForecastDAL
{
    Task AddWeatherForecastAsync(WeatherForecastModel model);
    Task<WeatherForecastModel> GetWeatherForecastByIdAsync(long id);
    Task<WeatherForecastModel> GetWeatherForecastByRegionAsync(long id);
    Task UpdateWeatherAsync(WeatherForecastModel model);
    Task DeleteWeatherAsync(WeatherForecastModel model);
}