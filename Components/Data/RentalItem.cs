using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VillageRental.Components.Data
{
    public class RentalItem
    {
        public int EquipmentID { get; set; }
        public DateTime RentalDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public double CostOfRental { get; set; }
        public int Quantity { get; set; }

        public RentalItem(int _equipmentID, DateTime _rentalDate, DateTime _returnDate, double _costOfRental, int _quantity)
        {
            EquipmentID = _equipmentID;
            RentalDate = _rentalDate;
            ReturnDate = _returnDate;
            CostOfRental = _costOfRental;
            Quantity = _quantity;
        }
    }
}
