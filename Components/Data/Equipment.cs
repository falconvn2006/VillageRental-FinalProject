using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public string EquipmentStatus { get; set; }
        private bool inInventory;
        private bool isSold;

        public Equipment(int _equipmentID, int _categoryID, string _name, string _description, double _dailyRentalCost, int _availableQuantity, string _equipmentStatus)
        {
            EquipmentID = _equipmentID;
            CategoryID = _categoryID;
            Name = _name;
            Description = _description;
            DailyRentalCost = _dailyRentalCost;
            AvailableQuantity = _availableQuantity;
            EquipmentStatus = _equipmentStatus;

            inInventory = true;
            isSold = false;
        }

        public void SetInInventory(bool _inInventory)
        {
            inInventory = _inInventory;
        }

        public void SetIsSold(bool _isSold)
        {
            isSold = _isSold;
        }
    }
}
