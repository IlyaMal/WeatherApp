using Microsoft.EntityFrameworkCore;
using WeatherAPI.DAL.Models;

namespace WeatherAPI.DAL.Weather;

public class WeatherDAL: IWeatherDAL
{
    public async Task AddWeatherAsync(WeatherModel model)
    {
        using (var connection = new DBHelper())
        {
            await connection.Weather.AddAsync(model);
            await connection.SaveChangesAsync();
        }
    }

    public async Task<WeatherModel> GetWeatherByIdAsync(long id)
    {
        using (var connection = new DBHelper())
        {
            return await connection.Weather.FindAsync(id) ?? new WeatherModel();
        }
    }
    
    public async Task<WeatherModel> GetWeatherByRegionIdAsync(long id)
    {
        using (var connection = new DBHelper())
        {
            var model = await connection.Weather.FirstOrDefaultAsync(w => w.regionId == id) ?? new WeatherModel();
            return model;
        }
    }

    public async Task UpdateWeatherAsync(WeatherModel model)
    {
        await using var context = new DBHelper();
        await Task.Run(() => context.Weather.Update(model));
        await context.SaveChangesAsync();
    }
    public async Task DeleteWeatherAsync(long id)
    {
        await using var context = new DBHelper();
        var model = await context.Weather.FirstOrDefaultAsync(w => w.id == id) ?? new WeatherModel();
        await Task.Run(() => context.Weather.Remove(model));
        await context.SaveChangesAsync();
    }

    public async Task DeleteWeatherByRegionAsync(long regionId)
    {
        await using var context = new DBHelper();
        var model = await context.Weather.FirstOrDefaultAsync(w => w.regionId == regionId) ?? new WeatherModel();
        await Task.Run(() => context.Weather.Remove(model));
        await context.SaveChangesAsync();
    }
}