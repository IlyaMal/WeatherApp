using Microsoft.AspNetCore.Mvc;
using WeatherAPI.BLL.Account;

namespace WeatherAPI.Controllers;

[Route("/login/")]
[ApiController]
public class LoginController: ControllerBase
{
    private readonly IAccountBLL _accountBll;
    

    public LoginController(IAccountBLL accountBll, IAuth auth)
    {
        _accountBll = accountBll;

    }

    [HttpPost]
    public async Task<IActionResult> Index([FromForm]string email,[FromForm]string password)
    {
        //TODO
        long id = await _accountBll.Login(email, password);
        if (id == 0) return Unauthorized();
        var options = new CookieOptions()
        {
            Expires = DateTime.Now.AddDays(1),
            IsEssential = true
        };
        Response.Cookies.Append("userId", id.ToString(), options);
        return Ok(new {id = id});
    }
}