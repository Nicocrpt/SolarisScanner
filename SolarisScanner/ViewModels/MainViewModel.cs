using CommunityToolkit.Mvvm.Input;
using SolarisScanner.Views;

namespace SolarisScanner.ViewModels;

public class MainViewModel : BaseViewModel
{
    private readonly INavigation _navigation;
    public IRelayCommand GoToScanPageCommand { get; }

    public MainViewModel(INavigation navigation)
    {
        _navigation = navigation;
        GoToScanPageCommand = new RelayCommand( () => _navigation.PushAsync(new ScanPage()));
    }
    
    
}