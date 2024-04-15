using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WeatherAPI.BLL.Account;
using WeatherAPI.BLL.Region;
using WeatherAPI.DAL.Models;

namespace WeatherAPI.Controllers;

[Route("/region/")]
[ApiController]
public class RegionController: ControllerBase
{
    private readonly IRegionBLL _regionBll;
    private readonly IAuth _auth;

    public RegionController(IRegionBLL regionBll, IAuth auth)
    {
        _regionBll = regionBll;
        _auth = auth;
    }
    
    [HttpGet("{regionId?}")]
    public async Task<IActionResult> Index(long? regionId)
    {
        if (regionId <= 0 || !regionId.HasValue) return BadRequest();
        if (_auth.IsUnauthorized(Request.Cookies)) return Unauthorized();
        var model = await _regionBll.GetRegionByIdAsync(regionId.Value);
        if (model.id == 0) return NotFound();
        return Ok(model); 
    }

    [HttpPost]
    public async Task<IActionResult> IndexAdd([FromBody] RegionModel model)
    {
        if (model.latitude.Equals(null) || model.longitude.Equals(null) || model.name.Equals(null)) return BadRequest();
        if (_auth.IsUnauthorized(Request.Cookies)) return Unauthorized();
        model.accountId = long.Parse(Request.Cookies["userId"]!); 
        bool check = await _regionBll.GetRegionByCoordinatesAsync(model.latitude, model.longitude);
        if (check) return Conflict();
        await _regionBll.AddRegionAsync(model);
        var region = new
        {
            id = model.id,
            name = model.name,
            parentRegion = model.parentRegion,
            regionType = model.regionType,
            latitude = model.latitude,
            longitude = model.longitude

        };
        return new ContentResult
        {
            StatusCode = 201,
            Content = JsonConvert.SerializeObject(region),
            ContentType = "application/json"
        };
    }

    [HttpPut("{regionId?}")]
    public async Task<IActionResult> IndexPut([FromBody] RegionModel model, long? regionId)
    {
        if (model.latitude.Equals(null) || model.longitude.Equals(null) || model.name.Equals(null) || !regionId.HasValue) return BadRequest();
        if (_auth.IsUnauthorized(Request.Cookies)) return Unauthorized();
        var existedModel = await _regionBll.GetRegionByIdAsync(regionId.Value);
        if (existedModel.id == 0) return NotFound();
        bool check = await _regionBll.GetRegionByCoordinatesAsync(model.latitude, model.longitude);
        if (check) return Conflict();
        model.id = regionId.Value;
        await _regionBll.UpdateRegionAsync(model);
        var region = new
        {
            id = model.id,
            name = model.name,
            parentRegion = model.parentRegion,
            regionType = model.regionType,
            latitude = model.latitude,
            longitude = model.longitude

        };
        return Ok(region);
    }

    [HttpDelete("{regionId?}")]
    public async Task<IActionResult> IndexDelete(int? regionId)
    {
        if (regionId <= 0 || !regionId.HasValue) return BadRequest();
        if (_auth.IsUnauthorized(Request.Cookies)) return Unauthorized();
        var existedModel = await _regionBll.GetRegionByIdAsync(regionId.Value);
        if (existedModel.id == 0) return NotFound();
        await _regionBll.DeleteRegionAsync(regionId.Value);
        return Ok();
    }
}