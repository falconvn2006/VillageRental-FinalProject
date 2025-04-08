using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public Customer(int _customerID, string _lastName, string _firstName, string _phoneNumber, string _email, bool _isBanned = false) 
        { 
            CustomerID = _customerID;
            LastName = _lastName;
            FirstName = _firstName;
            PhoneNumber = _phoneNumber;
            Email = _email;
            isBanned = _isBanned;
        }

        public void UpdateCustomer(string _newLastName, string _newFirstName, string _newPhoneNumber, string _newEmail)
        {
            LastName = _newLastName;
            FirstName = _newFirstName;
            PhoneNumber = _newPhoneNumber;
            Email = _newEmail;
        }

        public void SetBan(bool banned)
        {
            isBanned = banned;
        }
    }
}
