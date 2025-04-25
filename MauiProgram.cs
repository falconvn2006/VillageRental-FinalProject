using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using VillageRental.Components.Instances;

namespace VillageRental
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            string? databaseAddress = string.Empty;
			string? databaseUsername = string.Empty;
			string? databasePassword = string.Empty;
			string? databaseName = string.Empty;

			try
            {
				IConfigurationRoot config = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json")
                    .Build();

				databaseAddress = config["DATABASE_SERVER_ADDRESS"];
				databaseUsername = config["USERNAME"];
				databasePassword = config["PASSWORD"];
				databaseName = config["DATABASE_NAME"];
			}
            catch
            {
                
            }

			builder.Services.AddMauiBlazorWebView();
            builder.Services.AddSingleton<SystemManagement>();

			builder.Services.AddSingleton((db) => new DatabaseManager(databaseAddress, databaseUsername, databasePassword, databaseName));

#if DEBUG
			builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
