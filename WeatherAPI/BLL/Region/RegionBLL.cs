using WeatherAPI.DAL.Models;
using WeatherAPI.DAL.Region;

namespace WeatherAPI.BLL.Region;

public class RegionBLL: IRegionBLL
{
    private readonly IRegionDAL _regionDal;

    public RegionBLL(IRegionDAL regionDal)
    {
        _regionDal = regionDal;
    }
    
    public async Task<RegionModel> AddRegionAsync(RegionModel model)
    {
        await _regionDal.AddRegionAsync(model);
        return model;
    }

    public async Task<RegionModel> GetRegionByIdAsync(long id)
    {
        return await _regionDal.GetRegionByIdAsync(id);
    }

    public async Task<RegionModel> GetRegionByTypeAsync(long type)
    {
        return await _regionDal.GetRegionByTypeAsync(type);
    }

    public async Task<bool> GetRegionByCoordinatesAsync(double latitude, double longitude)
    {
        var model = await _regionDal.GetRegionByCoordinatesAsync(latitude, longitude);
        if (model.id == 0) return false;
        return true;
    }

    public async Task<RegionModel> UpdateRegionAsync(RegionModel model)
    {
        await _regionDal.UpdateRegionAsync(model);
        return model;
    }

    public async Task DeleteRegionAsync(long id)
    {
        var model = await _regionDal.GetRegionByIdAsync(id);
        await _regionDal.DeleteRegionAsync(model);
    }
}