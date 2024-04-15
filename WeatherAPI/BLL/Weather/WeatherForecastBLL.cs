using WeatherAPI.DAL.Models;
using WeatherAPI.DAL.Weather;

namespace WeatherAPI.BLL.Weather;

public class WeatherForecastBLL: IWeatherForecastBLL
{
    private readonly IWeatherForecastDAL _weatherForecastDal;

    public WeatherForecastBLL(IWeatherForecastDAL weatherForecastDal)
    {
        _weatherForecastDal = weatherForecastDal;
    }
    
    public async Task AddWeatherForecastAsync(WeatherForecastModel model)
    {
        await _weatherForecastDal.AddWeatherForecastAsync(model);
    }

    public async Task<WeatherForecastModel> GetWeatherForecastByIdAsync(long id)
    {
        return await _weatherForecastDal.GetWeatherForecastByIdAsync(id);
    }

    public async Task<WeatherForecastModel> GetWeatherForecastByRegionAsync(long id)
    {
        return await _weatherForecastDal.GetWeatherForecastByRegionAsync(id);
    }

    public async Task UpdateWeatherForecastAsync(WeatherForecastModel model)
    {
        await _weatherForecastDal.UpdateWeatherAsync(model);
    }

    public async Task DeleteWeatherForecastAsync(long forecastId)
    {
        var model = await _weatherForecastDal.GetWeatherForecastByIdAsync(forecastId);
        await _weatherForecastDal.DeleteWeatherAsync(model);
    }
}