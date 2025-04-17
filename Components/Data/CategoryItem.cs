namespace VillageRental.Components.Data
{
    public class CategoryItem
    {
        public int CategoryID { get; set; }
        public string Description { get; set; }

        public CategoryItem(int _categoryID, string _description)
        {
            CategoryID = _categoryID;
            Description = _description;
        }

		public override string ToString()
		{
			return $"{CategoryID};{Description}";
		}
	}
}
