using Api.Controllers;
using Api.Services.Interfaces;
using Api.ViewModels.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Api.UnitTests.ControllerTests
{
    public class SignInControllerTests
    {
        [Test]
        public async Task RegisterUser_WhenCorrectData_ShouldReturnOkStatus()
        {
            var mockUserService = new Mock<IUserService>();
            UserVm userVm = new UserVm
            {
                username = "John",
                firstName = "John",
                lastName = "Doe",
                email = "JohnDoe@example.com"
            };
            mockUserService.Setup(x => x.AddOrUpdate(userVm, null)).Returns(userVm);
            var signInController = new SignInController(mockUserService.Object);

            ActionResult<UserVm> result = await signInController.RegisterUser(userVm);
            var createdResult = result.Result as ObjectResult;

            Assert.IsNotNull(createdResult);
            Assert.AreEqual(201, createdResult.StatusCode);
        }
    }
}
