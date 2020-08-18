using Api.ViewModels.ViewModel;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Api.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserVm> AddOrUpdate(UserVm userVm, ClaimsIdentity identity = null);
        string GetUserName(ClaimsIdentity identity);
        Task<UserVm> GetCurrentUser(ClaimsIdentity identity);
    }
}
