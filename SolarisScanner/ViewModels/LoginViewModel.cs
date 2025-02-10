
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SolarisScanner.Models;
using SolarisScanner.Services;
using Debug = System.Diagnostics.Debug;

namespace SolarisScanner.ViewModels;

public partial class LoginViewModel : BaseViewModel
{
    private readonly ILoginService _loginService;

    [ObservableProperty] 
    string _username;
    
    [ObservableProperty]
    string _password;
    
    public IRelayCommand LoginCommand { get; }
    
    public LoginViewModel()
    {
        _loginService = new LoginService();
        LoginCommand = new RelayCommand(async () => await LoginAsync());
    }

    public async Task LoginAsync()
    {
        Console.WriteLine("hello");
        IsBusy = true; // Début de l'opération
        try
        {
            // Appel à LoginService pour tenter la connexion
            User user = await _loginService.LoginAsync(Username, Password); // On récupère un objet User

            // Logique après une connexion réussie (par exemple, stocker le token)
            if (user != null && !string.IsNullOrEmpty(user.token))
            {
                // Tu peux aussi stocker le token dans un service ou un store pour l'utiliser plus tard
                // Exemple : _tokenService.SaveToken(user.Token);
                await SecureStorage.SetAsync("user_token", user.token);
                
                Application.Current.MainPage = new AppShell();
            }
            else
            {
                HandleError("Erreur : Impossible de récupérer le token.");
                await Application.Current.MainPage.DisplayAlert("Erreur", "Nom d'utilisateur ou mot de passe incorrect.", "OK");
            }
        }
        catch (Exception ex)
        {
            HandleError("Erreur lors de la connexion : " + ex.Message);
        }
        finally
        {
            IsBusy = false; // Fin de l'opération
        }
    }
    
    private void HandleError(string errorMessage)
    {
        // Tu peux ajouter un message d'erreur à afficher dans l'interface ou journaliser l'erreur
        Title = errorMessage;  // Exemple de mise à jour du titre avec le message d'erreur

        // Si tu veux avoir un message plus détaillé dans une autre propriété, tu peux ajouter une nouvelle propriété Observable
        // par exemple, une propriété "ErrorMessage" pour l'afficher dans la vue
    }
    
    public async Task CheckForExistingUser()
    {
        var token = await SecureStorage.GetAsync("user_token");

        if (!string.IsNullOrEmpty(token))
        {
            // Token trouvé, utilisateur déjà connecté
            Title = "Utilisateur déjà connecté";
            // Ici, tu peux également appeler une méthode pour récupérer l'utilisateur et son profil
            // Exemple : await GetUserData(token);
        }
        else
        {
            // Pas de token, l'utilisateur doit se connecter
            Title = "Veuillez vous connecter";
        }
    }
}