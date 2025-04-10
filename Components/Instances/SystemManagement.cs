using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VillageRental.Components.Data;

namespace VillageRental.Components.Instances
{
    public static class ExampleData
    {
		public static string CategoryContent = "10;Power tools\r\n20;Yard equipment\r\n30;Compressors\r\n40;Generators\r\n50;Air Tools";
        public static string RentalInformationContent = "1000;15/02/2024;1001;201;20/02/2024;23/02/2024;149,97\r\n1001;16/02/2024;1002;501;21/02/2024;25/02/2024;43,96";
        public static string CustomerContent = "1001;Doe;John;(555) 555-1212;jd@sample.net;False\r\n1002;Smith;Jane;(555) 555-3434;js@live.com;False\r\n1003;Lee;Michael;(555) 555-5656;ml@sample.net;False";
        public static string EquipmentContent = "101;10;Hammer drill;Powerful drill for concrete and masonry;25,99;1;Good\r\n201;20;Chainsaw;Gas-powered chainsaw for cutting wood;49,99;1;Good\r\n202;20;Lawn mower;Self-propelled lawn mower with mulching function;19,99;1;Good\r\n301;30;Small Compressor;5 Gallon Compressor-Portable;14,99;1;Good\r\n501;50;Brad Nailer;Brad Nailer. Requires 3/4 to 1 1/2 Brad Nails;10,99;1;Good";
	}

    public class SystemManagement
    {
        public bool loadExampleData = true, overwriteExampleDataOnStart = true;

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

            if (loadExampleData)
            {
                string fileCategoryPath = FileSystem.Current.AppDataDirectory + "\\category.txt";
                string fileRentalInformationPath = FileSystem.Current.AppDataDirectory + "\\rental_information.txt";
                string fileCustomerPath = FileSystem.Current.AppDataDirectory + "\\customer.txt";
                string fileEquipmentPath = FileSystem.Current.AppDataDirectory + "\\equipment.txt";

                if (overwriteExampleDataOnStart)
                {
                    using (StreamWriter stream = File.CreateText(fileCategoryPath))
                    {
                        stream.Write(ExampleData.CategoryContent);
                    }

                    using (StreamWriter stream = File.CreateText(fileCustomerPath))
                    {
                        stream.Write(ExampleData.CustomerContent);
                    }

                    using (StreamWriter stream = File.CreateText(fileEquipmentPath))
                    {
                        stream.Write(ExampleData.EquipmentContent);
                    }

					using (StreamWriter stream = File.CreateText(fileRentalInformationPath))
					{
						stream.Write(ExampleData.RentalInformationContent);
					}
				}

                using (StreamReader stream = File.OpenText(fileCategoryPath))
                {
                    while (!stream.EndOfStream)
                    {
                        string[] content = stream.ReadLine().Trim().Split(';');
                        int categoryId = Convert.ToInt32(content[0]);
                        string categoryDescription = content[1];

                        CategoryItem categoryItem = new CategoryItem(categoryId, categoryDescription);
                        AddNewCategory(categoryItem);
                    }
                }

                using (StreamReader stream = File.OpenText(fileCustomerPath))
                {
                    while (!stream.EndOfStream)
                    {
                        string[] content = stream.ReadLine().Trim().Split(';');
                        int customerId = Convert.ToInt32(content[0]);
                        string lastName = content[1];
                        string firstName = content[2];
                        string phoneNumber = content[3];
                        string email = content[4];
                        bool isBanned = Convert.ToBoolean(content[5]);

                        Customer customerData = new Customer(customerId, lastName, firstName, phoneNumber, email, isBanned);
                        AddCustomerToList(customerData);
                    }
                }

                using (StreamReader stream = File.OpenText(fileEquipmentPath))
                {
                    while (!stream.EndOfStream)
                    {
                        string[] content = stream.ReadLine().Trim().Split(';');
                        int equipmentId = Convert.ToInt32(content[0]);
                        int categoryId = Convert.ToInt32(content[1]);
                        string name = content[2];
                        string description = content[3];
                        double dailyRentalCost = Convert.ToDouble(content[4]);
                        int quantity = Convert.ToInt32(content[5]);
                        string status = content[6];

                        Equipment newEquipment = new Equipment(equipmentId, categoryId, name, description, dailyRentalCost, quantity, status);
                        AddEquipmentToList(newEquipment);
                    }
                }

                LoadRentalInformation(fileRentalInformationPath);
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
                equipmentList.Remove(equipmentFound);
        }

        public void SellEquipment(int _equipmentID, double _priceToSell)
        {
            Equipment equipmentFound = FindEquipment(_equipmentID);
            if (equipmentFound != null)
                equipmentFound.SetIsSold(true);
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

        public RentalInformation FindRentalInformation(int _rentalId)
        {
            foreach(RentalInformation information in  rentalInformationList)
            {
                if(information.RentalID == _rentalId)
                    return information;
            }


            return null;
        }

        private void LoadRentalInformation(string _filePath)
        {
            using (StreamReader stream = File.OpenText(_filePath))
            {
                while (!stream.EndOfStream)
                {
                    string[] content = stream.ReadLine().Trim().Split(';');
                    int rentalId = Convert.ToInt32(content[0]);
                    DateTime currentDate = Convert.ToDateTime(content[1]);
                    int customerId = Convert.ToInt32(content[2]);
                    int equipmentId = Convert.ToInt32(content[3]);
                    DateTime rentalDate = Convert.ToDateTime(content[4]);
                    DateTime returnDate = Convert.ToDateTime(content[5]);
                    double totalCost = Convert.ToDouble(content[6]);

                    RentalInformation rentalInformation = FindRentalInformation(rentalId);

                    if(rentalInformation == null)
                    {
						string customerLastName = FindCustomer(customerId).LastName;
						rentalInformation = new RentalInformation(rentalId, currentDate, customerId, customerLastName, "Renting");

                        double rentalCost = ((double)(returnDate - rentalDate).Days * FindEquipment(equipmentId).DailyRentalCost);
                        RentalItem newRentalItem = new RentalItem(equipmentId, rentalDate, returnDate, rentalCost, 1);

                        rentalInformation.AddRentalItem(newRentalItem);
					}
                    else
                    {
						double rentalCost = ((double)(returnDate - rentalDate).Days * FindEquipment(equipmentId).DailyRentalCost);
						RentalItem newRentalItem = new RentalItem(equipmentId, rentalDate, returnDate, rentalCost, 1);

                        rentalInformation.AddRentalItem(newRentalItem);
					}

                    rentalInformationList.Add(rentalInformation);
                }
            }
        }
    }
}
