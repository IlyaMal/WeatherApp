using Microsoft.EntityFrameworkCore;
using WeatherAPI.DAL.Models;

namespace WeatherAPI.DAL.Account;

public class AccountDAL: IAccountDAL
{
    public async Task<AccountModel> CreateAccountAsync(AccountModel model)
    {
        using (var connection = new DBHelper())
        {
            await connection.Accounts.AddAsync(model);
            await connection.SaveChangesAsync();
            return model;
        }
    }

    public async Task<AccountModel> GetAccountByIdAsync(long id)
    {
        using (var connection = new DBHelper())
        {
            return await connection.Accounts.FindAsync(id) ?? new AccountModel();
        }
    }

    public async Task<AccountModel> GetAccountByEmailAsync(string email)
    {
        using (var connection = new DBHelper())
        {
            return await connection.Accounts.FirstOrDefaultAsync(a => a.email == email) ?? new AccountModel();
        }
    }

    public async Task<List<AccountModel>> GetAccountByParamsAsync(string firstName, string lastName, string email, int from, int size)
    {
        await using var context = new DBHelper();
        IQueryable<AccountModel> query = context.Accounts;
        
        if (!string.IsNullOrEmpty(firstName))
            query = query.Where(u => u.firstName.ToLower().Contains(firstName.ToLower()));
        if (!string.IsNullOrEmpty(lastName))
            query = query.Where(u => u.lastName.ToLower().Contains(lastName.ToLower()));
        if (!string.IsNullOrEmpty(email))
            query = query.Where(u => u.email.ToLower().Contains(email.ToLower()));
        query = query.Skip(from).Take(size);
        var models = await query.ToListAsync();

        return models;
    }

    public async Task UpdateAccountAsync(AccountModel model)
    {
        using (var connection = new DBHelper())
        {
            connection.Accounts.Update(model);
            await connection.SaveChangesAsync();
        }
    }

    public async Task DeleteAccountAsync(AccountModel model)
    {
        using (var connection = new DBHelper())
        {
            connection.Accounts.Remove(model);
            await connection.SaveChangesAsync();
        }
    }
}