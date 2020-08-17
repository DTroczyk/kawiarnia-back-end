using Api.ViewModels.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Api.Services.Interfaces
{
    public interface IUserService
    {
        UserVm AddOrUpdate(UserVm addOrUpdateDto);
        string GetUserName();
    }
}
