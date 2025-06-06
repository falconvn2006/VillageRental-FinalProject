﻿using MySqlConnector;
using System.Globalization;
using VillageRental.Components.Data;
using VillageRental.Components.Data.Exceptions;
using VillageRental.Components.Instances.Ultility;

namespace VillageRental.Components.Instances
{
	class DatabaseManager : IDataManager
	{
		public bool loadOnStart = false;
		private bool connected = false;

		private MySqlConnection connection;

		NumberFormatInfo nfi = new NumberFormatInfo();

		/// <summary>
		/// Try to connect to the a MariaDb and test the connection
		/// </summary>
		/// <param name="serverAddress"></param>
		/// <param name="username"></param>
		/// <param name="password"></param>
		/// <param name="databaseName"></param>
		public DatabaseManager(string serverAddress = "", string username = "", string password = "", string databaseName = "")
		{
			nfi.NumberDecimalSeparator = ".";

			MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder
			{
				Server = string.IsNullOrEmpty(serverAddress) ? "localhost" : serverAddress,
				UserID = string.IsNullOrEmpty(username) ? "root" : username,
				Password = string.IsNullOrEmpty(password) ? "admin123" : password,
				Database = string.IsNullOrEmpty(databaseName) ? "village_rentals" : databaseName,
			};

			connection = new MySqlConnection(builder.ConnectionString);

			try
			{
				connection.Open();

				connected = true;

				connection.Close();
			}
			catch (Exception ex)
			{
				SystemHandler systemHandler = new SystemHandler(900, "Cannot connect to database. The program will not load data and save data!");
				systemHandler.DisplayError();
			}
		}

		#region Load Data

		public void LoadAll(SystemManagement sysManagement)
		{
			LoadCategory(sysManagement);
			LoadCustomer(sysManagement);
			LoadEquipment(sysManagement);
			LoadRentalInformation(sysManagement);
		}

		public void LoadCategory(SystemManagement sysManagement)
		{
			if (!connected)
				return;

			if (connection.State == System.Data.ConnectionState.Open)
				connection.Close();

			connection.Open();

			MySqlCommand command = new MySqlCommand("SELECT * FROM category_item", connection);
			using (MySqlDataReader reader = command.ExecuteReader())
			{
				while (reader.Read())
				{
					CategoryItem newCategoryItem = new CategoryItem(reader.GetInt32(0), reader.GetString(1));
					sysManagement.AddNewCategory(newCategoryItem);
				}
			}

			connection.Close();
		}

		public void LoadCustomer(SystemManagement sysManagement)
		{
			if (!connected)
				return;

			if (connection.State == System.Data.ConnectionState.Open)
				connection.Close();

			connection.Open();

			MySqlCommand command = new MySqlCommand("SELECT * FROM customer", connection);
			using (MySqlDataReader reader = command.ExecuteReader())
			{
				while (reader.Read())
				{
					Customer newCustomerData = new Customer(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetBoolean(5));
					sysManagement.AddCustomerToList(newCustomerData);
				}
			}

			connection.Close();
		}

		public void LoadEquipment(SystemManagement sysManagement)
		{
			if (!connected)
				return;

			if (connection.State == System.Data.ConnectionState.Open)
				connection.Close();

			connection.Open();

			MySqlCommand command = new MySqlCommand("SELECT * FROM equipment", connection);
			using (MySqlDataReader reader = command.ExecuteReader())
			{
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
			}

			connection.Close();
		}

		public void LoadRentalInformation(SystemManagement sysManagement)
		{
			if (!connected)
				return;

			if (connection.State == System.Data.ConnectionState.Open)
				connection.Close();

			connection.Open();

			MySqlCommand command = new MySqlCommand("SELECT * FROM rental_information;", connection);
			using (MySqlDataReader reader = command.ExecuteReader())
			{
				while (reader.Read())
				{
					int rentalId = reader.GetInt32(0);
					DateTime dateOfRentalCreation = reader.GetDateTime(1);
					int customerId = reader.GetInt32(2);
					string rentalStatus = reader.GetString(3);

					RentalInformation newRentalInformationData = new RentalInformation(rentalId, dateOfRentalCreation, customerId, sysManagement.FindCustomer(customerId).LastName, rentalStatus);

					sysManagement.RentItem(newRentalInformationData);
				}
			}

			foreach (RentalInformation information in sysManagement.rentalInformationList)
			{
				MySqlCommand rentalItemCommand = new MySqlCommand($"SELECT * FROM rental_item WHERE rental_id = {information.RentalID}", connection);
				using (MySqlDataReader reader = rentalItemCommand.ExecuteReader())
				{
					while (reader.Read())
					{
						int equipmentId = reader.GetInt32(1);
						DateTime rentalDate = reader.GetDateTime(2);
						DateTime returnDate = reader.GetDateTime(3);
						int quantity = reader.GetInt32(4);
						double costOfRental = reader.GetDouble(5);


						RentalItem newRentalItemData = new RentalItem(equipmentId, rentalDate, returnDate, costOfRental, quantity);
						information.AddRentalItem(newRentalItemData);
					}
				}
			}
			connection.Close();
		}

