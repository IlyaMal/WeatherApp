using WeatherAPI.DAL.Models;

namespace WeatherAPI.BLL.Region;

public interface IRegionTypeBLL
{
    Task AddRegionTypeAsync(RegionTypeModel model);
    Task<RegionTypeModel> GetRegionTypeAsync(int id);
    Task<RegionTypeModel> GetRegionTypeByTypeAsync(string type);
    Task UpdateRegionTypeAsync(RegionTypeModel model);
    Task DeleteRegionTypeAsync(int id);
}