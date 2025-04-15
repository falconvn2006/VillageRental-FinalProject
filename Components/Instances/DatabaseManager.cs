using VillageRental.Components.Data;
using VillageRental.Components.Data.Exceptions;

namespace VillageRental.Components.Instances
{
	class DatabaseManager
	{
		public bool loadOnStart = false;

		public DatabaseManager() { }

		#region Load Data
		public void LoadCategory(SystemManagement sysManagement, string _filePath)
		{
			using (StreamReader stream = File.OpenText(_filePath))
			{
				while (!stream.EndOfStream)
				{
					string[] content = stream.ReadLine().Trim().Split(';');

					if (content.Length == 0)
						continue;

					foreach (string item in content)
					{
						if (string.IsNullOrWhiteSpace(item))
							throw new SystemHandler(500, "Data is missing or one of the line in the file is empty!");
					}

					int categoryId = Convert.ToInt32(content[0]);
					string categoryDescription = content[1];

					CategoryItem categoryItem = new CategoryItem(categoryId, categoryDescription);
					sysManagement.AddNewCategory(categoryItem);
				}
			}
		}

		public void LoadCustomer(SystemManagement sysManagement, string _filePath)
		{
			using (StreamReader stream = File.OpenText(_filePath))
			{
				while (!stream.EndOfStream)
				{
					string[] content = stream.ReadLine().Trim().Split(';');

					if (content.Length == 0)
						continue;

					foreach (string item in content)
					{
						if (string.IsNullOrWhiteSpace(item))
							throw new SystemHandler(500, "Data is missing or one of the line in the file is empty!");
					}

					int customerId = Convert.ToInt32(content[0]);
					string lastName = content[1];
					string firstName = content[2];
					string phoneNumber = content[3];
					string email = content[4];
					bool isBanned = Convert.ToBoolean(content[5]);

					Customer customerData = new Customer(customerId, lastName, firstName, phoneNumber, email, isBanned);
					sysManagement.AddCustomerToList(customerData);
				}
			}
		}

		public void LoadEquipment(SystemManagement sysManagement, string _filePath)
		{
			using (StreamReader stream = File.OpenText(_filePath))
			{
				while (!stream.EndOfStream)
				{
					string[] content = stream.ReadLine().Trim().Split(';');

					if (content.Length == 0)
						continue;

					foreach (string item in content)
					{
						if (string.IsNullOrWhiteSpace(item))
							throw new SystemHandler(500, "Data is missing or one of the line in the file is empty!");
					}

					int equipmentId = Convert.ToInt32(content[0]);
					int categoryId = Convert.ToInt32(content[1]);
					string name = content[2];
					string description = content[3];
					double dailyRentalCost = Convert.ToDouble(content[4]);
					int quantity = Convert.ToInt32(content[5]);
					string status = content[6];

					Equipment newEquipment = new Equipment(equipmentId, categoryId, name, description, dailyRentalCost, quantity, status);
					sysManagement.AddEquipmentToList(newEquipment);
				}
			}
		}

		public void LoadRentalInformation(SystemManagement sysManagement, string _filePath)
		{
			using (StreamReader stream = File.OpenText(_filePath))
			{
				while (!stream.EndOfStream)
				{
					string[] content = stream.ReadLine().Trim().Split(';');

					if (content.Length == 0)
						continue;

					foreach(string item in content)
					{
						if (string.IsNullOrWhiteSpace(item))
							throw new SystemHandler(500, "Data is missing or one of the line in the file is empty!");
					}

					int rentalId = Convert.ToInt32(content[0]);
					DateTime currentDate = Convert.ToDateTime(content[1]);
					int customerId = Convert.ToInt32(content[2]);
					int equipmentId = Convert.ToInt32(content[3]);
					DateTime rentalDate = Convert.ToDateTime(content[4]);
					DateTime returnDate = Convert.ToDateTime(content[5]);
					double totalCost = Convert.ToDouble(content[6]);
					int quantity = Convert.ToInt32(content[7]);
					string rentalStatus = content[8];

					RentalInformation rentalInformation = sysManagement.FindRentalInformation(rentalId);

					if (rentalInformation == null)
					{
						string customerLastName = sysManagement.FindCustomer(customerId).LastName;
						rentalInformation = new RentalInformation(rentalId, currentDate, customerId, customerLastName, rentalStatus);

						double rentalCost = ((double)(returnDate - rentalDate).Days * sysManagement.FindEquipment(equipmentId).DailyRentalCost);
						RentalItem newRentalItem = new RentalItem(equipmentId, rentalDate, returnDate, rentalCost, quantity);

						rentalInformation.AddRentalItem(newRentalItem);
					}
					else
					{
						double rentalCost = ((double)(returnDate - rentalDate).Days * sysManagement.FindEquipment(equipmentId).DailyRentalCost);
						RentalItem newRentalItem = new RentalItem(equipmentId, rentalDate, returnDate, rentalCost, quantity);

						rentalInformation.AddRentalItem(newRentalItem);
					}

					sysManagement.rentalInformationList.Add(rentalInformation);
				}

				sysManagement.rentalInformationList = sysManagement.rentalInformationList.Distinct().ToList();
			}
		}

		#endregion

		#region Write Data

		public void WriteCategoryData(SystemManagement sysManagement)
		{
			using (StreamWriter stream = File.CreateText(DataFilePath.fileCategoryPath))
			{
				foreach (CategoryItem item in sysManagement.categoryList)
				{
					string content = item.ToString();
					stream.WriteLine(content);
				}
			}
		}

		public void WriteCustomerData(SystemManagement sysManagement)
		{
			using (StreamWriter stream = File.CreateText(DataFilePath.fileCustomerPath))
			{
				foreach (Customer customer in sysManagement.customerList)
				{
					string content = customer.ToString();
					stream.WriteLine(content);
				}
			}
		}

		public void WriteEquipmentData(SystemManagement sysManagement)
		{
			using (StreamWriter stream = File.CreateText(DataFilePath.fileEquipmentPath))
			{
				foreach (Equipment equipment in sysManagement.equipmentList)
				{
					string content = equipment.ToString();
					stream.WriteLine(content);
				}
			}
		}

		public void WriteRentalInformationData(SystemManagement sysManagement)
		{
			using (StreamWriter stream = File.CreateText(DataFilePath.fileRentalInformationPath))
			{
				foreach (RentalInformation information in sysManagement.rentalInformationList)
				{
					string currentDate = $"{information.CurrentDate.Day}/{information.CurrentDate.Month}/{information.CurrentDate.Year}";
					string content = $"{information.RentalID};{currentDate};{information.CustomerID};";
					foreach (RentalItem item in information.rentalItemList)
					{
						string copyContent = content.Clone().ToString();
						string rentalDate = $"{item.RentalDate.Day}/{item.RentalDate.Month}/{item.RentalDate.Year}";
						string returnDate = $"{item.ReturnDate.Day}/{item.ReturnDate.Month}/{item.ReturnDate.Year}";
						copyContent += $"{item.EquipmentID};{rentalDate};{returnDate};{item.CostOfRental};";
						copyContent += $"{item.Quantity};{information.RentalStatus}";

						stream.WriteLine(copyContent);
					}
				}
			}
		}
		#endregion
	}
}
