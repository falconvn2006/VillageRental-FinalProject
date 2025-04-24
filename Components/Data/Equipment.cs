namespace VillageRental.Components.Data
{
    public class Equipment
    {
        public int EquipmentID { get; set; }
        public int CategoryID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double DailyRentalCost { get; set; }
        public int AvailableQuantity { get; set; }
        /// <summary>
        /// The status saying how the equipment
        /// </summary>
        public string EquipmentStatus { get; set; }

        /// <summary>
        /// Construct a new Equipment object
        /// </summary>
        /// <param name="_equipmentID"></param>
        /// <param name="_categoryID"></param>
        /// <param name="_name"></param>
        /// <param name="_description"></param>
        /// <param name="_dailyRentalCost"></param>
        /// <param name="_availableQuantity"></param>
        /// <param name="_equipmentStatus"></param>
        public Equipment(int _equipmentID, int _categoryID, string _name, string _description, double _dailyRentalCost, int _availableQuantity, string _equipmentStatus)
        {
            EquipmentID = _equipmentID;
            CategoryID = _categoryID;
            Name = _name;
            Description = _description;
            DailyRentalCost = _dailyRentalCost;
            AvailableQuantity = _availableQuantity;
            EquipmentStatus = _equipmentStatus;
        }

        /// <summary>
        /// Update the equipment to a new data
        /// </summary>
        /// <param name="_equipment"></param>
        public void UpdateEquipment(Equipment _equipment)
        {
			CategoryID = _equipment.CategoryID;
			Name = _equipment.Name;
			Description = _equipment.Description;
			DailyRentalCost = _equipment.DailyRentalCost;
			AvailableQuantity = _equipment.AvailableQuantity;
			EquipmentStatus = _equipment.EquipmentStatus;
		}

        /// <summary>
        /// Return a string of the equipment properties separated by a semicolon
        /// </summary>
        /// <returns></returns>
		public override string ToString()
		{
			return $"{EquipmentID};{CategoryID};{Name};{Description};{DailyRentalCost};{AvailableQuantity};{EquipmentStatus}";
		}
	}
}