		#endregion

		#region Write Data

		public void WriteAll(SystemManagement sysManagement)
		{
			WriteCategoryData(sysManagement);
			WriteCustomerData(sysManagement);
			WriteEquipmentData(sysManagement);
			WriteRentalInformationData(sysManagement);
		}

		public void WriteCategoryData(SystemManagement sysManagement)
		{
			if (!connected)
				return;

			if (connection.State == System.Data.ConnectionState.Open)
				connection.Close();

			connection.Open();

			foreach (CategoryItem item in sysManagement.categoryList)
			{
				MySqlCommand command = new MySqlCommand($"INSERT INTO category_item VALUES ({item.CategoryID}, '{item.Description}') ON DUPLICATE KEY UPDATE description='{item.Description}';", connection);
				command.ExecuteNonQuery();
			}

			connection.Close();
		}

		public void WriteCustomerData(SystemManagement sysManagement)
		{
			if (!connected)
				return;

			if (connection.State == System.Data.ConnectionState.Open)
				connection.Close();

			connection.Open();

			foreach (Customer customer in sysManagement.customerList)
			{
				MySqlCommand command = new MySqlCommand($"INSERT INTO customer VALUES ({customer.CustomerID}, '{customer.LastName}', '{customer.FirstName}', '{customer.PhoneNumber}', '{customer.Email}', {(customer.isBanned ? 1 : 0)}) ON DUPLICATE KEY UPDATE last_name='{customer.LastName}', first_name='{customer.FirstName}', phone_number='{customer.PhoneNumber}', email='{customer.Email}', is_banned={(customer.isBanned ? 1 : 0)};", connection);
				command.ExecuteNonQuery();
			}

			connection.Close();
		}

		public void WriteEquipmentData(SystemManagement sysManagement)
		{
			if (!connected)
				return;

			if (connection.State == System.Data.ConnectionState.Open)
				connection.Close();

			connection.Open();

			foreach (Equipment equipment in sysManagement.equipmentList)
			{
				//Debug.WriteLine($"INSERT INTO equipment VALUES ({equipment.EquipmentID}, {equipment.CategoryID}, '{equipment.Name}', '{equipment.Description}', {equipment.DailyRentalCost}, '{equipment.EquipmentStatus}', {equipment.AvailableQuantity}) ON DUPLICATE KEY UPDATE category_id={equipment.CategoryID}, name='{equipment.Name}', description='{equipment.Description}', daily_rental_cost={equipment.DailyRentalCost}, equipment_status='{equipment.EquipmentStatus}', available_quantity={equipment.AvailableQuantity};");
				MySqlCommand command = new MySqlCommand($"INSERT INTO equipment VALUES ({equipment.EquipmentID}, {equipment.CategoryID}, '{equipment.Name}', '{equipment.Description}', {equipment.DailyRentalCost.ToString(nfi)}, '{equipment.EquipmentStatus}', {equipment.AvailableQuantity}) ON DUPLICATE KEY UPDATE category_id={equipment.CategoryID}, name='{equipment.Name}', description='{equipment.Description}', daily_rental_cost={equipment.DailyRentalCost.ToString(nfi)}, equipment_status='{equipment.EquipmentStatus}', available_quantity={equipment.AvailableQuantity};", connection);
				command.ExecuteNonQuery();
			}

			connection.Close();
		}

