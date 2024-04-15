using Microsoft.AspNetCore.Mvc;
using WeatherAPI.BLL.Account;
using WeatherAPI.BLL.Region;
using WeatherAPI.DAL.Models;

namespace WeatherAPI.Controllers;

[Route("/region/types/")]
[ApiController]
public class RegionTypeController: ControllerBase
{
    private readonly IRegionTypeBLL _regionTypeBll;
    private readonly IRegionBLL _regionBll;
    private readonly IAuth _auth;

    public RegionTypeController(IRegionTypeBLL regionTypeBll, IAuth auth, IRegionBLL regionBll)
    {
        _regionTypeBll = regionTypeBll;
        _regionBll = regionBll;
        _auth = auth;
    }

    [HttpGet("{typeId?}")]
    public async Task<IActionResult> Index(int? typeId)
    {
        if (typeId <= 0 || !typeId.HasValue) return BadRequest();
        if (_auth.IsUnauthorized(Request.Cookies)) return Unauthorized();
        var model = await _regionTypeBll.GetRegionTypeAsync(typeId.Value);
        if (model.id == 0) return NotFound();
        return Ok(model);
    }

    [HttpPost]
    public async Task<IActionResult> IndexAdd([FromBody] RegionTypeModel model)
    {
        if (model.type.Equals(null) || model.type.All(char.IsWhiteSpace)) return BadRequest();
        if (_auth.IsUnauthorized(Request.Cookies)) return Unauthorized();
        var existedModel = await _regionTypeBll.GetRegionTypeByTypeAsync(model.type);
        if (existedModel.id > 0) return Conflict();
        await _regionTypeBll.AddRegionTypeAsync(model);
        return Ok(model);
    }

    [HttpPut("{typeId?}")]
    public async Task<IActionResult> IndexUpdate([FromBody] RegionTypeModel model, int? typeId)
    {
        if (typeId <= 0 || !typeId.HasValue || model.type.All(char.IsWhiteSpace)) return BadRequest();
        if (_auth.IsUnauthorized(Request.Cookies)) return Unauthorized();
        var existedModel = await _regionTypeBll.GetRegionTypeByTypeAsync(model.type);
        if (existedModel.id > 0) return Conflict();
        existedModel = await _regionTypeBll.GetRegionTypeAsync(typeId.Value);
        if (existedModel.id == 0) return NotFound();
        model.id = typeId.Value;
        await _regionTypeBll.UpdateRegionTypeAsync(model);
        return Ok();
    }

    [HttpDelete("{typeId?}")]
    public async Task<IActionResult> IndexDelete(int? typeId)
    {
        if (typeId <= 0 || !typeId.HasValue) return BadRequest();
        var typeExistedModel = await _regionTypeBll.GetRegionTypeAsync(typeId.Value);
        if (typeExistedModel.id == 0) return NotFound();
        if (_auth.IsUnauthorized(Request.Cookies)) return Unauthorized();
        var regionExistedModel = await _regionBll.GetRegionByTypeAsync(typeId.Value);
        if (regionExistedModel.id > 0) return BadRequest();
        await _regionTypeBll.DeleteRegionTypeAsync(typeId.Value);
        return Ok();
    }
}