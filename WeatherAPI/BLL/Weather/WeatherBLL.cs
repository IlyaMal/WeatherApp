using WeatherAPI.DAL.Models;
using WeatherAPI.DAL.Region;
using WeatherAPI.DAL.Weather;

namespace WeatherAPI.BLL.Weather;

public class WeatherBLL: IWeatherBLL
{
    private readonly IWeatherDAL _weatherDal;

    public WeatherBLL(IWeatherDAL weatherDal)
    {
        _weatherDal = weatherDal;
    }
    public async Task AddWeatherAsync(WeatherModel model)
    {
        await _weatherDal.AddWeatherAsync(model);
    }

    public async Task<WeatherModel> GetWeatherByIdAsync(long id)
    {
        return await _weatherDal.GetWeatherByIdAsync(id);
    }
    
    public async Task<WeatherModel> GetWeatherByRegionIdAsync(long id)
    {
        return await _weatherDal.GetWeatherByIdAsync(id);
    }

    public async Task UpdateWeatherAsync(WeatherModel model)
    {
        await _weatherDal.UpdateWeatherAsync(model);
    }

    public async Task DeleteWeatherAsync(long id)
    {
        await _weatherDal.DeleteWeatherAsync(id);
    }
    public async Task DeleteWeatherByRegionAsync(long regionId)
    {
        await _weatherDal.DeleteWeatherByRegionAsync(regionId);
    }
}