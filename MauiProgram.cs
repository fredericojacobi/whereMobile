using WhereMobile.Services;
using WhereMobile.ViewModels;
using WhereMobile.Views;

namespace WhereMobile;

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
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
				fonts.AddFont("Proletarsk.ttf", "Proletarsk");
			});

        builder.Services.AddSingleton<IConnectivity>(Connectivity.Current);
        builder.Services.AddSingleton<IGeolocation>(Geolocation.Default);
        builder.Services.AddSingleton<IMap>(Map.Default);

        builder.Services.AddSingleton<EventService>();
        builder.Services.AddSingleton<UserService>();

        builder.Services.AddSingleton<EventsViewModel>();
        builder.Services.AddTransient<EventDetailsViewModel>();
        builder.Services.AddTransient<UserDetailsViewModel>();

        builder.Services.AddSingleton<EventsPage>();
        builder.Services.AddSingleton<HomePage>();
		builder.Services.AddTransient<EventDetailsPage>();
		builder.Services.AddTransient<UserDetailsPage>();

        return builder.Build();
	}
}
