using WeatherAPI.DAL;
using WeatherAPI.DAL.Models;
using WeatherAPI.DAL.Region;

namespace WeatherAPI.BLL.Region;

public class RegionTypeBLL: IRegionTypeBLL
{
    private readonly IRegionTypeDAL _regionTypeDal;

    public RegionTypeBLL(IRegionTypeDAL regionTypeDal)
    {
        _regionTypeDal = regionTypeDal;
    }
    
    public async Task AddRegionTypeAsync(RegionTypeModel model)
    {
        await _regionTypeDal.AddRegionTypeAsync(model);
    }

    public async Task<RegionTypeModel> GetRegionTypeAsync(int id)
    {
        return await _regionTypeDal.GetRegionTypeAsync(id);
    }

    public async Task<RegionTypeModel> GetRegionTypeByTypeAsync(string type)
    {
        return await _regionTypeDal.GetRegionTypeByTypeAsync(type);
    }

    public async Task UpdateRegionTypeAsync(RegionTypeModel model)
    {
        await _regionTypeDal.UpdateRegionTypeAsync(model);
    }

    public async Task DeleteRegionTypeAsync(int id)
    {
        var model = await _regionTypeDal.GetRegionTypeAsync(id);
        await _regionTypeDal.DeleteRegionTypeAsync(model);
    }
}