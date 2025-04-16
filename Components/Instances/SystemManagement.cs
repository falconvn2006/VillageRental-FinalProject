using VillageRental.Components.Data;
using VillageRental.Components.Data.Exceptions;

namespace VillageRental.Components.Instances
{
    public static class ExampleData
    {
		public static string CategoryContent = "10;Power tools\r\n20;Yard equipment\r\n30;Compressors\r\n40;Generators\r\n50;Air Tools";
        public static string RentalInformationContent = "1000;15/02/2024;1001;201;20/02/2024;23/02/2024;149,97;1;Renting\r\n1001;16/02/2024;1002;501;21/02/2024;25/02/2024;43,96;1;Renting";
        public static string CustomerContent = "1001;Doe;John;(555) 555-1212;jd@sample.net;False\r\n1002;Smith;Jane;(555) 555-3434;js@live.com;False\r\n1003;Lee;Michael;(555) 555-5656;ml@sample.net;False";
        public static string EquipmentContent = "101;10;Hammer drill;Powerful drill for concrete and masonry;25,99;1;Good\r\n201;20;Chainsaw;Gas-powered chainsaw for cutting wood;49,99;1;Good\r\n202;20;Lawn mower;Self-propelled lawn mower with mulching function;19,99;1;Good\r\n301;30;Small Compressor;5 Gallon Compressor-Portable;14,99;1;Good\r\n501;50;Brad Nailer;Brad Nailer. Requires 3/4 to 1 1/2 Brad Nails;10,99;1;Good";
    }

    public static class DataFilePath
    {
		public static string fileCategoryPath = FileSystem.Current.AppDataDirectory + "\\category.txt";
		public static string fileRentalInformationPath = FileSystem.Current.AppDataDirectory + "\\rental_information.txt";
		public static string fileCustomerPath = FileSystem.Current.AppDataDirectory + "\\customer.txt";
		public static string fileEquipmentPath = FileSystem.Current.AppDataDirectory + "\\equipment.txt";
	}


    public class SystemManagement
    {
        public bool writeExampleDataIfNotExits = true;

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

            if(writeExampleDataIfNotExits)
                WriteExampleData(false);
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

        private void WriteExampleData(bool overwriteOnStart)
        {
            if(!File.Exists(DataFilePath.fileCategoryPath) || overwriteOnStart)
		        using (StreamWriter stream = File.CreateText(DataFilePath.fileCategoryPath))
                    stream.Write(ExampleData.CategoryContent);

            if(!File.Exists(DataFilePath.fileCustomerPath) || overwriteOnStart)
			    using (StreamWriter stream = File.CreateText(DataFilePath.fileCustomerPath))
				    stream.Write(ExampleData.CustomerContent);

            if(!File.Exists(DataFilePath.fileEquipmentPath) || overwriteOnStart)
		        using (StreamWriter stream = File.CreateText(DataFilePath.fileEquipmentPath))
			        stream.Write(ExampleData.EquipmentContent);

            if(!File.Exists(DataFilePath.fileRentalInformationPath) || overwriteOnStart)
				using (StreamWriter stream = File.CreateText(DataFilePath.fileRentalInformationPath))
					stream.Write(ExampleData.RentalInformationContent);
		}
	}
}
