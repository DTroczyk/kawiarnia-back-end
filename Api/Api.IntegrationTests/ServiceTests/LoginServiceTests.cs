using Api.BLL.Entity;
using Api.DAL.EF;
using Api.IntegrationTests.TestAttributes;
using Api.Services.Services;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace Api.IntegrationTests.ServiceTests
{
    public class LoginServiceTests : BaseTest
    {
        private IConfiguration _config;

        public LoginServiceTests() : base()
        {
        }

        [Test, Isolated]
        public void AuthenticateUser_CorrectData_ShouldReturnUser()
        {
            var context = new ApplicationDbContext(_testcs);
            var service = new LoginService(context, _config);
            var user = new User()
            {
                UserName = "Test",
                PasswordHash = "FtLaz60bkEEw8NRm2eeZODjU8Do=",
            };

            var result = service.AuthenticateUser(user);

            Assert.That(result, Is.TypeOf<User>());
        }

        [Test, Isolated]
        public void AuthenticateUser_CorrectPassword_ShouldReturnUser()
        {
            var context = new ApplicationDbContext(_testcs);
            var service = new LoginService(context, _config);
            var user = new User()
            {
                UserName = "Test",
                PasswordHash = "haslo",
            };

            var result = service.AuthenticateUser(user);

            Assert.That(result, Is.TypeOf<User>());
        }

        [Test, Isolated]
        public void AuthenticateUser_WrongPassword_ShouldReturnNull()
        {
            var context = new ApplicationDbContext(_testcs);
            var service = new LoginService(context, _config);
            var user = new User()
            {
                UserName = "Test",
                PasswordHash = "Password",
            };

            var result = service.AuthenticateUser(user);

            Assert.That(result, Is.Null);
        }

        [Test, Isolated]
        public void Login_CorrectPasswordHash_ShouldReturnUser()
        {
            var context = new ApplicationDbContext(_testcs);
            var service = new LoginService(context, _config);

            var result = service.Login("Test", "FtLaz60bkEEw8NRm2eeZODjU8Do=");

            Assert.That(result, Is.TypeOf<User>());
        }

        [Test, Isolated]
        public void Login_CorrectPassword_ShouldReturnUser()
        {
            var context = new ApplicationDbContext(_testcs);
            var service = new LoginService(context, _config);

            var result = service.Login("Test", "haslo");

            Assert.That(result, Is.TypeOf<User>());
        }

        [Test, Isolated]
        public void Login_WrongPassword_ShouldReturnNull()
        {
            var context = new ApplicationDbContext(_testcs);
            var service = new LoginService(context, _config);
            
            var result = service.Login("Test", "Password");

            Assert.That(result, Is.Null);
        }

        [Test, Isolated]
        public void Login_WrongUsername_ShouldReturnNull()
        {
            var context = new ApplicationDbContext(_testcs);
            var service = new LoginService(context, _config);

            var result = service.Login("Te", "haslo");

            Assert.That(result, Is.Null);
        }
    }
}
