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

    
}