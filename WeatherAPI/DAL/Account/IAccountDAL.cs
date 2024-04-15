using WeatherAPI.DAL.Models;

namespace WeatherAPI.DAL.Account;

public interface IAccountDAL
{
    Task<AccountModel> CreateAccountAsync(AccountModel model);
    Task<AccountModel> GetAccountByIdAsync(long id);
    Task<AccountModel> GetAccountByEmailAsync(string email);
    Task<List<AccountModel>> GetAccountByParamsAsync(string firstName, string lastName, string email, int from, int size);
    Task UpdateAccountAsync(AccountModel model);
    Task DeleteAccountAsync(AccountModel model);
}