using Api.BLL.Entity;
using Api.Configuration;
using Api.DAL.EF;
using Api.IntegrationTests.TestAttributes;
using Api.Services.Services;
using Api.ViewModels.ViewModel;
using AutoMapper;
using NUnit.Framework;
using System;
using System.Linq;

namespace Api.IntegrationTests.ServiceTests
{
    public class UserServiceTests : BaseTest
    {
        public UserServiceTests() : base ()
        {
            
        }

        [Test, Isolated]
        public void AddOrUpdate_AddPassValidUser_ShouldAddUserToDatabase()
        {
            var context = new ApplicationDbContext(_testcs);
            var user = new User
            {
                UserName = "Joe",
                Email = "Joe@example.exam",
                RegistrationDate = DateTime.Now,
                PasswordHash = "Pa55word",
                IsVerifiedEmail = false,
                FirstName = "Joe",
                LastName = "Doe",
                PostalCode = "00-000",
                City = "Unknown",
                Street = "Unknown",
                HouseNumber = "30A",
                PhoneNumber = "485212352",
                DateOfBirth = DateTime.Now.AddYears(-20)
            };
            var userService = new UserService(context);
            var userVm = Mapper.Map<UserVm>(user);

            userService.AddOrUpdate(userVm);

            var newUser = context.Users.FirstOrDefault(u => u.UserName == "Joe");

            Assert.IsNotNull(newUser);
        }

        [Test, Isolated]
        public void AddOrUpdate_AddExistedUser_ShouldThrowException()
        {
            var context = new ApplicationDbContext(_testcs);
            var user = new User
            {
                UserName = "Joe",
                Email = "Joe@example.exam",
                RegistrationDate = DateTime.Now,
                PasswordHash = "Pa55word",
                IsVerifiedEmail = false,
                FirstName = "Joe",
                LastName = "Doe",
                PostalCode = "00-000",
                City = "Unknown",
                Street = "Unknown",
                HouseNumber = "30A",
                PhoneNumber = "485212352",
                DateOfBirth = DateTime.Now.AddYears(-20)
            };
            var userService = new UserService(context);
            var userVm = Mapper.Map<UserVm>(user);
            Exception exception = null;

            try
            {
                userService.AddOrUpdate(userVm);
                userService.AddOrUpdate(userVm);
            }
            catch (Exception e)
            {
                if (e.Message == "Username and Email address already exist.")
                {
                    exception = e;
                }
            }

            var newUser = context.Users.FirstOrDefault(u => u.UserName == "Joe");
            var countUsers = context.Users.Count(u => u.UserName == "Joe");

            Assert.IsNotNull(newUser);
            Assert.IsNotNull(exception);
            Assert.That(countUsers, Is.EqualTo(1));
        }

        [Test, Isolated]
        public void AddOrUpdate_AddInvalidInputData_ShouldThrowException()
        {
            var context = new ApplicationDbContext(_testcs);
            var user = new User
            {
                UserName = "Joe",
                Email = "Joe@example.exam",
                RegistrationDate = DateTime.Now,
                PasswordHash = "pass",
                IsVerifiedEmail = false,
                FirstName = "Joe",
                LastName = "Doe",
                PostalCode = "00-000!",
                City = "Unknown",
                Street = "Unknown",
                HouseNumber = "30A",
                PhoneNumber = "485212352",
                DateOfBirth = DateTime.Now.AddYears(-20)
            };
            var userService = new UserService(context);
            var userVm = Mapper.Map<UserVm>(user);
            Exception exception = null;

            try
            {
                userService.AddOrUpdate(userVm);
            }
            catch (Exception e)
            {
                exception = e;
            }

            var countUsers = context.Users.Count(u => u.UserName == "Joe");

            Assert.That(exception.Message, Is.EqualTo("password, zipcode"));
            Assert.That(countUsers, Is.EqualTo(0));
        }

        [Test, Isolated]
        public void Delete_CorrectUserName_ShouldRemovedUserFromDb()
        {
            var context = new ApplicationDbContext(_testcs);
            var user = new User
            {
                UserName = "Joe",
                Email = "Joe@example.exam",
                RegistrationDate = DateTime.Now,
                PasswordHash = "Pa55word",
                IsVerifiedEmail = false,
                FirstName = "Joe",
                LastName = "Doe",
                PostalCode = "00-000",
                City = "Unknown",
                Street = "Unknown",
                HouseNumber = "30A",
                PhoneNumber = "485212352",
                DateOfBirth = DateTime.Now.AddYears(-20)
            };
            var userService = new UserService(context);
            var userVm = Mapper.Map<UserVm>(user);

            userService.AddOrUpdate(userVm);
            var countUsers = context.Users.Count(u => u.UserName == "Joe");
            Assert.That(countUsers, Is.EqualTo(1));

            userService.Delete("Joe");
            countUsers = context.Users.Count(u => u.UserName == "Joe");
            Assert.That(countUsers, Is.EqualTo(0));
        }

