using Microsoft.AspNetCore.Mvc;
using WeatherAPI.BLL.Account;
using WeatherAPI.BLL.Region;
using WeatherAPI.BLL.Weather;
using WeatherAPI.DAL.Models;

namespace WeatherAPI.Controllers;

[Route ("/region/weather/forecast/")]
[ApiController]
public class WeatherForecastController: ControllerBase
{
    private readonly IWeatherForecastBLL _weatherForecastBll;
    private readonly IAuth _auth;
    private readonly IRegionBLL _regionBll;
    private string[] conditions = ["CLEAR", "CLOUDY", "RAIN", "SNOW", "FOG", "STORM"];

    public WeatherForecastController(IWeatherForecastBLL weatherForecastBll, IAuth auth, IRegionBLL regionBll)
    {
        _weatherForecastBll = weatherForecastBll;
        _auth = auth;
        _regionBll = regionBll;
    }
    
    [HttpGet("{forecastId?}")]
    public async Task<IActionResult> Index(long? forecastId)
    {
        if (_auth.IsUnauthorized(Request.Cookies)) return Unauthorized();
        if (!forecastId.HasValue || forecastId <= 0) return BadRequest();
        var model = await _weatherForecastBll.GetWeatherForecastByIdAsync(forecastId.Value);
        if (model.id == 0) return NotFound();
        return Ok(model);
    }

    [HttpPost]
    public async Task<IActionResult> IndexAdd([FromBody] WeatherForecastModel model)
    {
        if (_auth.IsUnauthorized(Request.Cookies)) return Unauthorized();
        if (model.regionId.Equals(null) || model.regionId <= 0 || !conditions.Contains(model.weatherCondition))
            return BadRequest();
        var existedModel = await _regionBll.GetRegionByIdAsync(model.regionId);
        if (existedModel.id == 0) return NotFound();
        await _weatherForecastBll.AddWeatherForecastAsync(model);
        return Ok(model);
        
    }

    [HttpPut("{forecastId?}")]
    public async Task<IActionResult> IndexUpdate([FromBody] WeatherForecastModel model, long? forecastId)
    {
        if (_auth.IsUnauthorized(Request.Cookies)) return Unauthorized();
        if (!forecastId.HasValue || forecastId <= 0 || conditions.Contains(model.weatherCondition)) return BadRequest();
        var existedModel = await _weatherForecastBll.GetWeatherForecastByIdAsync(forecastId.Value);
        if (existedModel.id == 0) return NotFound();
        model.id = forecastId.Value;
        await _weatherForecastBll.UpdateWeatherForecastAsync(model);
        return Ok(model);
    }

    [HttpDelete("{forecastId?}")]
    public async Task<IActionResult> IndexDelete(long? forecastId)
    {
        if (_auth.IsUnauthorized(Request.Cookies)) return Unauthorized();
        if (!forecastId.HasValue || forecastId <= 0) return BadRequest();
        var existedModel = await _weatherForecastBll.GetWeatherForecastByIdAsync(forecastId.Value);
        if (existedModel.id == 0) return NotFound();
        await _weatherForecastBll.DeleteWeatherForecastAsync(forecastId.Value);
        return Ok();
    }
}