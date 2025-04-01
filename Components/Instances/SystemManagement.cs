using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VillageRental.Components.Data;

namespace VillageRental.Components.Instances
{
    public class SystemManagement
    {
        public static SystemManagement Instance;

        private List<Customer> customerList;
        private List<Equipment> equipmentList;
        private List<CategoryItem> categoryList;
        private List<RentalInformation> rentalInformationList;

        public SystemManagement()
        {
            if(Instance == null)
                Instance = new SystemManagement();
        }

        #region Customer Management Functions
        public void AddCustomerToList(Customer _customer)
        {
            customerList.Add(_customer);
        }

        public void UpdateCustomer(int _customerId, Customer _newCustomerData)
        {
            Customer customerFound = FindCustomer(_customerId);

            if (customerFound != null)
            {
                customerFound.UpdateCustomer(_newCustomerData.LastName, _newCustomerData.FirstName, _newCustomerData.PhoneNumber, _newCustomerData.Email);
            }
        }

        public Customer FindCustomer(int _customerId)
        {
            foreach(Customer customer in customerList)
            {
                if (customer.CustomerID == _customerId)
                {
                    return customer;
                }
            }

            return null;
        }

        public void RemoveCustomer(int _customerId)
        {
            Customer foundCustomer = FindCustomer(_customerId);
            if (foundCustomer != null)
            {
                customerList.Remove(foundCustomer);
            }
        }

        #endregion

        #region Equipment Management Functions

        public void AddEquipmentToList(Equipment _equipment)
        {
            equipmentList.Add(_equipment);
        }

        public void RemoveEquipmentFromInventory(int _equipmentID)
        {
            Equipment equipmentFound = FindEquipment(_equipmentID);
            if (equipmentFound != null)
                equipmentFound.SetInInventory(false);
        }

        public Equipment FindEquipment(int _equipmentID)
        {
            foreach(Equipment equipment in equipmentList)
            {
                if(equipment.EquipmentID == _equipmentID)
                    return equipment;
            }

            return null;
        }

        #endregion
    }
}
