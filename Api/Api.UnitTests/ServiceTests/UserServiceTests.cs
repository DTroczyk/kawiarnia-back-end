using Api.BLL.Entity;
using Api.DAL.EF;
using Api.Services.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Api.UnitTests.ServiceTests
{
    public class UserServiceTests
    {
        private readonly ApplicationDbContext _dbContext;

        public UserServiceTests()
        {
            ConnectionStringDto testcs = new ConnectionStringDto { 
                ConnectionString = "Server=BASTION-1603\\UCZELNIA;Initial Catalog=Kawiarnia;User ID=Coffe;Password=coffe;Connection Timeout=30;"
            };
            _dbContext = new ApplicationDbContext(testcs);
        }

        // Correct data

        [Test]
        public void Validation_CorrectData_ShouldReturnEmptyString()
        {
            var userservice = new UserService(_dbContext);
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

            var result = userservice.Validation(user);

            Assert.That(result, Is.EqualTo(String.Empty));
        }

        [Test]
        public void Validation_EmptyStreet_ShouldReturnEmptyString()
        {
            var userservice = new UserService(_dbContext);
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
                Street = "",
                HouseNumber = "30A",
                PhoneNumber = "485212352",
                DateOfBirth = DateTime.Now.AddYears(-20)
            };

            var result = userservice.Validation(user);

            Assert.That(result, Is.EqualTo(String.Empty));
        }

        [Test]
        public void Validation_StreetWithSpace_ShouldReturnEmptyString()
        {
            var userservice = new UserService(_dbContext);
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
                Street = "Groove Street",
                HouseNumber = "30A",
                PhoneNumber = "485212352",
                DateOfBirth = DateTime.Now.AddYears(-20)
            };

            var result = userservice.Validation(user);

            Assert.That(result, Is.EqualTo(String.Empty));
        }

        [Test]
        public void Validation_CityWithSpace_ShouldReturnEmptyString()
        {
            var userservice = new UserService(_dbContext);
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
                City = "New York",
                Street = "Unknown",
                HouseNumber = "30A",
                PhoneNumber = "485212352",
                DateOfBirth = DateTime.Now.AddYears(-20)
            };

            var result = userservice.Validation(user);

            Assert.That(result, Is.EqualTo(String.Empty));
        }

        [Test]
        public void Validation_PhoneNumberWithAreaCode_ShouldReturnEmptyString()
        {
            var userservice = new UserService(_dbContext);
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
                PhoneNumber = "+48 485 212 352",
                DateOfBirth = DateTime.Now.AddYears(-20)
            };

            var result = userservice.Validation(user);

            Assert.That(result, Is.EqualTo(String.Empty));
        }

        // Incorrect data

        [Test]
        public void Validation_IncorrectUserName_ShouldReturnErrorString()
        {
            var userservice = new UserService(_dbContext);
            var user = new User
            {
                UserName = "12Jo€l Super Hero",
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

            var result = userservice.Validation(user);

            Assert.That(result, Is.EqualTo("username"));
        }

        [Test]
        public void Validation_ToShortPassword_ShouldReturnErrorString()
        {
            var userservice = new UserService(_dbContext);
            var user = new User
            {
                UserName = "Joe",
                Email = "Joe@example.exam",
                RegistrationDate = DateTime.Now,
                PasswordHash = "Pass",
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

            var result = userservice.Validation(user);

            Assert.That(result, Is.EqualTo("password"));
        }

        [Test]
        public void Validation_NoCapitalLetterInPassword_ShouldReturnErrorString()
        {
            var userservice = new UserService(_dbContext);
            var user = new User
            {
                UserName = "Joe",
                Email = "Joe@example.exam",
                RegistrationDate = DateTime.Now,
                PasswordHash = "pa55word",
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

            var result = userservice.Validation(user);

            Assert.That(result, Is.EqualTo("password"));
        }

        [Test]
        public void Validation_NoSignInPassword_ShouldReturnErrorString()
        {
            var userservice = new UserService(_dbContext);
            var user = new User
            {
                UserName = "Joe",
                Email = "Joe@example.exam",
                RegistrationDate = DateTime.Now,
                PasswordHash = "Password",
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

            var result = userservice.Validation(user);

            Assert.That(result, Is.EqualTo("password"));
        }

        [Test]
        public void Validation_NoSignAndCapitalLetterInPassword_ShouldReturnErrorString()
        {
            var userservice = new UserService(_dbContext);
            var user = new User
            {
                UserName = "Joe",
                Email = "Joe@example.exam",
                RegistrationDate = DateTime.Now,
                PasswordHash = "password",
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

            var result = userservice.Validation(user);

            Assert.That(result, Is.EqualTo("password"));
        }

        [Test]
        public void Validation_ToLongEmail_ShouldReturnErrorString()
        {
            var userservice = new UserService(_dbContext);
            var user = new User
            {
                UserName = "Joe",
                Email = "Joe@example.example",
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

            var result = userservice.Validation(user);

            Assert.That(result, Is.EqualTo("email"));
        }

        [Test]
        public void Validation_NoAtInEmail_ShouldReturnErrorString()
        {
            var userservice = new UserService(_dbContext);
            var user = new User
            {
                UserName = "Joe",
                Email = "Joeexample.exam",
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

            var result = userservice.Validation(user);

            Assert.That(result, Is.EqualTo("email"));
        }

        [Test]
        public void Validation_NoDotInEmail_ShouldReturnErrorString()
        {
            var userservice = new UserService(_dbContext);
            var user = new User
            {
                UserName = "Joe",
                Email = "Joe@exampleexam",
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

            var result = userservice.Validation(user);

            Assert.That(result, Is.EqualTo("email"));
        }

        [Test]
        public void Validation_IncorrectFirstName_ShouldReturnErrorString()
        {
            var userservice = new UserService(_dbContext);
            var user = new User
            {
                UserName = "Joe",
                Email = "Joe@example.exam",
                RegistrationDate = DateTime.Now,
                PasswordHash = "Pa55word",
                IsVerifiedEmail = false,
                FirstName = "Joe1",
                LastName = "Doe",
                PostalCode = "00-000",
                City = "Unknown",
                Street = "Unknown",
                HouseNumber = "30A",
                PhoneNumber = "485212352",
                DateOfBirth = DateTime.Now.AddYears(-20)
            };

            var result = userservice.Validation(user);

            Assert.That(result, Is.EqualTo("first name"));
        }

        [Test]
        public void Validation_IncorrectLastName_ShouldReturnErrorString()
        {
            var userservice = new UserService(_dbContext);
            var user = new User
            {
                UserName = "Joe",
                Email = "Joe@example.exam",
                RegistrationDate = DateTime.Now,
                PasswordHash = "Pa55word",
                IsVerifiedEmail = false,
                FirstName = "Joe",
                LastName = "D0e",
                PostalCode = "00-000",
                City = "Unknown",
                Street = "Unknown",
                HouseNumber = "30A",
                PhoneNumber = "485212352",
                DateOfBirth = DateTime.Now.AddYears(-20)
            };

            var result = userservice.Validation(user);

            Assert.That(result, Is.EqualTo("last name"));
        }

        [Test]
        public void Validation_IncorrectPostalCodeToManyDigitsInFirst_ShouldReturnErrorString()
        {
            var userservice = new UserService(_dbContext);
            var user = new User
            {
                UserName = "Joe",
                Email = "Joe@example.exam",
                RegistrationDate = DateTime.Now,
                PasswordHash = "Pa55word",
                IsVerifiedEmail = false,
                FirstName = "Joe",
                LastName = "Doe",
                PostalCode = "000-000",
                City = "Unknown",
                Street = "Unknown",
                HouseNumber = "30A",
                PhoneNumber = "485212352",
                DateOfBirth = DateTime.Now.AddYears(-20)
            };

            var result = userservice.Validation(user);

            Assert.That(result, Is.EqualTo("zipcode"));
        }

        [Test]
        public void Validation_IncorrectPostalCodeToManyDigitsInSecond_ShouldReturnErrorString()
        {
            var userservice = new UserService(_dbContext);
            var user = new User
            {
                UserName = "Joe",
                Email = "Joe@example.exam",
                RegistrationDate = DateTime.Now,
                PasswordHash = "Pa55word",
                IsVerifiedEmail = false,
                FirstName = "Joe",
                LastName = "Doe",
                PostalCode = "00-0000",
                City = "Unknown",
                Street = "Unknown",
                HouseNumber = "30A",
                PhoneNumber = "485212352",
                DateOfBirth = DateTime.Now.AddYears(-20)
            };

            var result = userservice.Validation(user);

            Assert.That(result, Is.EqualTo("zipcode"));
        }

        [Test]
        public void Validation_NoUnderscoreInPostalCode_ShouldReturnErrorString()
        {
            var userservice = new UserService(_dbContext);
            var user = new User
            {
                UserName = "Joe",
                Email = "Joe@example.exam",
                RegistrationDate = DateTime.Now,
                PasswordHash = "Pa55word",
                IsVerifiedEmail = false,
                FirstName = "Joe",
                LastName = "Doe",
                PostalCode = "00000",
                City = "Unknown",
                Street = "Unknown",
                HouseNumber = "30A",
                PhoneNumber = "485212352",
                DateOfBirth = DateTime.Now.AddYears(-20)
            };

            var result = userservice.Validation(user);

            Assert.That(result, Is.EqualTo("zipcode"));
        }

        [Test]
        public void Validation_IncorrectCity_ShouldReturnErrorString()
        {
            var userservice = new UserService(_dbContext);
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
                City = "O1d Town",
                Street = "Unknown",
                HouseNumber = "30A",
                PhoneNumber = "485212352",
                DateOfBirth = DateTime.Now.AddYears(-20)
            };

            var result = userservice.Validation(user);

            Assert.That(result, Is.EqualTo("place"));
        }

        [Test]
        public void Validation_IncorrectStreet_ShouldReturnErrorString()
        {
            var userservice = new UserService(_dbContext);
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
                Street = "@ Street",
                HouseNumber = "30A",
                PhoneNumber = "485212352",
                DateOfBirth = DateTime.Now.AddYears(-20)
            };

            var result = userservice.Validation(user);

            Assert.That(result, Is.EqualTo("road"));
        }

        [Test]
        public void Validation_IncorrectHouseNumber_ShouldReturnErrorString()
        {
            var userservice = new UserService(_dbContext);
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
                HouseNumber = "!28",
                PhoneNumber = "485212352",
                DateOfBirth = DateTime.Now.AddYears(-20)
            };

            var result = userservice.Validation(user);

            Assert.That(result, Is.EqualTo("house number"));
        }

        [Test]
        public void Validation_IncorrectPhoneNumberToManyDigits_ShouldReturnErrorString()
        {
            var userservice = new UserService(_dbContext);
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
                PhoneNumber = "485212352242",
                DateOfBirth = DateTime.Now.AddYears(-20)
            };

            var result = userservice.Validation(user);

            Assert.That(result, Is.EqualTo("telephone"));
        }

        [Test]
        public void Validation_SignsInPhoneNumber_ShouldReturnErrorString()
        {
            var userservice = new UserService(_dbContext);
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
                PhoneNumber = "48S2123S2",
                DateOfBirth = DateTime.Now.AddYears(-20)
            };

            var result = userservice.Validation(user);

            Assert.That(result, Is.EqualTo("telephone"));
        }
    }
}
