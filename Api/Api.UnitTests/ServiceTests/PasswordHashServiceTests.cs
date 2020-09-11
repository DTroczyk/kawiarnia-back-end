using Api.BLL.Entity;
using Api.Services.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Api.UnitTests.ServiceTests
{
    class PasswordHashServiceTests
    {
        [Test]
        public void HashPassword_CorrectData_ShouldReturnHashedPassword()
        {
            string password = "Pa55word";
            string hashedPass = String.Empty;
            string salt = String.Empty;
            for (int i = 0; i < 50; i++)
            {
                var user = PasswordHashService.HashPassword(password);
                var newHashedPass = user.PasswordHash;
                var newSalt = user.Salt;
                Assert.That(hashedPass, Is.Not.EqualTo(newHashedPass));
                Assert.That(salt, Is.Not.EqualTo(newSalt));
                hashedPass = newHashedPass;
                salt = newSalt;
            }
        }

        [Test]
        public void ValidatePassword_CorrectPassword_ShouldReturnTrue()
        {
            var user = PasswordHashService.HashPassword("Pa55word");
            bool result = PasswordHashService.ValidatePassword("Pa55word", user);
            Assert.IsTrue(result);
        }

        [Test]
        public void ValidatePassword_WrongPassword_ShouldReturnFalse()
        {
            var user = PasswordHashService.HashPassword("Pa55word");
            bool result = PasswordHashService.ValidatePassword("Password", user);
            Assert.IsFalse(result);
        }
    }
}
