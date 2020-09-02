private string Validation(User user)
        {
            string message = "Incorrect: ";

            // username
            Regex regex = new Regex(@"^(?!.*\.\.)(?!.*\.$)[^\W][\w.]{2,29}$");
            if (!regex.IsMatch(user.UserName))
            {
                message += "username, ";
            }
            

            // password
            regex = new Regex(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$");
            if (!regex.IsMatch(user.PasswordHash))
            {
                message += "password, ";
            }

            // first name
            regex = new Regex(@"^([A-z]|[ęóąśłżźćńĘÓĄŚŁŻŹĆŃ]){1,}$");
            if (!regex.IsMatch(user.FirstName))
            {
                message += "first name, ";
            }

            // last name
            if (!regex.IsMatch(user.LastName))
            {
                message += "last name, ";
            }

            // email
            regex = new Regex(@"^\b[\w\.-]+@[\w\.-]+\.\w{2,4}\b$");
            if (!regex.IsMatch(user.Email))
            {
                message += "email, ";
            }

            // zipcode
            regex = new Regex(@"^[0-9]{2}-[0-9]{3}$");
            if (!regex.IsMatch(user.PostalCode))
            {
                message += "zipcode, ";
            }

            // place
            regex = new Regex(@"^([A-z]|[ęóąśłżźćńĘÓĄŚŁŻŹĆŃ]){1,}$");
            if (!regex.IsMatch(user.City))
            {
                message += "place, ";
            }

            // road
            regex = new Regex(@"^([A-z]|[ęóąśłżźćńĘÓĄŚŁŻŹĆŃ]){1,}$");
            if (!regex.IsMatch(user.Street) && user.Street == "")
            {
                message += "road, ";
            }

            // house number
            regex = new Regex(@"^[0-9]{1,}[A-Z]{1}$|^[0-9]{1,}|^[0-9]{1,}[a-z]{1}$");
            if (!regex.IsMatch(user.HouseNumber))
            {
                message += "house number, ";
            }

            // telephone
            regex = new Regex(@"^(([0-9]{9})|(\+{1}[0-9]{2,})|(([0-9]{3} ){2}[0-9]{3})|(\+{1}[0-9]{2,} ([0-9]{3} ){2}[0-9]{3}))$");
            if (!regex.IsMatch(user.PhoneNumber))
            {
                message += "telephone, ";
            }

            // date of birth

            message = message.Remove(message.Length - 2);
            message += ".";

            return message;
        }