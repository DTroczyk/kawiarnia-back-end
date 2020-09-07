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
using System.Text.RegularExpressions;

namespace Api.Services.Services
{
    public class UserService : BaseService, IUserService
    {
        public UserService(ApplicationDbContext dbContext) : base(dbContext)
        {
            DotNetEnv.Env.Load();
        }

        public string Validation(UserVm user)
        {
            string message = String.Empty;

            user.username = user.username.Trim();
            // username
            Regex regex = new Regex(@"^(?!.*\.\.)(?!.*\.$)[^\W][\w.]{2,29}$");
            if (!regex.IsMatch(user.username))
            {
                message += "username, ";
            }

            user.password = user.password.Trim();
            // password
            regex = new Regex(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$");
            if (!regex.IsMatch(user.password))
            {
                message += "password, ";
            }

            user.firstName = user.firstName.Trim();
            // first name
            regex = new Regex(@"^([A-ZÀÁÂÄĄÇĆČĎÈÉÊËĘĚÍÎÏŁŃŇÑÓÔÖŘßŚŠŤÙÚÛÜŮÝŸŹŻŽa-zàáâäąçćčďèéêëęěíîïłńňñóôöřßśšťùúûüůýÿźżž]|[A-ZÀÁÂÄĄÇĆČĎÈÉÊËĘĚÍÎÏŁŃŇÑÓÔÖŘßŚŠŤÙÚÛÜŮÝŸŹŻŽa-zàáâäąçćčďèéêëęěíîïłńňñóôöřßśšťùúûüůýÿźżž](-| )[A-ZÀÁÂÄĄÇĆČĎÈÉÊËĘĚÍÎÏŁŃŇÑÓÔÖŘßŚŠŤÙÚÛÜŮÝŸŹŻŽa-zàáâäąçćčďèéêëęěíîïłńňñóôöřßśšťùúûüůýÿźżž]){1,}$");
            if (!regex.IsMatch(user.firstName))
            {
                message += "first name, ";
            }

            user.lastName = user.lastName.Trim();
            // last name
            if (!regex.IsMatch(user.lastName))
            {
                message += "last name, ";
            }

            user.place = user.place.Trim();
            // place
            if (!regex.IsMatch(user.place))
            {
                message += "place, ";
            }

            user.road = user.road.Trim();
            // road
            if (user.road != String.Empty || user.road != "")
            {
                if (!regex.IsMatch(user.road))
                {
                    message += "road, ";
                }
            }

            user.email = user.email.Trim();
            // email
            regex = new Regex(@"^\b[\w\.-]+@[\w\.-]+\.\w{2,4}\b$");
            if (!regex.IsMatch(user.email))
            {
                message += "email, ";
            }

            user.zipcode = user.zipcode.Trim();
            // zipcode
            regex = new Regex(@"^(([0-9]{2}-[0-9]{3})|[0-9]{5})$");
            if (!regex.IsMatch(user.zipcode))
            {
                message += "zipcode, ";
            }

            user.houseNumber = user.houseNumber.Trim();
            // house number
            regex = new Regex(@"^[0-9]{1,}[A-Z]{1}$|^[0-9]{1,}|^[0-9]{1,}[a-z]{1}$");
            if (!regex.IsMatch(user.houseNumber))
            {
                message += "house number, ";
            }

            user.telephone = user.telephone.Trim();
            // telephone
            regex = new Regex(@"^(([0-9]{9})|(\+{1}[0-9]{2,})|(([0-9]{3} ){2}[0-9]{3})|(\+{1}[0-9]{2,} ([0-9]{3} ){2}[0-9]{3}))$");
            if (!regex.IsMatch(user.telephone))
            {
                message += "telephone, ";
            }

            // date
            if (!DateValidation(user.dateOfBirth))
            {
                message += "date of birth, ";
            }

            if (message != String.Empty)
            {
                message = message.Remove(message.Length - 2);
            }

            return message;
        }

        public bool DateValidation(Date date)
        {
            bool isLeapYear = int.Parse(date.year) % 4 == 0 ? true : false;
            if (int.Parse(date.day) < 1)
            {
                return false;
            }
            if (int.Parse(date.month) > 12 || int.Parse(date.month) < 1)
            {
                return false;
            }
            if (int.Parse(date.day) > 28)
            {
                switch (int.Parse(date.month))
                {
                    case 1: case 3: case 5: case 7: case 8: case 10: case 12:
                        if (int.Parse(date.day) > 31)
                            return false;
                        break;
                    case 4: case 6: case 9: case 11:
                        if (int.Parse(date.day) > 30)
                            return false;
                        break;
                    case 2:
                        if (int.Parse(date.day) > 29 && isLeapYear)
                            return false;
                        if (int.Parse(date.day) > 28 && !isLeapYear)
                            return false;
                        break;
                }
            }
            return true;
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
            var message = Validation(userVm);
            if (message != String.Empty)
            {
                throw new Exception(message);
            }

            var user = Mapper.Map<User>(userVm);

            if (identity == null)
            {
                IsUserExist(user);

                user.RegistrationDate = DateTime.UtcNow.AddHours(2);
                user.IsVerifiedEmail = false;

                var userHash = PasswordHashService.HashPassword(userVm.password);

                user.PasswordHash = userHash.PasswordHash;
                user.Salt = userHash.Salt;

                _dbContext.Add(user);

                
            }
            else if (GetUserName(identity) == user.UserName)
            {
                User userDb = _dbContext.Users.FirstOrDefault(u => u.UserName == userVm.username);
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

                _dbContext.Entry(userDb).State = EntityState.Detached;
                _dbContext.Update(user);
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
