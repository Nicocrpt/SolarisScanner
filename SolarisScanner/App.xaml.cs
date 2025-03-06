using SolarisScanner.Views;


namespace SolarisScanner;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
        
        
        var userToken = SecureStorage.GetAsync("user_token").Result;
        var timestamp = SecureStorage.GetAsync("current_timestamp").Result;
        bool isIdentified = false;
        
        if (!string.IsNullOrEmpty(userToken) && !string.IsNullOrEmpty(timestamp))
        {
            DateTime savedDate = DateTime.Parse(timestamp);
            TimeSpan difference = DateTime.Now.ToLocalTime() - savedDate;
            
            isIdentified = difference.TotalHours < 1 ? true : false;
        }

        if (isIdentified)
        {
            MainPage = new NavigationPage(new ScanPage());
        }
        else
        {
            MainPage = new LoginPage();
        }
    }
}