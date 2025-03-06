using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Core.Platform;
using SolarisScanner.ViewModels;

namespace SolarisScanner.Views;

public partial class MainPage : ContentPage
{
    private readonly MainViewModel _viewModel;
    public MainPage()
    {
        InitializeComponent();
        _viewModel = new MainViewModel(Navigation);
        BindingContext = _viewModel;
    }

    protected override void OnAppearing()
    {
        StatusBar.SetStyle(StatusBarStyle.LightContent);
        StatusBar.SetColor(Colors.Black);
        base.OnAppearing();
    }
}