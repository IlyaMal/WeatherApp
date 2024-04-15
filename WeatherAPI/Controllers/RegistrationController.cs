using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WeatherAPI.BLL.Account;
using WeatherAPI.DAL.Models;
using WeatherAPI.Mappers;
using WeatherAPI.QueryModel;

namespace WeatherAPI.Controllers;

[Route("/registration/")]
[ApiController]
public class RegistrationController: ControllerBase
{
    private readonly IAccountBLL _accountBll;

    public RegistrationController(IAccountBLL accountBll)
    {
        _accountBll = accountBll;
    }
    
    [HttpPost]
    public async Task<IActionResult> Index([FromBody] AccountModel model)
    {
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
        bool checkExistedEmail = await _accountBll.GetAccountByEmail(model.email);
        if (checkExistedEmail) return Conflict();
        await _accountBll.Register(model);
        
        var account = new
        {
            id = model.id,
            firstName = model.firstName,
            lastName = model.lastName,
            email = model.email
            

        };
        return new ContentResult
        {
            StatusCode = 201,
            Content = JsonConvert.SerializeObject(account),
            ContentType = "application/json"
        };
    }

}