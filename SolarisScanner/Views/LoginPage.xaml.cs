using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Core.Platform;
using SolarisScanner.Services;
using SolarisScanner.ViewModels;

namespace SolarisScanner.Views;

public partial class LoginPage : ContentPage
{
    readonly LoginViewModel _viewModel;
    public LoginPage()
    {
        InitializeComponent();
        _viewModel = new LoginViewModel();
        BindingContext = _viewModel;
    }

    protected override void OnAppearing()
    {
        StatusBar.SetStyle(StatusBarStyle.DarkContent);
        base.OnAppearing();
        
    }
}