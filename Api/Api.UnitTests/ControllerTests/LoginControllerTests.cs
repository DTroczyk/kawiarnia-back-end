using Api.BLL.Entity;
using Api.Configuration;
using Api.Controllers;
using Api.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace Api.UnitTests.ControllerTests
{
    public class LoginControllerTests
    {
        public LoginControllerTests()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
            });
        }

        [Test]
        public void Login_WhenIncorrectData_ShouldReturnUnautorizeStatus()
        {
            var mockLoginService = new Mock<ILoginService>();
            mockLoginService.Setup(x => x.Login("user", "password")).Returns((User)null);
            var loginController = new LoginController(mockLoginService.Object);

            var result = loginController.Login("user", "password");
            var unauthorizedResult = result as UnauthorizedObjectResult;

            Assert.IsNotNull(unauthorizedResult);
            Assert.AreEqual(401, unauthorizedResult.StatusCode);
        }
        [Test]
        public void Login_WhenCorrectData_ShouldReturnOkStatus()
        {
            var mockLoginService = new Mock<ILoginService>();
            var user = new User { UserName = "user", PasswordHash = "password" };
            mockLoginService.Setup(x => x.Login("user", "password")).Returns(user);
            var loginController = new LoginController(mockLoginService.Object);

            var result = loginController.Login("user", "password");
            var okResult = result as OkObjectResult;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }
    }
}
