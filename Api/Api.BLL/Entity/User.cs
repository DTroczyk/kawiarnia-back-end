using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Api.BLL.Entity
{
    public class User
    {
        [Key]
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        [Required]
        public string PasswordHash { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime RegistrationDate { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        // Address

        public string City { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }
        public string HouseNumber { get; set; }

        public bool IsVerifiedEmail { get; set; }
        public string Salt { get; set; }

        // Collections

        public IList<Order> Orders { get; set; }
    }
}
