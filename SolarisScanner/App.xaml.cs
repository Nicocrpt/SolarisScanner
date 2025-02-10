using SolarisScanner.Views;

namespace SolarisScanner;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
        var userToken = SecureStorage.GetAsync("user_token").Result;
        if (!string.IsNullOrEmpty(userToken))
        {
            MainPage = new AppShell();
        }
        else
        {
            MainPage = new NavigationPage(new LoginPage());
        }

        
    }
}