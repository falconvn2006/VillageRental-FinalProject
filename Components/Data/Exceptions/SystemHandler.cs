namespace VillageRental.Components.Data.Exceptions
{
	class SystemHandler : Exception
	{
        public int ErrorCode { get; set; }
        public string Message { get; set; }

        public SystemHandler() : base() { }

		public SystemHandler(string message) : base(message)
		{
			Message = message;
		}

		public SystemHandler(int errorCode, string message) : base(message) 
		{
			ErrorCode = errorCode;
			Message = message;
		}

		public SystemHandler(int errorCode, string message,  Exception innerException) : base(message, innerException) 
		{ 
			ErrorCode = errorCode;
			Message = message;
		}

		public void DisplayError()
		{
			if(ErrorCode != 0)
				Application.Current.MainPage.DisplayAlert("Error encounter!", $"{ErrorCode}: {Message}", "OK");
			else
				Application.Current.MainPage.DisplayAlert("Error encounter!", $"{Message}", "OK");
		}
	}
}
