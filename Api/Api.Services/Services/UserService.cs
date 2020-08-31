using Api.BLL.Entity;
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
using MimeKit;
using MailKit.Net.Smtp;
using System.Security.Cryptography;

namespace Api.Services.Services
{
    public class UserService : BaseService, IUserService
    {
        public UserService(ApplicationDbContext dbContext) : base(dbContext)
        {

        }

        private bool Validation(User user)
        {
            // Validation here

            return true;
        }

        public async Task<UserVm> AddOrUpdate(UserVm userVm, ClaimsIdentity identity = null)
        {
            var user = Mapper.Map<User>(userVm);;

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

                var userHash = PasswordHashService.HashPassword(userVm.password);

                user.PasswordHash = userHash.PasswordHash;
                user.Salt = userHash.Salt;

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
                User userDb = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserName == userVm.username);
                user.RegistrationDate = userDb.RegistrationDate;
                if (PasswordHashService.ValidatePassword(userVm.password, userDb) || userVm.password == userDb.PasswordHash)
                {
                    user.PasswordHash = userDb.PasswordHash;
                    user.Salt = userDb.Salt;
                }
                else
                {
                    var userHash = PasswordHashService.HashPassword(userVm.password);

                    user.PasswordHash = userHash.PasswordHash;
                    user.Salt = userHash.Salt;
                }
                if (Validation(user))
                {
                    _dbContext.Entry(userDb).State = EntityState.Detached;
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

        public async Task<UserVm> Delete(string username)
        {
            var userEntity = await _dbContext.Users.FindAsync(username);

            if (userEntity == null)
            {
                throw new Exception("User not found.");
            }

            var userVm = Mapper.Map<UserVm>(userEntity);

            _dbContext.Users.Remove(userEntity);
            await _dbContext.SaveChangesAsync();

            return userVm;
        }

        public async Task<bool> ForgottenPassword(string email)
        {
            var user = await _dbContext.Users
                .Where(u => u.Email.ToUpper() == email.ToUpper())
                .FirstOrDefaultAsync();
            if (user == null)
            {
                throw new Exception("Email address does not exist.");
            }

            var cryptoProvider = new RNGCryptoServiceProvider();
            byte[] password = new byte[10];
            cryptoProvider.GetBytes(password);
            string newPassword = Convert.ToBase64String(password);

            User userHash = PasswordHashService.HashPassword(newPassword);

            user.PasswordHash = userHash.PasswordHash;
            user.Salt = userHash.Salt;

            _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync();

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Cafe", "kawiarnia2020@outlook.com"));
            message.To.Add(new MailboxAddress($"{user.FirstName} {user.LastName}", user.Email));
            message.Subject = "Cafe password recovery";
            message.Body = new TextPart("plain")
            {
                Text = @$"Hello, {user.FirstName}. 
                    
This is your new password: {newPassword}
Regards Cafe"
            };

            using (var client = new SmtpClient ())
            {
                client.Connect("smtp.office365.com", 587, MailKit.Security.SecureSocketOptions.StartTls);

                client.Authenticate("kawiarnia2020@outlook.com", "password");

                await client.SendAsync(message);
                client.Disconnect(true);
            }
            return true;
        }
    }
}
