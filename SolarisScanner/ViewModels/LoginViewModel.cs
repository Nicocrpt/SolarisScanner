
using System.Net;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json.Linq;
using RestSharp;
using SolarisScanner.Models;
using SolarisScanner.Services;
using SolarisScanner.Views;
using Debug = System.Diagnostics.Debug;

namespace SolarisScanner.ViewModels;

public partial class LoginViewModel : BaseViewModel
{
    private readonly ILoginService _loginService;
    private string? _errorMessage;
    public string? ErrorMessage
    {
        get => _errorMessage;
        set
        {
            _errorMessage = value;
            OnPropertyChanged(); 
        }
    }
    public bool HasError => !string.IsNullOrEmpty(ErrorMessage);

    [ObservableProperty] 
    string? _username;
    
    [ObservableProperty]
    string? _password;
    
    public IRelayCommand LoginCommand { get; }
    
    public LoginViewModel()
    {
        _loginService = new LoginService();
        LoginCommand = new RelayCommand(async () => await LoginAsync());
    }

    private async Task LoginAsync()
    {
        IsBusy = true;
        try
        {
            RestResponse response = await _loginService.LoginAsync(Username, Password);

            if (!response.IsSuccessful)
            {
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    ErrorMessage = "Nom d'utilisateur ou mot de passe incorrect.";
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Une erreur est survenue", response.ErrorMessage, "OK");
                }
            }
            else
            {
                JObject jsonResponse = JObject.Parse(response.Content);
                
                User user = new User(jsonResponse["user"]?["token"]?.ToString() ?? "", jsonResponse["user"]?["name"]?.ToString() ?? "");
                await SecureStorage.SetAsync("user_token", user.token);
                await SecureStorage.SetAsync("current_timestamp", DateTime.Now.ToString("o"));
                Application.Current.MainPage = new NavigationPage(new ScanPage());
            }
        }
        catch (Exception ex)
        {
            
            throw new Exception(ex.Message);
        }
        finally
        {
            IsBusy = false;
        }
    }

}