using System.Xml;
using Microsoft.AspNetCore.Mvc;
using WeatherAPI.BLL.Account;
using WeatherAPI.BLL.Region;
using WeatherAPI.DAL.Models;

namespace WeatherAPI.Controllers;
[Route("/accounts/")]
[ApiController]
public class AccountController: ControllerBase
{
    private readonly IAccountBLL _accountBll;
    private readonly IAuth _auth;

    public AccountController(IAccountBLL accountBll, IAuth auth)
    {
        _accountBll = accountBll;
        _auth = auth;
    }
    
    
    [HttpGet ("/{accountId?}")]
    public async Task<IActionResult> Index(long? accountId)
    {
        if (!accountId.HasValue || accountId <= 0) return BadRequest();
        if (_auth.IsUnauthorized(Request.Cookies)) return Unauthorized();
        var model = await _accountBll.GetAccount(accountId.Value);
        var account = new
        {
            id = model.id,
            firstName = model.firstName,
            lastName = model.lastName,
            email = model.email
            

        };
        if (account.id == 0) return NotFound();
        return Ok(account);
    }

    [HttpGet("search")]
    public async Task<IActionResult> IndexSearch(string firstName, string lastName, string email, int form, int size)
    {
        if (form < 0 || size <= 0) return BadRequest();
        if (_auth.IsUnauthorized(Request.Cookies)) return Unauthorized();
        var model = await _accountBll.SearchAccount(firstName, lastName, email, form, size);
        return Ok(model);
    }
    
    [HttpPut("{accountId?}")]
    public async Task<IActionResult> IndexUpdate(long? accountId, [FromBody] AccountModel model)
    {
        if (_auth.IsUnauthorized(Request.Cookies)) return Unauthorized();
        if (!accountId.HasValue || accountId <= 0)
        {
            return BadRequest();
        }
        if (model.firstName.Equals(null) || model.firstName.All(char.IsWhiteSpace))
        {
            return BadRequest();
        }
        if (model.lastName.Equals(null) || model.lastName.All(char.IsWhiteSpace))
        {
            return BadRequest();
        }
        if (model.email.Equals(null) || model.email.All(char.IsWhiteSpace))
        {
            return BadRequest();
        }
        if (model.password.Equals(null) || model.password.All(char.IsWhiteSpace))
        {
            return BadRequest();
        }

        var existedModel = await _accountBll.GetAccount(accountId.Value);
        if(_auth.VerificateAccount(Request.Cookies, accountId.Value) || existedModel.id == 0 ) return new ContentResult
        {
            StatusCode = 403,
            ContentType = "application/json"
        };
        bool checkExistedEmail = await _accountBll.GetAccountByEmail(model.email);
        if (checkExistedEmail) return Conflict();
        model.id = accountId.Value;
        await _accountBll.UpdateAccount(model);
        var account = new
        {
            id = model.id,
            firstName = model.firstName,
            lastName = model.lastName,
            email = model.email
        };
        return Ok(account);
    }

    [HttpDelete("{accountId?}")]
    public async Task<IActionResult> IndexDelete(long? accountId)
    {
        if (!accountId.HasValue || accountId <= 0) return BadRequest();
        if (_auth.IsUnauthorized(Request.Cookies)) return Unauthorized();
        var existedModel = await _accountBll.GetAccount(accountId.Value);
        if(_auth.VerificateAccount(Request.Cookies, accountId.Value) || existedModel.id == 0 ) return new ContentResult
        {
            StatusCode = 403,
            ContentType = "application/json"
        };
        await _accountBll.DeleteAccount(accountId.Value);
        return Ok();
    }
}