		public void WriteRentalInformationData(SystemManagement sysManagement)
		{
			if (!connected)
				return;

			if (connection.State == System.Data.ConnectionState.Open)
				connection.Close();

			connection.Open();

			foreach (RentalInformation information in sysManagement.rentalInformationList)
			{
				string currentDate = $"{information.CurrentDate.Date.Year}-{information.CurrentDate.Date.Month}-{information.CurrentDate.Date.Day}";
				MySqlCommand rentalInformationCommand = new MySqlCommand($"INSERT INTO rental_information VALUES ({information.RentalID}, '{currentDate}', {information.CustomerID}, '{information.RentalStatus}') ON DUPLICATE KEY UPDATE current_date_of_rental='{currentDate}', customer_id={information.CustomerID}, rental_status='{information.RentalStatus}'", connection);
				rentalInformationCommand.ExecuteNonQuery();

				foreach (RentalItem item in information.rentalItemList)
				{
					string rentalDate = $"{item.RentalDate.Date.Year}-{item.RentalDate.Date.Month}-{item.RentalDate.Date.Day}";
					string returnDate = $"{item.ReturnDate.Date.Year}-{item.ReturnDate.Date.Month}-{item.ReturnDate.Date.Day}";
					MySqlCommand rentalItemCommand = new MySqlCommand($"INSERT INTO rental_item VALUES ({information.RentalID}, {item.EquipmentID}, '{rentalDate}', '{returnDate}', {item.Quantity}, {item.CostOfRental.ToString(nfi)}) ON DUPLICATE KEY UPDATE rental_date='{rentalDate}', return_date='{returnDate}', quantity={item.Quantity}, cost_of_rental={item.CostOfRental.ToString(nfi)}", connection);
					rentalItemCommand.ExecuteNonQuery();
				}
			}

			connection.Close();
		}
		#endregion

		#region Delete Data

		public void DeleteCategory(int _categoryIdToDelete)
		{
			if (!connected)
				return;

			if (connection.State == System.Data.ConnectionState.Open)
				connection.Close();

			connection.Open();

			MySqlCommand deletionCommand = new MySqlCommand($"DELETE FROM category_item WHERE category_id={_categoryIdToDelete}", connection);
			deletionCommand.ExecuteNonQuery();

			connection.Close();
		}

		public void DeleteCustomer(int _customerIdToDelete)
		{
			if (!connected)
				return;

			if (connection.State == System.Data.ConnectionState.Open)
				connection.Close();

			connection.Open();

			MySqlCommand deletionCommand = new MySqlCommand($"DELETE FROM customer WHERE customer_id={_customerIdToDelete}", connection);
			deletionCommand.ExecuteNonQuery();

			connection.Close();
		}

		public void DeleteEquipment(int _equipmentIdToDelete)
		{
			if (!connected)
				return;

			if (connection.State == System.Data.ConnectionState.Open)
				connection.Close();

			connection.Open();

			MySqlCommand deletionCommand = new MySqlCommand($"DELETE FROM equipment WHERE equipment_id={_equipmentIdToDelete}", connection);
			deletionCommand.ExecuteNonQuery();

			connection.Close();
		}

		public void DeleteRentalItem(int _rentalInformationIdToDelete, int _rentalEquipmentIdToDelete)
		{
			if (!connected)
				return;

			if (connection.State == System.Data.ConnectionState.Open)
				connection.Close();

			connection.Open();

			MySqlCommand deletionCommand = new MySqlCommand($"DELETE FROM rental_item WHERE rental_id={_rentalInformationIdToDelete} AND equipment_id={_rentalEquipmentIdToDelete}", connection);
			deletionCommand.ExecuteNonQuery();

			connection.Close();
		}

		public void DeleteAllRentalItemInRentalInformation(int _rentalInformationIdToDelete)
		{
			if (!connected)
				return;

			if (connection.State == System.Data.ConnectionState.Open)
				connection.Close();

			connection.Open();

			MySqlCommand deletionCommand = new MySqlCommand($"DELETE FROM rental_item WHERE rental_id={_rentalInformationIdToDelete}", connection);
			deletionCommand.ExecuteNonQuery();

			connection.Close();
		}

		public void DeleteRentalInformation(int _rentalInformationIdToDelete)
		{
			DeleteAllRentalItemInRentalInformation(_rentalInformationIdToDelete);

			if (!connected)
				return;

			if (connection.State == System.Data.ConnectionState.Open)
				connection.Close();

			connection.Open();

			MySqlCommand deletionCommand = new MySqlCommand($"DELETE FROM rental_information WHERE rental_id={_rentalInformationIdToDelete}", connection);
			deletionCommand.ExecuteNonQuery();

			connection.Close();
		}

		#endregion
	}
}
