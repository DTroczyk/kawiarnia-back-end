using Api.DAL.EF;
using Api.Services.Services;
using Api.ViewModels.ViewModel;
using NUnit.Framework;
using System;

namespace Api.UnitTests.ServiceTests
{
    public class UserServiceTests
    {
        private readonly ApplicationDbContext _dbContext;

        // Date validation
        [Test]
        public void DateValidation_CorrectDate_ShouldReturnTrue()
        {
            var userservice = new UserService(_dbContext);
            var date = new Date
            {
                year = "1998",
                month = "7",
                day = "21"
            };

            var result = userservice.DateValidation(date);

            Assert.That(result, Is.EqualTo(true));
        }

        [Test]
        public void DateValidation_CorrectDateIsLeap_ShouldReturnTrue()
        {
            var userservice = new UserService(_dbContext);
            var date = new Date
            {
                year = "2000", // leap year
                month = "2",
                day = "29"
            };

            var result = userservice.DateValidation(date);

            Assert.That(result, Is.EqualTo(true));
        }

        [Test]
        public void DateValidation_IncorrectDateIsNotLeap_ShouldReturnFalse()
        {
            var userservice = new UserService(_dbContext);
            var date = new Date
            {
                year = "2001", // not leap year
                month = "2",
                day = "29"
            };

            var result = userservice.DateValidation(date);

            Assert.That(result, Is.EqualTo(false));
        }

        [Test]
        public void DateValidation_IncorrectDateOutOfRangeDayUp_ShouldReturnFalse()
        {
            var userservice = new UserService(_dbContext);
            var date = new Date
            {
                year = "2001", // not leap year
                month = "4",
                day = "31"
            };

            var result = userservice.DateValidation(date);

            Assert.That(result, Is.EqualTo(false));
        }

        [Test]
        public void DateValidation_IncorrectDateOutOfRangeDayDown_ShouldReturnFalse()
        {
            var userservice = new UserService(_dbContext);
            var date = new Date
            {
                year = "2001", // not leap year
                month = "4",
                day = "0"
            };

            var result = userservice.DateValidation(date);

            Assert.That(result, Is.EqualTo(false));
        }

        [Test]
        public void DateValidation_IncorrectDateOutOfRangeMonthUp_ShouldReturnFalse()
        {
            var userservice = new UserService(_dbContext);
            var date = new Date
            {
                year = "2001", // not leap year
                month = "13",
                day = "21"
            };

            var result = userservice.DateValidation(date);

            Assert.That(result, Is.EqualTo(false));
        }

        [Test]
        public void DateValidation_IncorrectDateOutOfRangeMonthDown_ShouldReturnFalse()
        {
            var userservice = new UserService(_dbContext);
            var date = new Date
            {
                year = "2001", // not leap year
                month = "0",
                day = "21"
            };

            var result = userservice.DateValidation(date);

            Assert.That(result, Is.EqualTo(false));
        }

        // Correct data

        [Test]
        public void Validation_CorrectData_ShouldReturnEmptyString()
        {
            var userservice = new UserService(_dbContext);
            var user = new UserVm
            {
                username = "Joe",
                email = "Joe@example.exam",
                password = "Pa55word",
                firstName = "Joe",
                lastName = "Doe",
                zipcode = "00-000",
                place = "Unknown",
                road = "Unknown",
                houseNumber = "30A",
                telephone = "485212352",
                dateOfBirth = new Date()
                {
                    day = DateTime.Now.Day.ToString(),
                    month = DateTime.Now.Month.ToString(),
                    year = (DateTime.Now.Year-20).ToString()
                }
            };

            var result = userservice.Validation(user);

            Assert.That(result, Is.EqualTo(String.Empty));
        }

        // Correct data with space on the end

