using System.Text.RegularExpressions;
using VillageRental.Components.Data.Exceptions;

namespace VillageRental.Components.Data
{
    public class Customer
    {
        public int CustomerID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public bool isBanned;

		private Regex regexEmail = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
		private Regex regexPhoneNumber = new Regex(@"\([0-9]{3}\) [0-9]{3}-[0-9]{4}");

		public Customer(int _customerID, string _lastName, string _firstName, string _phoneNumber, string _email, bool _isBanned = false) 
        {
            if (!regexEmail.IsMatch(_email))
                throw new SystemHandler(100, "Email is invalid format");
            if(!regexPhoneNumber.IsMatch(_phoneNumber) || _phoneNumber.Length != 14)
                throw new SystemHandler(100, "Phone number must be in this format '(555) 555-5555'. And must be 14 characters long");

            CustomerID = _customerID;
            LastName = _lastName;
            FirstName = _firstName;
            PhoneNumber = _phoneNumber;
            Email = _email;
            isBanned = _isBanned;
        }

        public void UpdateCustomer(int _newCustomerID,string _newLastName, string _newFirstName, string _newPhoneNumber, string _newEmail)
        {
			if (!regexEmail.IsMatch(_newEmail))
				throw new SystemHandler(100, "Email is invalid format");
			if (!regexPhoneNumber.IsMatch(_newPhoneNumber) || _newPhoneNumber.Length != 14)
				throw new SystemHandler(100, "Phone number must be in this format '(555) 555-5555'. And must be 14 characters long");

			LastName = _newLastName;
            FirstName = _newFirstName;
            PhoneNumber = _newPhoneNumber;
            Email = _newEmail;
        }

        public void SetBan(bool banned)
        {
            isBanned = banned;
        }

		public override string ToString()
		{
			return $"{CustomerID};{LastName};{FirstName};{PhoneNumber};{Email};{isBanned}";
		}
	}
}
