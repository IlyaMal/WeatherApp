using Microsoft.EntityFrameworkCore;
using WeatherAPI.DAL.Models;

namespace WeatherAPI.DAL;

public class DBHelper: DbContext
{
    public DbSet<AccountModel> Accounts { get; set; } = null!;
    public DbSet<RegionModel> Regions { get; set; } = null!;
    public DbSet<RegionTypeModel> RegionTypes { get; set; } = null!;
    public DbSet<WeatherModel> Weather { get; set; } = null!;
    public DbSet<WeatherForecastModel> Forecasts { get; set; } = null!;
    
    public DBHelper()
    {
        Database.EnsureCreated();
        
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=weatherDB;Username=postgres;Password=Vomber123");
    }
}