        [Test]
        public void Validation_CorrectDataWithSpaceOnTheEnd_ShouldReturnEmptyString()
        {
            var userservice = new UserService(_dbContext);
            var user = new UserVm
            {
                username = "Joe  ",
                email = "Joe@example.exam   ",
                password = "Pa55word ",
                firstName = "Joe   ",
                lastName = "Doe ",
                zipcode = "00-000  ",
                place = "New York   ",
                road = "Unknown ",
                houseNumber = "30A   ",
                telephone = "485212352  ",
                dateOfBirth = new Date()
                {
                    day = DateTime.Now.Day.ToString(),
                    month = DateTime.Now.Month.ToString(),
                    year = (DateTime.Now.Year - 20).ToString()
                }
            };

            var result = userservice.Validation(user);

            Assert.That(result, Is.EqualTo(String.Empty));
            Assert.That(user.place, Is.EqualTo("New York"));
        }

        // Correct Postal code
        [Test]
        public void Validation_NoUnderscoreInPostalCode_ShouldReturnEmptyString()
        {
            var userservice = new UserService(_dbContext);
            var user = new UserVm
            {
                username = "Joe",
                email = "Joe@example.exam",
                password = "Pa55word",
                firstName = "Joe",
                lastName = "Doe",
                zipcode = "00000",
                place = "Unknown",
                road = "Unknown",
                houseNumber = "30A",
                telephone = "485212352",
                dateOfBirth = new Date()
                {
                    day = DateTime.Now.Day.ToString(),
                    month = DateTime.Now.Month.ToString(),
                    year = (DateTime.Now.Year-20).ToString()
                }
            };

            var result = userservice.Validation(user);

            Assert.That(result, Is.EqualTo(String.Empty));
        }

        // Correct German data
        [Test]
        public void Validation_CorrectGermanData_ShouldReturnEmptyString()
        {
            var userservice = new UserService(_dbContext);
            var user = new UserVm
            {
                username = "Joe",
                email = "Joe@example.exam",
                password = "Pa55word",
                firstName = "Jan-Michael",
                lastName = "Weißmeister",
                zipcode = "01324",
                place = "Dresden",
                road = "Wießiger Weg",
                houseNumber = "30A",
                telephone = "485212352",
                dateOfBirth = new Date()
                {
                    day = DateTime.Now.Day.ToString(),
                    month = DateTime.Now.Month.ToString(),
                    year = (DateTime.Now.Year-20).ToString()
                }
            };

            var result = userservice.Validation(user);

            Assert.That(result, Is.EqualTo(String.Empty));
        }

        // Correct Street
        [Test]
        public void Validation_EmptyStreet_ShouldReturnEmptyString()
        {
            var userservice = new UserService(_dbContext);
            var user = new UserVm
            {
                username = "Joe",
                email = "Joe@example.exam",
                password = "Pa55word",
                firstName = "Joe",
                lastName = "Doe",
                zipcode = "00-000",
                place = "Unknown",
                road = "",
                houseNumber = "30A",
                telephone = "485212352",
                dateOfBirth = new Date()
                {
                    day = DateTime.Now.Day.ToString(),
                    month = DateTime.Now.Month.ToString(),
                    year = (DateTime.Now.Year-20).ToString()
                }
            };

            var result = userservice.Validation(user);

            Assert.That(result, Is.EqualTo(String.Empty));
        }

        [Test]
        public void Validation_StreetWithSpace_ShouldReturnEmptyString()
        {
            var userservice = new UserService(_dbContext);
            var user = new UserVm
            {
                username = "Joe",
                email = "Joe@example.exam",
                password = "Pa55word",
                firstName = "Joe",
                lastName = "Doe",
                zipcode = "00-000",
                place = "Unknown",
                road = "Groove Street",
                houseNumber = "30A",
                telephone = "485212352",
                dateOfBirth = new Date()
                {
                    day = DateTime.Now.Day.ToString(),
                    month = DateTime.Now.Month.ToString(),
                    year = (DateTime.Now.Year-20).ToString()
                }
            };

            var result = userservice.Validation(user);

            Assert.That(result, Is.EqualTo(String.Empty));
        }

