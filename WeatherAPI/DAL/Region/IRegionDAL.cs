using WeatherAPI.DAL.Models;

namespace WeatherAPI.DAL.Region;

public interface IRegionDAL
{
    Task AddRegionAsync(RegionModel model);
    Task<RegionModel> GetRegionByIdAsync(long id);
    Task<RegionModel> GetRegionByNameAsync(string name);
    Task<RegionModel> GetRegionByTypeAsync(long type);
    Task<RegionModel> GetRegionByCoordinatesAsync(double latitude, double longitude);
    Task UpdateRegionAsync(RegionModel model);
    Task DeleteRegionAsync(RegionModel model);
}