using Api.BLL.Entity;
using Api.ViewModels.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Api.Services.Interfaces
{
    public interface ILoginService
    {
        public User Login(string username, string pass);
        public User AuthenticateUser(User login);
        public string GenerateJSONWebToken(User userInfo);
    }
}