        // Correct city
        [Test]
        public void Validation_CityWithSpace_ShouldReturnEmptyString()
        {
            var userservice = new UserService(_dbContext);
            var user = new UserVm
            {
                username = "Joe",
                email = "Joe@example.exam",
                password = "Pa55word",
                firstName = "Joe",
                lastName = "Doe",
                zipcode = "00-000",
                place = "New York",
                road = "Unknown",
                houseNumber = "30A",
                telephone = "485212352",
                dateOfBirth = new Date()
                {
                    day = DateTime.Now.Day.ToString(),
                    month = DateTime.Now.Month.ToString(),
                    year = (DateTime.Now.Year-20).ToString()
                }
            };

            var result = userservice.Validation(user);

            Assert.That(result, Is.EqualTo(String.Empty));
        }

        // Correct PhoneNumber
        [Test]
        public void Validation_PhoneNumberWithAreaCodeWithSpace_ShouldReturnEmptyString()
        {
            var userservice = new UserService(_dbContext);
            var user = new UserVm
            {
                username = "Joe",
                email = "Joe@example.exam",
                password = "Pa55word",
                firstName = "Joe",
                lastName = "Doe",
                zipcode = "00-000",
                place = "Unknown",
                road = "Unknown",
                houseNumber = "30A",
                telephone = "+48 521 235 562",
                dateOfBirth = new Date()
                {
                    day = DateTime.Now.Day.ToString(),
                    month = DateTime.Now.Month.ToString(),
                    year = (DateTime.Now.Year-20).ToString()
                }
            };

            var result = userservice.Validation(user);

            Assert.That(result, Is.EqualTo(String.Empty));
        }

        [Test]
        public void Validation_PhoneNumberWithAreaCodeWithoutSpace_ShouldReturnEmptyString()
        {
            var userservice = new UserService(_dbContext);
            var user = new UserVm
            {
                username = "Joe",
                email = "Joe@example.exam",
                password = "Pa55word",
                firstName = "Joe",
                lastName = "Doe",
                zipcode = "00-000",
                place = "Unknown",
                road = "Unknown",
                houseNumber = "30A",
                telephone = "+48125212352",
                dateOfBirth = new Date()
                {
                    day = DateTime.Now.Day.ToString(),
                    month = DateTime.Now.Month.ToString(),
                    year = (DateTime.Now.Year-20).ToString()
                }
            };

            var result = userservice.Validation(user);

            Assert.That(result, Is.EqualTo(String.Empty));
        }

        // Incorrect data

        [Test]
        public void Validation_IncorrectUserName_ShouldReturnErrorString()
        {
            var userservice = new UserService(_dbContext);
            var user = new UserVm
            {
                username = "Jo€l Super",
                email = "Joe@example.exam",
                password = "Pa55word",
                firstName = "Joe",
                lastName = "Doe",
                zipcode = "00-000",
                place = "Unknown",
                road = "Unknown",
                houseNumber = "30A",
                telephone = "485212352",
                dateOfBirth = new Date()
                {
                    day = DateTime.Now.Day.ToString(),
                    month = DateTime.Now.Month.ToString(),
                    year = (DateTime.Now.Year-20).ToString()
                }
            };
            var result = userservice.Validation(user);

            Assert.That(result, Is.EqualTo("username"));
        }

        [Test]
        public void Validation_ToShortPassword_ShouldReturnErrorString()
        {
            var userservice = new UserService(_dbContext);
            var user = new UserVm
            {
                username = "Joe",
                email = "Joe@example.exam",
                password = "Pass",
                firstName = "Joe",
                lastName = "Doe",
                zipcode = "00-000",
                place = "Unknown",
                road = "Unknown",
                houseNumber = "30A",
                telephone = "485212352",
                dateOfBirth = new Date()
                {
                    day = DateTime.Now.Day.ToString(),
                    month = DateTime.Now.Month.ToString(),
                    year = (DateTime.Now.Year-20).ToString()
                }
            };

            var result = userservice.Validation(user);

            Assert.That(result, Is.EqualTo("password"));
        }

