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

        /// <summary>
        /// Add a new customer object to the system list, but only if there isn't already a customer with the ID already in there
        /// </summary>
        /// <param name="_customer"></param>
        /// <exception cref="SystemHandler"></exception>
        public void AddCustomerToList(Customer _customer)
        {
            if (FindCustomer(_customer.CustomerID) != null)
                throw new SystemHandler("Customer with that ID already exists!");

            customerList.Add(_customer);
        }

        /// <summary>
        /// Update the customer with this ID to a new data
        /// </summary>
        /// <param name="_customerId"></param>
        /// <param name="_newCustomerData"></param>
        public void UpdateCustomer(int _customerId, Customer _newCustomerData)
        {
            Customer customerFound = FindCustomer(_customerId);

            if (customerFound != null)
            {
                customerFound.UpdateCustomer(_newCustomerData);
                foreach (RentalInformation information in rentalInformationList)
                {
                    if (information.CustomerID == _customerId)
                    {
                        information.CustomerLastName = _newCustomerData.LastName;
                    }
                }
            }
        }

        /// <summary>
        /// Find a single customer with this ID
        /// </summary>
        /// <param name="_customerId"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Find multiple customer with this id
        /// </summary>
        /// <param name="_customerId"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Find multiple customer with this last name
        /// </summary>
        /// <param name="_lastName"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Find multiple customer with this email
        /// </summary>
        /// <param name="_email"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Remove the customer with this ID from the system list, only when there isn't any rental information with that customer
        /// </summary>
        /// <param name="_customerId"></param>
        /// <exception cref="SystemHandler"></exception>
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

        /// <summary>
        /// Ban a customer with this ID
        /// </summary>
        /// <param name="_customerId"></param>
        public void BanCustomer(int _customerId)
        {
            Customer customerFound = FindCustomer(_customerId);
            if (customerFound != null)
                customerFound.SetBan(true);
        }

        public void UnbanCustomer(int _customerId)
        {
            Customer customerFound = FindCustomer(_customerId);
            if(customerFound != null)
                customerFound.SetBan(false);
        }

        #endregion

        #region Equipment Management Functions

        /// <summary>
        /// Add a new equipment object into the system list, but addition can only happen when there isn't any equipment with this ID
        /// and the category of the equipment must exists in the category list
        /// </summary>
        /// <param name="_equipment"></param>
        /// <exception cref="SystemHandler"></exception>
        public void AddEquipmentToList(Equipment _equipment)
        {
            if (FindEquipment(_equipment.EquipmentID) != null)
                throw new SystemHandler("Equipment with that ID already exists!");

            if (FindCategory(_equipment.CategoryID) == null)
                throw new SystemHandler("Cannot add equipment with a non existing category to list!");

            equipmentList.Add(_equipment);
        }

        /// <summary>
        /// Remove the equipment from the system but removal can only happens if there isn't any
        /// rental information with a rental item that have this equipment
        /// </summary>
        /// <param name="_equipmentID"></param>
        /// <exception cref="SystemHandler"></exception>
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

        /// <summary>
        /// Update the equipment with this ID with new data, but only if the new data category
        /// exists in the category list
        /// </summary>
        /// <param name="_equipmentID"></param>
        /// <param name="_newEquipmentData"></param>
        /// <exception cref="SystemHandler"></exception>
        public void UpdateEquipment(int _equipmentID, Equipment _newEquipmentData)
        {
            if (FindCategory(_newEquipmentData.CategoryID) == null)
				throw new SystemHandler("Cannot update equipment with a non existing category to list!");

			Equipment equipmentToUpdate = FindEquipment(_equipmentID);

            if(equipmentToUpdate != null)
            {
                equipmentToUpdate.UpdateEquipment(_newEquipmentData);
            }
        }

        /// <summary>
        /// Find a single equipment with this ID
        /// </summary>
        /// <param name="_equipmentID"></param>
        /// <returns></returns>
        public Equipment FindEquipment(int _equipmentID)
        {
            foreach (Equipment equipment in equipmentList)
            {
                if (equipment.EquipmentID == _equipmentID)
                    return equipment;
            }

            return null;
        }

        /// <summary>
        /// Find multiple equipment with this ID
        /// </summary>
        /// <param name="_equipmentID"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Find multiple equipment that belongs to this category based on the category ID
        /// </summary>
        /// <param name="_categoryID"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Find multiple equipment with this name
        /// </summary>
        /// <param name="_name"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Add a new category object to the system, but only if there isn't any category with that
        /// id already exists
        /// </summary>
        /// <param name="_newCategoryItem"></param>
        /// <exception cref="SystemHandler"></exception>
        public void AddNewCategory(CategoryItem _newCategoryItem)
        {
            if (FindCategory(_newCategoryItem.CategoryID) != null)
                throw new SystemHandler("Category with that ID already exists!");

            categoryList.Add(_newCategoryItem);
        }

        /// <summary>
        /// Remove the category with this ID, but only if there isn't any equipment with that category
        /// </summary>
        /// <param name="_categoryID"></param>
        /// <exception cref="SystemHandler"></exception>
        public void RemoveCategory(int _categoryID)
        {
            foreach(Equipment equipment in equipmentList)
			{
				if (equipment.CategoryID == _categoryID)
                    throw new SystemHandler(304, "There is an existing equipment with this category ID. Deletion cannot perform.");
            }

            CategoryItem categoryFound = FindCategory(_categoryID);
            if(categoryFound != null)
                categoryList.Remove(categoryFound);
        }

        /// <summary>
        /// Update the category with this ID with the new category data
        /// </summary>
        /// <param name="_categoryID"></param>
        /// <param name="_newCategoryData"></param>
        public void UpdateCategory(int _categoryID, CategoryItem _newCategoryData)
        {
            CategoryItem categoryToChange = FindCategory(_categoryID);
            categoryToChange.Description = _newCategoryData.Description;
        }

        /// <summary>
        /// Find a single category with this ID
        /// </summary>
        /// <param name="_categoryID"></param>
        /// <returns></returns>
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

		#region Rental Management Functions

        /// <summary>
        /// Add a new rental information into the system list, but only if there isn't any
        /// rental information with that ID already exists
        /// </summary>
        /// <param name="_rentalInformation"></param>
        /// <exception cref="SystemHandler"></exception>
		public void RentItem(RentalInformation _rentalInformation)
        {
            if (FindRentalInformation(_rentalInformation.RentalID) != null)
                throw new SystemHandler("Rental Information with that ID already exists!");

            rentalInformationList.Add(_rentalInformation);
        }

        /// <summary>
        /// Remove the rental information with that ID from the system list
        /// </summary>
        /// <param name="_rentalId"></param>
        public void RemoveRentalInformation(int _rentalId)
        {
            RentalInformation informationToRemmove = FindRentalInformation(_rentalId);

            rentalInformationList.Remove(informationToRemmove);
        }

        /// <summary>
        /// Find a single rental information with that rental ID
        /// </summary>
        /// <param name="_rentalId"></param>
        /// <returns></returns>
        public RentalInformation FindRentalInformation(int _rentalId)
        {
            foreach(RentalInformation information in  rentalInformationList)
            {
                if(information.RentalID == _rentalId)
                    return information;
            }


            return null;
        }

		#endregion
	}
}
