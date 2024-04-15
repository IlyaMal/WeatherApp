using WeatherAPI.DAL.Models;

namespace WeatherAPI.BLL.Account;

public interface IAccountBLL
{
    Task<AccountModel> Register(AccountModel model);
    Task<long> Login(string email, string password);
    Task<AccountModel> GetAccount(long id);
    Task<Boolean> GetAccountByEmail(string email);
    Task<List<ProfileModel>> SearchAccount(string firstName, string lastName, string email, int from, int size);
    Task<AccountModel> UpdateAccount(AccountModel model);
    Task DeleteAccount(long id);
}