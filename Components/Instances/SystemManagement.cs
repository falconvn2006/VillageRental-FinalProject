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
        public bool loadExampleData = true;

        public List<Customer> customerList;
		public List<Equipment> equipmentList;
		public List<CategoryItem> categoryList;
		public List<RentalInformation> rentalInformationList;

        public SystemManagement()
        {
            customerList = new List<Customer>();
            equipmentList = new List<Equipment>();
            categoryList = new List<CategoryItem>();
            rentalInformationList = new List<RentalInformation>();

            if(loadExampleData)
            {

            }
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

        public List<Customer> FindCustomerMultipleResult(int _customerId)
        {
            List<Customer> customerResultList = new List<Customer>();

			foreach (Customer customer in customerList)
			{
				if (customer.CustomerID == _customerId)
				{
					customerResultList.Add(customer);
				}
			}

			return customerResultList;
		}

		public List<Customer> FindCustomerMultipleResultByLastName(string _lastName)
		{
			List<Customer> customerResultList = new List<Customer>();

			foreach (Customer customer in customerList)
			{
				if (customer.LastName == _lastName)
				{
					customerResultList.Add(customer);
				}
			}

			return customerResultList;
		}

		public List<Customer> FindCustomerMultipleResultByEmail(string _email)
		{
			List<Customer> customerResultList = new List<Customer>();

			foreach (Customer customer in customerList)
			{
				if (customer.Email == _email)
				{
					customerResultList.Add(customer);
				}
			}

			return customerResultList;
		}

		public void RemoveCustomer(int _customerId)
        {
            Customer foundCustomer = FindCustomer(_customerId);
            if (foundCustomer != null)
            {
                customerList.Remove(foundCustomer);
            }
        }

        public void BanCustomer(int _customerId)
        {
            Customer customerFound = FindCustomer(_customerId);
            if (customerFound != null)
                customerFound.SetBan(true);
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

        public void SellEquipment(int _equipmentID, double _priceToSell)
        {
            Equipment equipmentFound = FindEquipment(_equipmentID);
            if (equipmentFound != null)
                equipmentFound.SetIsSold(true);
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

        #region Category Management Functions

        public void AddNewCategory(CategoryItem _newCategoryItem)
        {
            categoryList.Add(_newCategoryItem);
        }

        public void RemoveCategory(int _categoryID)
        {
            CategoryItem categoryFound = FindCategory(_categoryID);
            if(categoryFound != null)
                categoryList.Remove(categoryFound);
        }

        public CategoryItem FindCategory(int _categoryID)
        {
            foreach(CategoryItem categoryItem in categoryList)
            {
                if(categoryItem.CategoryID == _categoryID)
                    return categoryItem;
            }

            return null;
        }

        #endregion

        public void RentItem(RentalInformation _rentalInformation)
        {
            rentalInformationList.Add(_rentalInformation);
        }
    }
}
