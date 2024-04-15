using WeatherAPI.DAL.Models;

namespace WeatherAPI.BLL.Region;

public interface IRegionBLL
{
    Task<RegionModel> AddRegionAsync(RegionModel model);
    Task<RegionModel> GetRegionByIdAsync(long id);
    Task<RegionModel> GetRegionByTypeAsync(long type);
    Task<bool> GetRegionByCoordinatesAsync(double latitude, double longitude);
    Task<RegionModel> UpdateRegionAsync(RegionModel model);
    Task DeleteRegionAsync(long id);
}