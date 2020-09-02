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

        }

        private string Validation(User user)
        {
            string message = "Incorrect: ";

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
            regex = new Regex(@"^([A-z]|[ęóąśłżźćńĘÓĄŚŁŻŹĆŃ]){1,}$");
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
            regex = new Regex(@"^([A-z]|[ęóąśłżźćńĘÓĄŚŁŻŹĆŃ]){1,}$");
            if (!regex.IsMatch(user.City))
            {
                message += "place, ";
            }

            // road
            regex = new Regex(@"^([A-z]|[ęóąśłżźćńĘÓĄŚŁŻŹĆŃ]|[-. ,/'()]|[0-9]){1,}$");
            if (!regex.IsMatch(user.Street) && user.Street == "")
            {
                message += "road, ";
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

            message = message.Remove(message.Length - 2);
            message += ".";

            return message;
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

                user.RegistrationDate = DateTime.UtcNow.AddHours(2);
                user.IsVerifiedEmail = false;

                var userHash = PasswordHashService.HashPassword(userVm.password);

                var message = Validation(user);

                user.PasswordHash = userHash.PasswordHash;
                user.Salt = userHash.Salt;

                if (message == "Incorrect.")
                {
                    _dbContext.Add(user);
                    string text = @$"Cześć, {user.FirstName}

Cieszymy się że dołączyłeś/dołączyłaś do klientów naszej kawiarni. Mamy nadzieję, że posmakuje Ci nasza kawa.

Zapraszamy,
Super Kawiarnia XYZ";
                    await SendEmail(user, text, "Rejestracja w Super Kawiarnia XYZ");
                }
                else
                {
                    throw new Exception(message);
                }
            }
            else if (GetUserName(identity) == user.UserName)
            {
                User userDb = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserName == userVm.username);
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

                if (message == "Incorrect.")
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

        private async Task<bool> SendEmail(User user, string text, string subject)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Cafe", "kawiarnia2020@outlook.com"));
            message.To.Add(new MailboxAddress($"{user.FirstName} {user.LastName}", user.Email));
            message.Subject = subject;
            message.Body = new TextPart("plain")
            {
                Text = text
            };

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.office365.com", 587, MailKit.Security.SecureSocketOptions.StartTls);

                client.Authenticate("kawiarnia2020@outlook.com", "password");

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
