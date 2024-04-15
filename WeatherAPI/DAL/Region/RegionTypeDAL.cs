using Microsoft.EntityFrameworkCore;
using WeatherAPI.DAL.Models;

namespace WeatherAPI.DAL.Region;

public class RegionTypeDAL: IRegionTypeDAL
{
    public async Task AddRegionTypeAsync(RegionTypeModel model)
    {
        using (var connection = new DBHelper())
        {
            await connection.RegionTypes.AddAsync(model);
            await connection.SaveChangesAsync();
        }
    }

    public async Task<RegionTypeModel> GetRegionTypeAsync(int id)
    {
        using (var connection = new DBHelper())
        {
            return await connection.RegionTypes.FindAsync(id) ?? new RegionTypeModel();
        }
    }

    public async Task<RegionTypeModel> GetRegionTypeByTypeAsync(string type)
    {
        using (var connection = new DBHelper())
        {
            return await connection.RegionTypes.FirstOrDefaultAsync(rt=>rt.type == type) ?? new RegionTypeModel();
        }
    }

    public async Task UpdateRegionTypeAsync(RegionTypeModel model)
    {
        using (var connection = new DBHelper())
        {
            connection.RegionTypes.Update(model);
            await connection.SaveChangesAsync();
        }
    }

    public async Task DeleteRegionTypeAsync(RegionTypeModel model)
    {
        using (var connections = new DBHelper())
        {
            connections.RegionTypes.Remove(model);
            await connections.SaveChangesAsync();
        }
    }
}