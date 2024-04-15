using Microsoft.EntityFrameworkCore;
using WeatherAPI.DAL.Models;

namespace WeatherAPI.DAL.Weather;

public class WeatherForecastDAL: IWeatherForecastDAL
{
    public async Task AddWeatherForecastAsync(WeatherForecastModel model)
    {
        using (var connection = new DBHelper())
        {
            await connection.Forecasts.AddAsync(model);
            await connection.SaveChangesAsync();
        }
    }

    public async Task<WeatherForecastModel> GetWeatherForecastByIdAsync(long id)
    {
        using (var connection = new DBHelper())
        {
            return await connection.Forecasts.FindAsync(id) ?? new WeatherForecastModel();
        }
    }

    public async Task<WeatherForecastModel> GetWeatherForecastByRegionAsync(long id)
    {
        using (var connection = new DBHelper())
        {
            return await connection.Forecasts.FirstOrDefaultAsync(wf=>wf.regionId == id) ?? new WeatherForecastModel();
        }
    }

    public async Task UpdateWeatherAsync(WeatherForecastModel model)
    {
        await using var context = new DBHelper();
        await Task.Run(() => context.Forecasts.Update(model));
        await context.SaveChangesAsync();
    }

    public async Task DeleteWeatherAsync(WeatherForecastModel model)
    {
        await using var context = new DBHelper();
        await Task.Run(() => context.Forecasts.Remove(model));
        await context.SaveChangesAsync();
    }
}