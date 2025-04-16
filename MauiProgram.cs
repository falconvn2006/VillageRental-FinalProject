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

            IConfigurationRoot config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            builder.Services.AddMauiBlazorWebView();
            builder.Services.AddSingleton<SystemManagement>();

			string? databaseAddress = config["DATABASE_SERVER_ADDRESS"];
			string? databaseUsername = config["USERNAME"];
			string? databasePassword = config["PASSWORD"];
			string? databaseName = config["DATABASE_NAME"];

			builder.Services.AddSingleton((db) => new DatabaseManager(databaseAddress, databaseUsername, databasePassword, databaseName));

#if DEBUG
    		builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
