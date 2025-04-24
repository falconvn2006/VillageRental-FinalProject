namespace VillageRental.Components.Data
{
    public class RentalInformation
    {
        public int RentalID { get; set; }
        /// <summary>
        /// The date that rental information is created
        /// </summary>
        public DateTime CurrentDate { get; set; }
        public int CustomerID { get; set; }
        public string CustomerLastName { get; set; }
        public List<RentalItem> rentalItemList;
        public string RentalStatus { get; set; }
        public double TotalCostToRent { get; set; } = 0;

        /// <summary>
        /// Constructor for creating the Rental Information object
        /// </summary>
        /// <param name="_rentalID"></param>
        /// <param name="_currentDate"></param>
        /// <param name="_customerID"></param>
        /// <param name="_customerLastName"></param>
        /// <param name="_rentalStatus"></param>
        public RentalInformation(int _rentalID, DateTime _currentDate, int _customerID, string _customerLastName, string _rentalStatus)
        {
            RentalID = _rentalID;
            CurrentDate = _currentDate;
            CustomerID = _customerID;
            CustomerLastName = _customerLastName;
            RentalStatus = _rentalStatus;
            rentalItemList = new List<RentalItem>();
        }

        /// <summary>
        /// Update the rental information with a new information and a new list of rental item
        /// </summary>
        /// <param name="newRentalInformation"></param>
        /// <param name="newRentalItemList"></param>
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

        /// <summary>
        /// Add a new rental item to the list and add the cost of rent to the total cost
        /// </summary>
        /// <param name="_item"></param>
        public void AddRentalItem(RentalItem _item)
        {
            rentalItemList.Add(_item);
            TotalCostToRent += _item.CostOfRental;
        }

        /// <summary>
        /// Remove the rental item from the rental information then subtract the rental cost
        /// </summary>
        /// <param name="_item"></param>
        public void RemoveRentalItem(RentalItem _item)
        {
            bool removeSuccesful = rentalItemList.Remove(_item);
            if(removeSuccesful)
                TotalCostToRent -= _item.CostOfRental;
        }
    }
}
