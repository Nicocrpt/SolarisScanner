using Microsoft.Extensions.Logging;
using SolarisScanner.Services;
using SolarisScanner.ViewModels;
using SolarisScanner.Views;

namespace SolarisScanner;

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
            });

        builder.Services.AddSingleton<LoginPage>();
        builder.Services.AddSingleton<ApiClient>();
        builder.Services.AddSingleton<ILoginService, LoginService>();
        builder.Services.AddSingleton<LoginViewModel>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}