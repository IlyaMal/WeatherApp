using System.Net;

namespace WeatherAPI.BLL.Account;

public interface IAuth
{
    bool IsUnauthorized(IRequestCookieCollection cookie);
    bool VerificateAccount(IRequestCookieCollection cookie, long id);
}