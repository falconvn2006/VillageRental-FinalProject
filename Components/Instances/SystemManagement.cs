using VillageRental.Components.Data;
using VillageRental.Components.Data.Exceptions;

namespace VillageRental.Components.Instances
{
    public class SystemManagement
    {
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
        }


        #region Customer Management Functions
        public void AddCustomerToList(Customer _customer)
        {
            if (FindCustomer(_customer.CustomerID) != null)
                throw new SystemHandler("Customer with that ID already exists!");

            customerList.Add(_customer);
        }

        public void UpdateCustomer(int _customerId, Customer _newCustomerData)
        {
            Customer customerFound = FindCustomer(_customerId);

            if (customerFound != null)
            {
                customerFound.UpdateCustomer(_newCustomerData.CustomerID, _newCustomerData.LastName, _newCustomerData.FirstName, _newCustomerData.PhoneNumber, _newCustomerData.Email);
            }
        }

        public Customer FindCustomer(int _customerId)
        {
            foreach (Customer customer in customerList)
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
			foreach (RentalInformation rentalInformation in rentalInformationList)
			{
                if (rentalInformation.CustomerID == _customerId)
					throw new SystemHandler(301, "There is an existing rental information with this customer ID. Deletion cannot perform.");
			}

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
            if (FindEquipment(_equipment.EquipmentID) != null)
                throw new SystemHandler("Equipment with that ID already exists!");

            if (FindCategory(_equipment.CategoryID) == null)
                throw new SystemHandler("Cannot add equipment with a non existing category to list!");

            equipmentList.Add(_equipment);
        }

        public void RemoveEquipmentFromInventory(int _equipmentID)
        {
			foreach (RentalInformation rentalInformation in rentalInformationList)
            {
                foreach(RentalItem rentalItem in rentalInformation.rentalItemList)
                {
                    if(rentalItem.EquipmentID == _equipmentID)
                    {
                        throw new SystemHandler(302, "There is an existing rental information with this equipment ID. Deletion cannot perform.");
                    }
                }
            }

			Equipment equipmentFound = FindEquipment(_equipmentID);
            if (equipmentFound != null)
                equipmentList.Remove(equipmentFound);
        }

        public void UpdateEquipment(int _equipmentID, Equipment _newEquipmentData)
        {
            Equipment equipmentToUpdate = FindEquipment(_equipmentID);

            if(equipmentToUpdate != null)
            {
                equipmentToUpdate.UpdateEquipment(_newEquipmentData);
            }
        }

        public Equipment FindEquipment(int _equipmentID)
        {
            foreach (Equipment equipment in equipmentList)
            {
                if (equipment.EquipmentID == _equipmentID)
                    return equipment;
            }

            return null;
        }

        public List<Equipment> FindEquipmentMultipleResult(int _equipmentID)
        {
            List<Equipment> result = new List<Equipment>();
            foreach(Equipment equipment in equipmentList)
            {
                if(equipment.EquipmentID == _equipmentID)
                    result.Add(equipment);
            }

            return result;
        }

        public List<Equipment> FindEquipmentMultipleResultByCategoryID(int _categoryID)
        {
            List<Equipment> result = new List<Equipment> ();
            foreach(Equipment equipment in equipmentList)
            {
                if(equipment.CategoryID == _categoryID)
                    result.Add(equipment);
            }

            return result;
        }

        public List<Equipment> FindEquipmentMultipleResultByName(string _name)
        {
            List<Equipment> result = new List<Equipment>();
            foreach(Equipment equipment in equipmentList)
            {
                if(equipment.Name == _name)
                    result.Add(equipment);
            }

            return result;
        }

        #endregion

        #region Category Management Functions

        public void AddNewCategory(CategoryItem _newCategoryItem)
        {
            if (FindCategory(_newCategoryItem.CategoryID) != null)
                throw new SystemHandler("Category with that ID already exists!");

            categoryList.Add(_newCategoryItem);
        }

        public void RemoveCategory(int _categoryID)
        {
            foreach(Equipment equipment in equipmentList)
            {
                if (equipment.CategoryID == _categoryID)
                    throw new SystemHandler(304, "There is an existing equipment with this category ID. Deletion cannot perform.");
            }

            foreach(RentalInformation rentalInformation in rentalInformationList)
            {
                foreach(RentalItem rentalItem in rentalInformation.rentalItemList)
                {
                    if (FindEquipment(rentalItem.EquipmentID).CategoryID == _categoryID)
                        throw new SystemHandler(303, "There is an existing rental information with an equipment that has this category ID. Deletion cannot perform.");
                }
            }


            CategoryItem categoryFound = FindCategory(_categoryID);
            if(categoryFound != null)
                categoryList.Remove(categoryFound);
        }

        public void UpdateCategory(int _categoryID, CategoryItem _newCategoryData)
        {
            CategoryItem categoryToChange = FindCategory(_categoryID);
            categoryToChange.Description = _newCategoryData.Description;
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
            if (FindRentalInformation(_rentalInformation.RentalID) != null)
                throw new SystemHandler("Rental Information with that ID already exists!");

            rentalInformationList.Add(_rentalInformation);
        }

        public RentalInformation FindRentalInformation(int _rentalId)
        {
            foreach(RentalInformation information in  rentalInformationList)
            {
                if(information.RentalID == _rentalId)
                    return information;
            }


            return null;
        }
	}
}
