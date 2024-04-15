using Microsoft.AspNetCore.Mvc;
using WeatherAPI.BLL.Account;
using WeatherAPI.BLL.Region;
using WeatherAPI.BLL.Weather;
using WeatherAPI.DAL.Models;

namespace WeatherAPI.Controllers;

[Route ("/region/")]
[ApiController]
public class WeatherController: ControllerBase
{
    private readonly IWeatherBLL _weatherBll;
    private readonly IAuth _auth;
    private readonly IWeatherForecastBLL _weatherForecastBll;
    private readonly IRegionBLL _regionBll; 
    private string[] conditions = ["CLEAR", "CLOUDY", "RAIN", "SNOW", "FOG", "STORM"];

    public WeatherController(IWeatherBLL weatherBll, IAuth auth, IWeatherForecastBLL weatherForecastBll, IRegionBLL regionBll)
    {
        _weatherBll = weatherBll;
        _auth = auth;
        _weatherForecastBll = weatherForecastBll;
        _regionBll = regionBll;
    }

    [HttpGet("weather/{regionId?}")]
    public async Task<IActionResult> Index(long? regionId)
    {
        if (regionId <= 0 || !regionId.HasValue) return BadRequest();
        if (_auth.IsUnauthorized(Request.Cookies)) return Unauthorized();
        var regionExistedModel = await _regionBll.GetRegionByIdAsync(regionId.Value);
        if (regionExistedModel.id == 0) return NotFound();
        var forecastExistedModel = await _weatherForecastBll.GetWeatherForecastByRegionAsync(regionId.Value);
        if (forecastExistedModel.id == 0) return NotFound();
        var model = await _weatherBll.GetWeatherByIdAsync(regionId.Value);
        
        return Ok(model);
    }

    [HttpPost("weather/")]
    public async Task<IActionResult> IndexAdd([FromBody] WeatherModel model)
    {
        if (_auth.IsUnauthorized(Request.Cookies)) return Unauthorized();
        if (model.regionId <= 0 || model.windSpeed < 0 || !conditions.Contains(model.weatherCondition) ||
            model.precipitationAmount < 0) return BadRequest();
        var regionExistedModel = await _regionBll.GetRegionByIdAsync(model.regionId);
        if (regionExistedModel.id == 0) return NotFound();
        var forecastExistedModel = await _weatherForecastBll.GetWeatherForecastByRegionAsync(model.regionId);
        if (forecastExistedModel.id == 0) return NotFound();
        
        await _weatherBll.AddWeatherAsync(model);
        return Ok(model);
    }

    [HttpPut("weather/{regionId?}")]
    public async Task<IActionResult> IndexPut([FromBody] WeatherModel model, long? regionId)
    {
        if (_auth.IsUnauthorized(Request.Cookies)) return Unauthorized();
        if (!regionId.HasValue || model.regionId <= 0 || model.windSpeed < 0 || !conditions.Contains(model.weatherCondition) ||
            model.precipitationAmount < 0) return BadRequest();
        var regionExistedModel = await _regionBll.GetRegionByIdAsync(regionId.Value);
        if (regionExistedModel.id == 0) return NotFound();
        var forecastExistedModel = await _weatherForecastBll.GetWeatherForecastByRegionAsync(regionId.Value);
        if (forecastExistedModel.id == 0) return NotFound();
        await _weatherBll.UpdateWeatherAsync(model);
        return Ok(model);
    }

    [HttpDelete("weather/{regionId?}")]
    public async Task<IActionResult> IndexDelete(long? regionId)
    {
        if (!regionId.HasValue || regionId <= 0) return BadRequest();
        if (_auth.IsUnauthorized(Request.Cookies)) return Unauthorized();
        var existedModel = await _regionBll.GetRegionByIdAsync(regionId.Value);
        if (existedModel.id == 0) return NotFound();
        await _weatherBll.DeleteWeatherByRegionAsync(regionId.Value);
        return Ok();
    }
    
    [HttpPost("{regionId?}/weather/{weatherId?}")]
    public async Task<IActionResult> IndexPost([FromBody] WeatherModel model, long? regionId)
    {
        if (_auth.IsUnauthorized(Request.Cookies)) return Unauthorized();
        if (model.regionId <= 0 || model.windSpeed < 0 || !conditions.Contains(model.weatherCondition) ||
            model.precipitationAmount < 0) return BadRequest();
        var regionExistedModel = await _regionBll.GetRegionByIdAsync(model.regionId);
        if (regionExistedModel.id == 0) return NotFound();
        var forecastExistedModel = await _weatherForecastBll.GetWeatherForecastByRegionAsync(model.regionId);
        if (forecastExistedModel.id == 0) return NotFound();
        
        await _weatherBll.AddWeatherAsync(model);
        return Ok(model);
    }
    [HttpDelete("{regionId?}/weather/{weatherId?}")]
    public async Task<IActionResult> IndexRemove(long? regionId, long? weatherId)
    {
        if (!regionId.HasValue || regionId <= 0 || !weatherId.HasValue) return BadRequest();
        if (_auth.IsUnauthorized(Request.Cookies)) return Unauthorized();
        var existedModel = await _regionBll.GetRegionByIdAsync(regionId.Value);
        if (existedModel.id == 0) return NotFound();
        await _weatherBll.DeleteWeatherAsync(weatherId.Value);
        return Ok();
    }
}