namespace VillageRental.Components.Data
{
    public class RentalInformation
    {
        public int RentalID { get; set; }
        public DateTime CurrentDate { get; set; }
        public int CustomerID { get; set; }
        public string CustomerLastName { get; set; }
        public List<RentalItem> rentalItemList;
        public string RentalStatus { get; set; }
        public double TotalCostToRent { get; set; } = 0;

        public RentalInformation(int _rentalID, DateTime _currentDate, int _customerID, string _customerLastName, string _rentalStatus)
        {
            RentalID = _rentalID;
            CurrentDate = _currentDate;
            CustomerID = _customerID;
            CustomerLastName = _customerLastName;
            RentalStatus = _rentalStatus;
            rentalItemList = new List<RentalItem>();
        }

        public void UpdateRentalInformation(RentalInformation newRentalInformation, List<RentalItem> newRentalItemList)
        {
            CustomerID = newRentalInformation.CustomerID;
            CurrentDate = newRentalInformation.CurrentDate;
            CustomerLastName = newRentalInformation.CustomerLastName;
            RentalStatus = newRentalInformation.RentalStatus;
            rentalItemList.Clear();

            foreach (RentalItem item in newRentalItemList)
            {
                AddRentalItem(item);
            }
        }

        public void AddRentalItem(RentalItem _item)
        {
            rentalItemList.Add(_item);
            TotalCostToRent += _item.CostOfRental;
        }

        public void RemoveRentalItem(RentalItem _item)
        {
            rentalItemList.Remove(_item);
        }
    }
}
