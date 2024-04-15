namespace WeatherAPI.BLL.Account;

public class Auth: IAuth
{
    public bool IsUnauthorized(IRequestCookieCollection cookie)
    {
        if (cookie["userId"] == null) return true;
        return false;
    }
    public bool VerificateAccount(IRequestCookieCollection cookie, long id)
    {
        if (cookie["userId"] != id.ToString()) return true;
        return false;
    }
}