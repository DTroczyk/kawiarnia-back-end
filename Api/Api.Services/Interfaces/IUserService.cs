using Api.BLL.Entity;
using Api.ViewModels.ViewModel;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Api.Services.Interfaces
{
    public interface IUserService
    {
        UserVm AddOrUpdate(UserVm userVm, ClaimsIdentity identity = null);
        string GetUserName(ClaimsIdentity identity);
        Task<UserVm> GetCurrentUser(ClaimsIdentity identity);
        UserVm Delete(string username);
        Task<bool> ForgottenPassword(string email);
        public Task<bool> SendEmail(User user, string text, string subject);
        public Task<bool> SendEmail(UserVm user, string text, string subject);
    }
}
