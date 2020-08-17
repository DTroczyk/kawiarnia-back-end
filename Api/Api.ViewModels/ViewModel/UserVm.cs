using System;
using System.Collections.Generic;
using System.Text;

namespace Api.ViewModels.ViewModel
{
    public class UserVm
    {
        public string username { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public DateTime dateOfBirth { get; set; }
        public string road { get; set; }
        public string houseNumber { get; set; }
        public string zipcode { get; set; }
        public string place { get; set; }
        public string telephone { get; set; }
    }
}