        [Test]
        public void Validation_NoCapitalLetterInPassword_ShouldReturnErrorString()
        {
            var userservice = new UserService(_dbContext);
            var user = new UserVm
            {
                username = "Joe",
                email = "Joe@example.exam",
                password = "pa55word",
                firstName = "Joe",
                lastName = "Doe",
                zipcode = "00-000",
                place = "Unknown",
                road = "Unknown",
                houseNumber = "30A",
                telephone = "485212352",
                dateOfBirth = new Date()
                {
                    day = DateTime.Now.Day.ToString(),
                    month = DateTime.Now.Month.ToString(),
                    year = (DateTime.Now.Year-20).ToString()
                }
            };

            var result = userservice.Validation(user);

            Assert.That(result, Is.EqualTo("password"));
        }

        [Test]
        public void Validation_NoDigitInPassword_ShouldReturnErrorString()
        {
            var userservice = new UserService(_dbContext);
            var user = new UserVm
            {
                username = "Joe",
                email = "Joe@example.exam",
                password = "Password",
                firstName = "Joe",
                lastName = "Doe",
                zipcode = "00-000",
                place = "Unknown",
                road = "Unknown",
                houseNumber = "30A",
                telephone = "485212352",
                dateOfBirth = new Date()
                {
                    day = DateTime.Now.Day.ToString(),
                    month = DateTime.Now.Month.ToString(),
                    year = (DateTime.Now.Year-20).ToString()
                }
            };

            var result = userservice.Validation(user);

            Assert.That(result, Is.EqualTo("password"));
        }

        [Test]
        public void Validation_NoSignAndCapitalLetterInPassword_ShouldReturnErrorString()
        {
            var userservice = new UserService(_dbContext);
            var user = new UserVm
            {
                username = "Joe",
                email = "Joe@example.exam",
                password = "password",
                firstName = "Joe",
                lastName = "Doe",
                zipcode = "00-000",
                place = "Unknown",
                road = "Unknown",
                houseNumber = "30A",
                telephone = "485212352",
                dateOfBirth = new Date()
                {
                    day = DateTime.Now.Day.ToString(),
                    month = DateTime.Now.Month.ToString(),
                    year = (DateTime.Now.Year-20).ToString()
                }
            };

            var result = userservice.Validation(user);

            Assert.That(result, Is.EqualTo("password"));
        }

        [Test]
        public void Validation_ToLongEmail_ShouldReturnErrorString()
        {
            var userservice = new UserService(_dbContext);
            var user = new UserVm
            {
                username = "Joe",
                email = "Joe@example.example",
                password = "Pa55word",
                firstName = "Joe",
                lastName = "Doe",
                zipcode = "00-000",
                place = "Unknown",
                road = "Unknown",
                houseNumber = "30A",
                telephone = "485212352",
                dateOfBirth = new Date()
                {
                    day = DateTime.Now.Day.ToString(),
                    month = DateTime.Now.Month.ToString(),
                    year = (DateTime.Now.Year-20).ToString()
                }
            };

            var result = userservice.Validation(user);

            Assert.That(result, Is.EqualTo("email"));
        }

        [Test]
        public void Validation_NoAtInEmail_ShouldReturnErrorString()
        {
            var userservice = new UserService(_dbContext);
            var user = new UserVm
            {
                username = "Joe",
                email = "Joeexample.exam",
                password = "Pa55word",
                firstName = "Joe",
                lastName = "Doe",
                zipcode = "00-000",
                place = "Unknown",
                road = "Unknown",
                houseNumber = "30A",
                telephone = "485212352",
                dateOfBirth = new Date()
                {
                    day = DateTime.Now.Day.ToString(),
                    month = DateTime.Now.Month.ToString(),
                    year = (DateTime.Now.Year-20).ToString()
                }
            };

            var result = userservice.Validation(user);

            Assert.That(result, Is.EqualTo("email"));
        }

