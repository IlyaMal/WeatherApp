using WeatherAPI.DAL.Account;
using WeatherAPI.DAL.Models;
using WeatherAPI.Mappers;

namespace WeatherAPI.BLL.Account;

public class AccountBLL: IAccountBLL
{
    private readonly IAccountDAL _accountDal;
    private readonly IEncrypt _encrypt;

    public AccountBLL(IAccountDAL accountDal, IEncrypt encrypt)
    {
        _accountDal = accountDal;
        _encrypt = encrypt;
    }
    public async Task<AccountModel> Register(AccountModel model)
    {
        model.salt = Guid.NewGuid().ToString();
        model.password = _encrypt.HashPassword(model.password, model.salt);
        await _accountDal.CreateAccountAsync(model);
        return model;
    }

    public async Task<long> Login(string email, string password)
    {
        var model = await _accountDal.GetAccountByEmailAsync(email);
        if (model.password == _encrypt.HashPassword(password, model.salt))
        {
            return model.id;
        }

        return 0;
    }

    public async Task<AccountModel> GetAccount(long id)
    {
        return await _accountDal.GetAccountByIdAsync(id);
    }

    public async Task<Boolean> GetAccountByEmail(string email)
    {
        var model = await _accountDal.GetAccountByEmailAsync(email);
        if (model.id == 0) return false;
        return true;
    }

    public async Task<List<ProfileModel>> SearchAccount(string firstName, string lastName, string email, int from, int size)
    {
        List<ProfileModel> profiles = new List<ProfileModel>();
        var models = await _accountDal.GetAccountByParamsAsync(firstName, lastName, email, from, size);
        foreach (var model in models)
        {
            profiles.Add(MapperAccountModel.MapQueryAccountModelToAccount(model));
        }
        return profiles;
    }

    public async Task<AccountModel> UpdateAccount(AccountModel model)
    {
        model.salt = Guid.NewGuid().ToString();
        model.password = _encrypt.HashPassword(model.password, model.salt);
        await _accountDal.UpdateAccountAsync(model);
        return model;
    }

    public async Task DeleteAccount(long id)
    {
        var model = await _accountDal.GetAccountByIdAsync(id);
        await _accountDal.DeleteAccountAsync(model);
    }
}