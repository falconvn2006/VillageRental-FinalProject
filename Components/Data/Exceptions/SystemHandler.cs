using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VillageRental.Components.Data.Exceptions
{
	class SystemHandler : Exception
	{
        public int ErrorCode { get; set; }
        public string Message { get; set; }

        public SystemHandler() : base() { }

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
			Application.Current.MainPage.DisplayAlert("Error encounter!", $"{ErrorCode}: {Message}", "OK");
		}
	}
}