        [Test]
        public void Validation_NoDotInEmail_ShouldReturnErrorString()
        {
            var userservice = new UserService(_dbContext);
            var user = new UserVm
            {
                username = "Joe",
                email = "Joe@exampleexam",
                password = "Pa55word",
                firstName = "Joe",
                lastName = "Doe",
                zipcode = "00-000",
                place = "Unknown",
                road = "Unknown",
                houseNumber = "30A",
                telephone = "485212352",
                dateOfBirth = new Date()
                {
                    day = DateTime.Now.Day.ToString(),
                    month = DateTime.Now.Month.ToString(),
                    year = (DateTime.Now.Year-20).ToString()
                }
            };

            var result = userservice.Validation(user);

            Assert.That(result, Is.EqualTo("email"));
        }

        [Test]
        public void Validation_IncorrectFirstName_ShouldReturnErrorString()
        {
            var userservice = new UserService(_dbContext);
            var user = new UserVm
            {
                username = "Joe",
                email = "Joe@example.exam",
                password = "Pa55word",
                firstName = "Joe1",
                lastName = "Doe",
                zipcode = "00-000",
                place = "Unknown",
                road = "Unknown",
                houseNumber = "30A",
                telephone = "485212352",
                dateOfBirth = new Date()
                {
                    day = DateTime.Now.Day.ToString(),
                    month = DateTime.Now.Month.ToString(),
                    year = (DateTime.Now.Year-20).ToString()
                }
            };

            var result = userservice.Validation(user);

            Assert.That(result, Is.EqualTo("first name"));
        }

        [Test]
        public void Validation_IncorrectLastName_ShouldReturnErrorString()
        {
            var userservice = new UserService(_dbContext);
            var user = new UserVm
            {
                username = "Joe",
                email = "Joe@example.exam",
                password = "Pa55word",
                firstName = "Joe",
                lastName = "D0e",
                zipcode = "00-000",
                place = "Unknown",
                road = "Unknown",
                houseNumber = "30A",
                telephone = "485212352",
                dateOfBirth = new Date()
                {
                    day = DateTime.Now.Day.ToString(),
                    month = DateTime.Now.Month.ToString(),
                    year = (DateTime.Now.Year-20).ToString()
                }
            };

            var result = userservice.Validation(user);

            Assert.That(result, Is.EqualTo("last name"));
        }

        [Test]
        public void Validation_IncorrectPostalCodeToManyDigitsInFirst_ShouldReturnErrorString()
        {
            var userservice = new UserService(_dbContext);
            var user = new UserVm
            {
                username = "Joe",
                email = "Joe@example.exam",
                password = "Pa55word",
                firstName = "Joe",
                lastName = "Doe",
                zipcode = "000-000",
                place = "Unknown",
                road = "Unknown",
                houseNumber = "30A",
                telephone = "485212352",
                dateOfBirth = new Date()
                {
                    day = DateTime.Now.Day.ToString(),
                    month = DateTime.Now.Month.ToString(),
                    year = (DateTime.Now.Year-20).ToString()
                }
            };

            var result = userservice.Validation(user);

            Assert.That(result, Is.EqualTo("zipcode"));
        }

        [Test]
        public void Validation_IncorrectPostalCodeToManyDigitsInSecond_ShouldReturnErrorString()
        {
            var userservice = new UserService(_dbContext);
            var user = new UserVm
            {
                username = "Joe",
                email = "Joe@example.exam",
                password = "Pa55word",
                firstName = "Joe",
                lastName = "Doe",
                zipcode = "00-0000",
                place = "Unknown",
                road = "Unknown",
                houseNumber = "30A",
                telephone = "485212352",
                dateOfBirth = new Date()
                {
                    day = DateTime.Now.Day.ToString(),
                    month = DateTime.Now.Month.ToString(),
                    year = (DateTime.Now.Year-20).ToString()
                }
            };

            var result = userservice.Validation(user);

            Assert.That(result, Is.EqualTo("zipcode"));
        }

