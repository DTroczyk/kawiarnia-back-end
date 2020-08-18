﻿using Api.BLL.Entity;
using Api.DAL.EF;
using Api.Services.Interfaces;
using Api.ViewModels.ViewModel;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Api.Services.Services
{
    public class UserService : BaseService, IUserService
    {
        public UserService(ApplicationDbContext dbContext) : base(dbContext)
        {

        }

        private bool Validation(User user)
        {
            // RegEx here

            return true;
        }

        public async Task<UserVm> AddOrUpdate(UserVm userVm, ClaimsIdentity identity = null)
        {
            var user = Mapper.Map<User>(userVm);

            if (identity == null)
            {
                bool isUserExist = false;
                bool isEmailExist = false;

                if (await _dbContext.Users.AnyAsync(u => u.UserName == user.UserName))
                {
                    isUserExist = true;
                }
                if (await _dbContext.Users.AnyAsync(u => u.Email == user.Email))
                {
                    isEmailExist = true;
                }
                if (isEmailExist && isUserExist)
                {
                    throw new Exception("Username and Email address already exist.");
                }
                if (isEmailExist)
                {
                    throw new Exception("Email address already exist.");
                }
                if (isUserExist)
                {
                    throw new Exception("Username already exist.");
                }

                user.RegistrationDate = DateTime.Now;
                user.IsVerifiedEmail = false;

                if (Validation(user))
                {
                    _dbContext.Add(user);
                }
                else
                {
                    throw new NotImplementedException();
                }
            }
            else if (GetUserName(identity) == user.UserName)
            {
                if (Validation(user))
                {
                    _dbContext.Update(user);
                }
                else
                {
                    throw new NotImplementedException();
                }
            }
            else if (GetUserName(identity) != user.UserName)
            {
                throw new Exception("User is invalid.");
            }

            await _dbContext.SaveChangesAsync();

            userVm = Mapper.Map<UserVm>(user);

            return userVm;
        }

        public async Task<UserVm> GetCurrentUser(ClaimsIdentity identity)
        {
            string userName = GetUserName(identity);
            User userEntity = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserName == userName);
            UserVm userVm = Mapper.Map<UserVm>(userEntity);

            return userVm;
        }

        public string GetUserName(ClaimsIdentity identity)
        {
            IList<Claim> claims = identity.Claims.ToList();
            string userName = claims[0].Value;
            return userName;
        }
    }
}
