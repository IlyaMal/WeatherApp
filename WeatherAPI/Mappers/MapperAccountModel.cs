using WeatherAPI.DAL.Models;
using WeatherAPI.QueryModel;

namespace WeatherAPI.Mappers;

public class MapperAccountModel
{
    public static ProfileModel MapQueryAccountModelToAccount(AccountModel model)
    {
        return new ProfileModel()
        {
            id = model.id,
            firstName = model.firstName,
            lastName = model.lastName,
            email = model.email

        };
    }
}