        [Test]
        public void Validation_IncorrectCity_ShouldReturnErrorString()
        {
            var userservice = new UserService(_dbContext);
            var user = new UserVm
            {
                username = "Joe",
                email = "Joe@example.exam",
                password = "Pa55word",
                firstName = "Joe",
                lastName = "Doe",
                zipcode = "00-000",
                place = "0ld T0wn",
                road = "Unknown",
                houseNumber = "30A",
                telephone = "485212352",
                dateOfBirth = new Date()
                {
                    day = DateTime.Now.Day.ToString(),
                    month = DateTime.Now.Month.ToString(),
                    year = (DateTime.Now.Year-20).ToString()
                }
            };

            var result = userservice.Validation(user);

            Assert.That(result, Is.EqualTo("place"));
        }

        [Test]
        public void Validation_IncorrectStreet_ShouldReturnErrorString()
        {
            var userservice = new UserService(_dbContext);
            var user = new UserVm
            {
                username = "Joe",
                email = "Joe@example.exam",
                password = "Pa55word",
                firstName = "Joe",
                lastName = "Doe",
                zipcode = "00-000",
                place = "Unknown",
                road = "@Street",
                houseNumber = "30A",
                telephone = "485212352",
                dateOfBirth = new Date()
                {
                    day = DateTime.Now.Day.ToString(),
                    month = DateTime.Now.Month.ToString(),
                    year = (DateTime.Now.Year-20).ToString()
                }
            };

            var result = userservice.Validation(user);

            Assert.That(result, Is.EqualTo("road"));
        }

        [Test]
        public void Validation_IncorrectHouseNumber_ShouldReturnErrorString()
        {
            var userservice = new UserService(_dbContext);
            var user = new UserVm
            {
                username = "Joe",
                email = "Joe@example.exam",
                password = "Pa55word",
                firstName = "Joe",
                lastName = "Doe",
                zipcode = "00-000",
                place = "Unknown",
                road = "Unknown",
                houseNumber = "!30A",
                telephone = "485212352",
                dateOfBirth = new Date()
                {
                    day = DateTime.Now.Day.ToString(),
                    month = DateTime.Now.Month.ToString(),
                    year = (DateTime.Now.Year-20).ToString()
                }
            };

            var result = userservice.Validation(user);

            Assert.That(result, Is.EqualTo("house number"));
        }

        [Test]
        public void Validation_IncorrectPhoneNumberToManyDigits_ShouldReturnErrorString()
        {
            var userservice = new UserService(_dbContext);
            var user = new UserVm
            {
                username = "Joe",
                email = "Joe@example.exam",
                password = "Pa55word",
                firstName = "Joe",
                lastName = "Doe",
                zipcode = "00-000",
                place = "Unknown",
                road = "Unknown",
                houseNumber = "30A",
                telephone = "48521235552142",
                dateOfBirth = new Date()
                {
                    day = DateTime.Now.Day.ToString(),
                    month = DateTime.Now.Month.ToString(),
                    year = (DateTime.Now.Year-20).ToString()
                }
            };

            var result = userservice.Validation(user);

            Assert.That(result, Is.EqualTo("telephone"));
        }

        [Test]
        public void Validation_SignsInPhoneNumber_ShouldReturnErrorString()
        {
            var userservice = new UserService(_dbContext);
            var user = new UserVm
            {
                username = "Joe",
                email = "Joe@example.exam",
                password = "Pa55word",
                firstName = "Joe",
                lastName = "Doe",
                zipcode = "00-000",
                place = "Unknown",
                road = "Unknown",
                houseNumber = "30A",
                telephone = "48S2!23S2",
                dateOfBirth = new Date()
                {
                    day = DateTime.Now.Day.ToString(),
                    month = DateTime.Now.Month.ToString(),
                    year = (DateTime.Now.Year-20).ToString()
                }
            };

            var result = userservice.Validation(user);

            Assert.That(result, Is.EqualTo("telephone"));
        }
    }
}
