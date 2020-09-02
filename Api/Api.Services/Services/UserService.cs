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
using System.Text.RegularExpressions;

namespace Api.Services.Services
{
    public class UserService : BaseService, IUserService
    {
        public UserService(ApplicationDbContext dbContext) : base(dbContext)
        {
            DotNetEnv.Env.Load();
        }

        public string Validation(User user)
        {
            string message = String.Empty;

            // username
            Regex regex = new Regex(@"^(?!.*\.\.)(?!.*\.$)[^\W][\w.]{2,29}$");
            if (!regex.IsMatch(user.UserName))
            {
                message += "username, ";
            }
            

            // password
            regex = new Regex(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$");
            if (!regex.IsMatch(user.PasswordHash))
            {
                message += "password, ";
            }

            // first name
            regex = new Regex(@"^([A-Z]|[a-z]|[ęóąśłżźćńĘÓĄŚŁŻŹĆŃ]){1,}$");
            if (!regex.IsMatch(user.FirstName))
            {
                message += "first name, ";
            }

            // last name
            if (!regex.IsMatch(user.LastName))
            {
                message += "last name, ";
            }

            // email
            regex = new Regex(@"^\b[\w\.-]+@[\w\.-]+\.\w{2,4}\b$");
            if (!regex.IsMatch(user.Email))
            {
                message += "email, ";
            }

            // zipcode
            regex = new Regex(@"^[0-9]{2}-[0-9]{3}$");
            if (!regex.IsMatch(user.PostalCode))
            {
                message += "zipcode, ";
            }

            // place
            regex = new Regex(@"^(([A-Z]|[a-z]|[ęóąśłżźćńĘÓĄŚŁŻŹĆŃ])|([A-Z]|[a-z]|[ęóąśłżźćńĘÓĄŚŁŻŹĆŃ]){1,} ([A-Z]|[a-z]|[ęóąśłżźćńĘÓĄŚŁŻŹĆŃ])){1,}$");
            if (!regex.IsMatch(user.City))
            {
                message += "place, ";
            }

            // road
            if (user.Street != String.Empty || user.Street != "")
            {
                regex = new Regex(@"^([A-z]|[ęóąśłżźćńĘÓĄŚŁŻŹĆŃ]|[-. ,/'()]|[0-9]){1,}$");
                if (!regex.IsMatch(user.Street))
                {
                    message += "road, ";
                }
            }

            // house number
            regex = new Regex(@"^[0-9]{1,}[A-Z]{1}$|^[0-9]{1,}|^[0-9]{1,}[a-z]{1}$");
            if (!regex.IsMatch(user.HouseNumber))
            {
                message += "house number, ";
            }

            // telephone
            regex = new Regex(@"^(([0-9]{9})|(\+{1}[0-9]{2,})|(([0-9]{3} ){2}[0-9]{3})|(\+{1}[0-9]{2,} ([0-9]{3} ){2}[0-9]{3}))$");
            if (!regex.IsMatch(user.PhoneNumber))
            {
                message += "telephone, ";
            }

            // date of birth

            if (message != String.Empty)
            {
                message = message.Remove(message.Length - 2);
            }

            return message;
        }

        public void IsUserExist(User user)
        {
            bool isUserExist = false;
            bool isEmailExist = false;
            if (_dbContext.Users.Any(u => u.UserName == user.UserName))
            {
                isUserExist = true;
            }
            if (_dbContext.Users.Any(u => u.Email == user.Email))
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
        }

        public UserVm AddOrUpdate(UserVm userVm, ClaimsIdentity identity = null)
        {
            var user = Mapper.Map<User>(userVm);

            if (identity == null)
            {
                IsUserExist(user);

                user.RegistrationDate = DateTime.UtcNow.AddHours(2);
                user.IsVerifiedEmail = false;

                var userHash = PasswordHashService.HashPassword(userVm.password);

                var message = Validation(user);

                user.PasswordHash = userHash.PasswordHash;
                user.Salt = userHash.Salt;

                if (message == String.Empty)
                {
                    _dbContext.Add(user);
                }
                else
                {
                    throw new Exception(message);
                }
            }
            else if (GetUserName(identity) == user.UserName)
            {
                User userDb = _dbContext.Users.FirstOrDefault(u => u.UserName == userVm.username);
                user.RegistrationDate = userDb.RegistrationDate;

                var message = Validation(user);

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

                if (message == String.Empty)
                {
                    _dbContext.Entry(userDb).State = EntityState.Detached;
                    _dbContext.Update(user);
                }
                else
                {
                    throw new Exception(message);
                }
            }
            else if (GetUserName(identity) != user.UserName)
            {
                throw new Exception("User is invalid.");
            }

            _dbContext.SaveChanges();
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

        public UserVm Delete(string username)
        {
            var userEntity = _dbContext.Users.Find(username);

            if (userEntity == null)
            {
                throw new Exception("User not found.");
            }

            var userVm = Mapper.Map<UserVm>(userEntity);

            _dbContext.Users.Remove(userEntity);
            _dbContext.SaveChanges();

            return userVm;
        }

        public async Task<bool> SendEmail(User user, string text, string subject)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Kawiarnia", System.Environment.GetEnvironmentVariable("EMAIL")));
            message.To.Add(new MailboxAddress($"{user.FirstName} {user.LastName}", user.Email));
            message.Subject = subject;
            message.Body = new TextPart("plain")
            {
                Text = text
            };

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.office365.com", 587, MailKit.Security.SecureSocketOptions.StartTls);

                client.Authenticate(System.Environment.GetEnvironmentVariable("EMAIL"), System.Environment.GetEnvironmentVariable("EMAIL_PASSWORD"));

                await client.SendAsync(message);
                client.Disconnect(true);
            }

            return true;
        }
        public async Task<bool> SendEmail(UserVm user, string text, string subject)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Kawiarnia", System.Environment.GetEnvironmentVariable("EMAIL")));
            message.To.Add(new MailboxAddress($"{user.firstName} {user.lastName}", user.email));
            message.Subject = subject;
            message.Body = new TextPart("plain")
            {
                Text = text
            };

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.office365.com", 587, MailKit.Security.SecureSocketOptions.StartTls);

                client.Authenticate(System.Environment.GetEnvironmentVariable("EMAIL"), System.Environment.GetEnvironmentVariable("EMAIL_PASSWORD"));

                await client.SendAsync(message);
                client.Disconnect(true);
            }

            return true;
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

            Random random = new Random();
            string newPassword = String.Empty;
            for (int i = 0; i < 16; i++)
            {
                newPassword += (char)(random.Next()%43+48);
            }

            User userHash = PasswordHashService.HashPassword(newPassword);

            user.PasswordHash = userHash.PasswordHash;
            user.Salt = userHash.Salt;

            var text = @$"Cześć, {user.FirstName}. 
                    
Oto Twoje nowe hasło: {newPassword}

Pozdrawiamy
Super Kawiarnia XYZ";
            if (await SendEmail(user, text, "Odzyskiwanie hasła Super Kawiarnia XYZ") == false)
            {
                throw new Exception("Email wasn't send. Try again.");
            }

            _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync();

            
            return true;
        }
    }
}
