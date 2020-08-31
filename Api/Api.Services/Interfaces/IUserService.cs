using Api.ViewModels.ViewModel;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Api.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserVm> AddOrUpdate(UserVm userVm, ClaimsIdentity identity = null);
        string GetUserName(ClaimsIdentity identity);
        Task<UserVm> GetCurrentUser(ClaimsIdentity identity);
        Task<UserVm> Delete(string username);
        Task<bool> ForgottenPassword(string email);
    }
}
