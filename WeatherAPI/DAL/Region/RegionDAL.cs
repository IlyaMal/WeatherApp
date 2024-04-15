using System.Data.Entity;
using WeatherAPI.DAL.Models;

namespace WeatherAPI.DAL.Region;

public class RegionDAL: IRegionDAL
{
    public async Task AddRegionAsync(RegionModel model)
    {
        using (var connection = new DBHelper())
        {
            await connection.Regions.AddAsync(model);
            await connection.SaveChangesAsync();
        }
    }

    public async Task<RegionModel> GetRegionByIdAsync(long id)
    {
        using (var connections = new DBHelper())
        {
            return await connections.Regions.FindAsync(id) ?? new RegionModel();
        }
    }

    public async Task<RegionModel> GetRegionByNameAsync(string name)
    {
        using (var connection = new DBHelper())
        {
            return await connection.Regions.FirstOrDefaultAsync(r => r.name == name) ?? new RegionModel();
        }
    }

    public async Task<RegionModel> GetRegionByTypeAsync(long type)
    {
        using (var connection = new DBHelper())
        {
            return await connection.Regions.FirstOrDefaultAsync(r => r.regionType == type) ?? new RegionModel();
        }
    }

    public async Task<RegionModel> GetRegionByCoordinatesAsync(double latitude, double longitude)
    {
        using (var connection = new DBHelper())
        {
            return connection.Regions.FirstOrDefault(r =>
                r.latitude == latitude && r.longitude == longitude) ?? new RegionModel();
        }
    }

    public async Task UpdateRegionAsync(RegionModel model)
    {
        using (var connection = new DBHelper())
        {
            connection.Regions.Update(model);
            await connection.SaveChangesAsync();
        }
    }

    public async Task DeleteRegionAsync(RegionModel model)
    {
        using (var connection = new DBHelper())
        {
            connection.Regions.Remove(model);
            await connection.SaveChangesAsync();
        }
    }
}