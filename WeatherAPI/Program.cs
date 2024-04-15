using WeatherAPI.BLL.Account;
using WeatherAPI.BLL.Region;
using WeatherAPI.BLL.Weather;
using WeatherAPI.DAL.Account;
using WeatherAPI.DAL.Region;
using WeatherAPI.DAL.Weather;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddSingleton<IAccountDAL, AccountDAL>();
builder.Services.AddScoped<IAccountBLL, AccountBLL>();
builder.Services.AddSingleton<IEncrypt, Encrypt>();

builder.Services.AddSingleton<IAuth, Auth>();

builder.Services.AddSingleton<IRegionDAL, RegionDAL>();
builder.Services.AddScoped<IRegionBLL, RegionBLL>();

builder.Services.AddSingleton<IRegionTypeDAL, RegionTypeDAL>();
builder.Services.AddScoped<IRegionTypeBLL, RegionTypeBLL>();

builder.Services.AddSingleton<IWeatherDAL, WeatherDAL>();
builder.Services.AddScoped<IWeatherBLL, WeatherBLL>();

builder.Services.AddSingleton<IWeatherForecastDAL, WeatherForecastDAL>();
builder.Services.AddScoped<IWeatherForecastBLL, WeatherForecastBLL>();


var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{ 
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};


app.UseRouting();

app.UseEndpoints(endpoints => 
{
    endpoints.MapControllers();
});
app.Run();

