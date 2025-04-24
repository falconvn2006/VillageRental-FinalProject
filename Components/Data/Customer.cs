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

        /// <summary>
        /// Construct a new Customer object. The constructor will also check the email and phone number format using regular expresison
        /// </summary>
        /// <param name="_customerID"></param>
        /// <param name="_lastName"></param>
        /// <param name="_firstName"></param>
        /// <param name="_phoneNumber"></param>
        /// <param name="_email"></param>
        /// <param name="_isBanned"></param>
        /// <exception cref="SystemHandler"></exception>
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

        /// <summary>
        /// Update the customer data with a new customer data object.
        /// The function will also check email and phone number format using regular expression
        /// </summary>
        /// <param name="_newCustomerData"></param>
        /// <exception cref="SystemHandler"></exception>
        public void UpdateCustomer(Customer _newCustomerData)
        {
			if (!regexEmail.IsMatch(_newCustomerData.Email))
				throw new SystemHandler(100, "Email is invalid format");
			if (!regexPhoneNumber.IsMatch(_newCustomerData.PhoneNumber) || _newCustomerData.PhoneNumber.Length != 14)
				throw new SystemHandler(100, "Phone number must be in this format '(555) 555-5555'. And must be 14 characters long");

			LastName = _newCustomerData.LastName;
            FirstName = _newCustomerData.FirstName;
            PhoneNumber = _newCustomerData.PhoneNumber;
            Email = _newCustomerData.Email;
        }

        /// <summary>
        /// Set the isBanned to true
        /// </summary>
        /// <param name="banned"></param>
        public void SetBan(bool banned)
        {
            isBanned = banned;
        }

        /// <summary>
        /// Return a string of all the properties of the object seperated with a semicolon
        /// </summary>
        /// <returns></returns>
		public override string ToString()
		{
			return $"{CustomerID};{LastName};{FirstName};{PhoneNumber};{Email};{isBanned}";
		}
	}
}
