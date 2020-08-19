using Api.BLL.Entity;

namespace Api.Services.Interfaces
{
    public interface ILoginService
    {
        public User Login(string username, string pass);
        public User AuthenticateUser(User login);
        public string GenerateJSONWebToken(User userInfo);
    }
}
