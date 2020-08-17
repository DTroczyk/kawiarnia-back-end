using Api.DAL.EF;
using Api.Services.Interfaces;
using Api.ViewModels.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Api.Services.Services
{
    public class UserService : BaseService, IUserService
    {
        public UserService(ApplicationDbContext dbContext) : base(dbContext)
        {

        }

        public UserVm AddOrUpdate(UserVm addOrUpdateDto)
        {
            throw new NotImplementedException();
        }

        public string GetUserName()
        {
            throw new NotImplementedException();
        }
    }
}
