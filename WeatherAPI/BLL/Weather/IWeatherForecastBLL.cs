using WeatherAPI.DAL.Models;

namespace WeatherAPI.BLL.Weather;

public interface IWeatherForecastBLL
{
    Task AddWeatherForecastAsync(WeatherForecastModel model);
    Task<WeatherForecastModel> GetWeatherForecastByIdAsync(long id);
    Task<WeatherForecastModel> GetWeatherForecastByRegionAsync(long id);
    Task UpdateWeatherForecastAsync(WeatherForecastModel model);
    Task DeleteWeatherForecastAsync(long forecastId);
}