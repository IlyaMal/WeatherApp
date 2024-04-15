﻿using WeatherAPI.DAL.Models;

namespace WeatherAPI.DAL.Region;

public interface IRegionTypeDAL
{
    Task AddRegionTypeAsync(RegionTypeModel model);
    Task<RegionTypeModel> GetRegionTypeAsync(int id);
    Task<RegionTypeModel> GetRegionTypeByTypeAsync(string type);
    Task UpdateRegionTypeAsync(RegionTypeModel model);
    Task DeleteRegionTypeAsync(RegionTypeModel model);
}