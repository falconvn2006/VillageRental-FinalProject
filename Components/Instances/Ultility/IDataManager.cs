namespace VillageRental.Components.Instances.Ultility
{
	public interface IDataManager
	{
		#region Load Data

		/// <summary>
		/// Load all of the category, customer, equipment, rental information all at once
		/// </summary>
		/// <param name="sysManagement"></param>
		void LoadAll(SystemManagement sysManagement);

		/// <summary>
		/// Load all the categories that is saved from a file or a database into the System Management entity
		/// </summary>
		/// <param name="sysManagement"></param>
		void LoadCategory(SystemManagement sysManagement);

		/// <summary>
		/// Load all the customers that are saved from a file or a database into the System Management entity
		/// </summary>
		/// <param name="sysManagement"></param>
		void LoadCustomer(SystemManagement sysManagement);

		/// <summary>
		/// Load all the equipments that are saved from a file or a database into the System Management entity
		/// </summary>
		/// <param name="sysManagement"></param>
		void LoadEquipment(SystemManagement sysManagement);

		/// <summary>
		/// Load all the rental informations that are save from a file or a database into the System Management entity
		/// </summary>
		/// <param name="sysManagement"></param>
		void LoadRentalInformation(SystemManagement sysManagement);

		#endregion

		#region Write Data

		/// <summary>
		/// Save all the category, customer, equipment and rental information all at once
		/// </summary>
		/// <param name="systemManagement"></param>
		void WriteAll(SystemManagement systemManagement);

		/// <summary>
		/// Get all the categories in the System Management entity and write it into a database or a file
		/// </summary>
		/// <param name="sysManagement"></param>
		void WriteCategoryData(SystemManagement sysManagement);

		/// <summary>
		/// Get all the customers in the System Management entity and write it into a database or a file
		/// </summary>
		/// <param name="sysManagement"></param>
		void WriteCustomerData(SystemManagement sysManagement);

		/// <summary>
		/// Get all the equipments in the System Management entity and write it into a database or a file
		/// </summary>
		/// <param name="sysManagement"></param>
		void WriteEquipmentData(SystemManagement sysManagement);

		/// <summary>
		/// Get all the rental informations in the System Management entity and write it into a database or a file
		/// </summary>
		/// <param name="sysManagement"></param>
		void WriteRentalInformationData(SystemManagement sysManagement);

		#endregion

		#region Delete Data
		/// <summary>
		/// Remove a category base on its id from a file or database
		/// </summary>
		/// <param name="_categoryIdToDelete"></param>
		void DeleteCategory(int _categoryIdToDelete);

		/// <summary>
		/// Remove a customer base on its id from a file or database
		/// </summary>
		/// <param name="_customerIdToDelete"></param>
		void DeleteCustomer(int _customerIdToDelete);

		/// <summary>
		/// Remove an equipment base on its id from a file or database
		/// </summary>
		/// <param name="_equipmentIdToDelete"></param>
		void DeleteEquipment(int _equipmentIdToDelete);

		/// <summary>
		/// Remove a rental item base on what rental information it comes from and what equipment to remove
		/// </summary>
		/// <param name="_rentalInformationIdToDelete"></param>
		/// <param name="_rentalEquipmentIdToDelete"></param>
		void DeleteRentalItem(int _rentalInformationIdToDelete, int _rentalEquipmentIdToDelete);

		/// <summary>
		/// Remove all the rental item from a rental information
		/// </summary>
		/// <param name="_rentalInformationIdToDelete"></param>
		void DeleteAllRentalItemInRentalInformation(int _rentalInformationIdToDelete);

		/// <summary>
		/// Remove a rental information base on its id from a file or database
		/// </summary>
		/// <param name="_rentalInformationIdToDelete"></param>
		void DeleteRentalInformation(int _rentalInformationIdToDelete);

		#endregion
	}
}
