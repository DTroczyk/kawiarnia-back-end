using Api.BLL.Entity;

namespace Api.Services.Interfaces
{
    public interface ILoginService
    {
        User Login(string username, string pass);
        User AuthenticateUser(User login);
        string GenerateJSONWebToken(User userInfo);
    }
}