        [Test, Isolated]
        public void Delete_IncorrectUserName_ShouldThrowException()
        {
            var context = new ApplicationDbContext(_testcs);
            var user = new User
            {
                UserName = "Joe",
                Email = "Joe@example.exam",
                RegistrationDate = DateTime.Now,
                PasswordHash = "Pa55word",
                IsVerifiedEmail = false,
                FirstName = "Joe",
                LastName = "Doe",
                PostalCode = "00-000",
                City = "Unknown",
                Street = "Unknown",
                HouseNumber = "30A",
                PhoneNumber = "485212352",
                DateOfBirth = DateTime.Now.AddYears(-20)
            };
            var userService = new UserService(context);
            var userVm = Mapper.Map<UserVm>(user);
            Exception exception = null;
            var countUsers = 0;

            userService.AddOrUpdate(userVm);
            countUsers = context.Users.Count(u => u.UserName == "Joe");
            Assert.That(countUsers, Is.EqualTo(1));

            try
            {
                userService.Delete("Joel");
            }
            catch (Exception e)
            {
                exception = e;
            }
            countUsers = context.Users.Count(u => u.UserName == "Joe");

            Assert.That(exception.Message, Is.EqualTo("User not found."));
            Assert.That(countUsers, Is.EqualTo(1));
        }

        [Test, Isolated]
        public void IsUserExist_CorrectData_ShouldReturnNothing()
        {
            var context = new ApplicationDbContext(_testcs);
            var user = new User
            {
                UserName = "Test",
                Email = "Test@gmail.com",
                RegistrationDate = DateTime.Now,
                PasswordHash = "Pa55word",
                IsVerifiedEmail = false,
                FirstName = "Test",
                LastName = "Test",
                PostalCode = "21-186",
                City = "Test",
                Street = "Test",
                HouseNumber = "30A",
                PhoneNumber = "485212352",
                DateOfBirth = DateTime.Now.AddYears(-20)
            };
            var userService = new UserService(context);
            Exception exception = null;
            var countUsers = 0;

            countUsers = context.Users.Count(u => u.UserName == "Test");
            Assert.That(countUsers, Is.EqualTo(1));

            user.Email = "Joe@example.exa";
            user.UserName = "Joel";
            try
            {
                userService.IsUserExist(user);
            }
            catch (Exception e)
            {
                exception = e;
            }

            Assert.IsNull(exception);
        }

        [Test, Isolated]
        public void IsUserExist_ExistingUserWithEmailAddress_ShouldThrowException()
        {
            var context = new ApplicationDbContext(_testcs);
            var user = new User
            {
                UserName = "Test",
                Email = "Test@gmail.com",
                RegistrationDate = DateTime.Now,
                PasswordHash = "Pa55word",
                IsVerifiedEmail = false,
                FirstName = "Test",
                LastName = "Test",
                PostalCode = "21-186",
                City = "Test",
                Street = "Test",
                HouseNumber = "30A",
                PhoneNumber = "485212352",
                DateOfBirth = DateTime.Now.AddYears(-20)
            };
            var userService = new UserService(context);
            Exception exception = null;
            var countUsers = 0;

            countUsers = context.Users.Count(u => u.UserName == "Test");
            Assert.That(countUsers, Is.EqualTo(1));

            user.UserName = "Joel";
            try
            {
                userService.IsUserExist(user);
            }
            catch (Exception e)
            {
                exception = e;
            }

            Assert.That(exception.Message, Is.EqualTo("Email address already exist."));
        }

        [Test, Isolated]
        public void IsUserExist_ExistingUserWithUsername_ShouldThrowException()
        {
            var context = new ApplicationDbContext(_testcs);
            var user = new User
            {
                UserName = "Test",
                Email = "Test@gmail.com",
                RegistrationDate = DateTime.Now,
                PasswordHash = "Pa55word",
                IsVerifiedEmail = false,
                FirstName = "Test",
                LastName = "Test",
                PostalCode = "21-186",
                City = "Test",
                Street = "Test",
                HouseNumber = "30A",
                PhoneNumber = "485212352",
                DateOfBirth = DateTime.Now.AddYears(-20)
            };
            var userService = new UserService(context);
            Exception exception = null;
            var countUsers = 0;

            countUsers = context.Users.Count(u => u.UserName == "Test");
            Assert.That(countUsers, Is.EqualTo(1));

            user.Email = "Joe@example.exa";
            try
            {
                userService.IsUserExist(user);
            }
            catch (Exception e)
            {
                exception = e;
            }

            Assert.That(exception.Message, Is.EqualTo("Username already exist."));
        }

        [Test, Isolated]
        public void IsUserExist_ExistingUserWithUsernameAndEmailAddress_ShouldThrowException()
        {
            var context = new ApplicationDbContext(_testcs);
            var user = new User
            {
                UserName = "Test",
                Email = "Test@gmail.com",
                RegistrationDate = DateTime.Now,
                PasswordHash = "Pa55word",
                IsVerifiedEmail = false,
                FirstName = "Test",
                LastName = "Test",
                PostalCode = "21-186",
                City = "Test",
                Street = "Test",
                HouseNumber = "30A",
                PhoneNumber = "485212352",
                DateOfBirth = DateTime.Now.AddYears(-20)
            };
            var userService = new UserService(context);
            Exception exception = null;
            var countUsers = 0;

            countUsers = context.Users.Count(u => u.UserName == "Test");
            Assert.That(countUsers, Is.EqualTo(1));
            
            try
            {
                userService.IsUserExist(user);
            }
            catch (Exception e)
            {
                exception = e;
            }

            Assert.That(exception.Message, Is.EqualTo("Username and Email address already exist."));
        }
    }
}
