using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SolarisScanner.Services;
using SolarisScanner.ViewModels;

namespace SolarisScanner.Views;

public partial class LoginPage : ContentPage
{
    private readonly LoginViewModel _viewModel;
    
    public LoginPage()
    {
        InitializeComponent();
        _viewModel = new LoginViewModel();
        BindingContext = _viewModel;
    }
    
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        
        // Vérifie si un utilisateur est déjà connecté
        await _viewModel.CheckForExistingUser();
    }
}