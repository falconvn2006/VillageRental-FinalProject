using MySqlConnector;
using VillageRental.Components.Data;
using VillageRental.Components.Data.Exceptions;

namespace VillageRental.Components.Instances
{
	class DatabaseManager
	{
		public bool loadOnStart = false;

		private MySqlConnection connection;

		public DatabaseManager(string serverAddress, string username, string password, string databaseName) 
		{ 
			MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder
			{
				Server = serverAddress,
				UserID = username,
				Password = password,
				Database = databaseName,
			};

			connection = new MySqlConnection(builder.ConnectionString);
		}

		#region Load Data
		public void LoadCategory(SystemManagement sysManagement, string _filePath)
		{
			connection.Open();

			MySqlCommand command = new MySqlCommand("SELECT * FROM category_item", connection);
			MySqlDataReader reader = command.ExecuteReader();

			while(reader.Read())
			{
				CategoryItem newCategoryItem = new CategoryItem(reader.GetInt32(0), reader.GetString(1));
				sysManagement.AddNewCategory(newCategoryItem);
			}

			connection.Close();
		}

		public void LoadCustomer(SystemManagement sysManagement, string _filePath)
		{
			connection.Open();

			MySqlCommand command = new MySqlCommand("SELECT * FROM customer", connection);
			MySqlDataReader reader = command.ExecuteReader();

			while (reader.Read())
			{
				Customer newCustomerData = new Customer(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetBoolean(5));
				sysManagement.AddCustomerToList(newCustomerData);
			}

			connection.Close();
		}

		public void LoadEquipment(SystemManagement sysManagement, string _filePath)
		{
			//using (StreamReader stream = File.OpenText(_filePath))
			//{
			//	while (!stream.EndOfStream)
			//	{
			//		string[] content = stream.ReadLine().Trim().Split(';');

			//		if (content.Length == 0)
			//			continue;

			//		foreach (string item in content)
			//		{
			//			if (string.IsNullOrWhiteSpace(item))
			//				throw new SystemHandler(500, "Data is missing or one of the line in the file is empty!");
			//		}

			//		int equipmentId = Convert.ToInt32(content[0]);
			//		int categoryId = Convert.ToInt32(content[1]);
			//		string name = content[2];
			//		string description = content[3];
			//		double dailyRentalCost = Convert.ToDouble(content[4]);
			//		int quantity = Convert.ToInt32(content[5]);
			//		string status = content[6];

			//		Equipment newEquipment = new Equipment(equipmentId, categoryId, name, description, dailyRentalCost, quantity, status);
			//		sysManagement.AddEquipmentToList(newEquipment);
			//	}
			//}

			connection.Open();

			MySqlCommand command = new MySqlCommand("SELECT * FROM equipment", connection);
			MySqlDataReader reader = command.ExecuteReader();

			while (reader.Read())
			{
				int equipmentId = reader.GetInt32(0);
				int categoryId = reader.GetInt32(1);
				string name = reader.GetString(2);
				string description = reader.GetString(3);
				double dailyRentalCost = reader.GetDouble(4);
				string equipmentStatus = reader.GetString(5);
				int availableQuantity = reader.GetInt32(6);

				Equipment newEquipmentData = new Equipment(equipmentId, categoryId, name, description, dailyRentalCost, availableQuantity, equipmentStatus);
				sysManagement.AddEquipmentToList(newEquipmentData);
			}

			connection.Close();
		}

		public void LoadRentalInformation(SystemManagement sysManagement, string _filePath)
		{
			connection.Open();

			MySqlCommand command = new MySqlCommand("SELECT rinfo.rental_id, rinfo.date_of_rental_creation, rinfo.customer_id, ritem.equipment_id, ritem.rental_date, ritem.return_date, ritem.quantity, ritem.cost_of_rental, rinfo.rental_status FROM rental_information rinfo JOIN rental_item ritem ON (rinfo.rental_id = ritem.rental_id);", connection);

			connection.Close();
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
