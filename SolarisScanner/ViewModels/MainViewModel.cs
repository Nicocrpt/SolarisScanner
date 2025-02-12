using CommunityToolkit.Mvvm.Input;
using SolarisScanner.Views;

namespace SolarisScanner.ViewModels;

public class MainViewModel : BaseViewModel
{
    private readonly INavigation _navigation;
    public IRelayCommand goToScanPageCommand { get; }

    public MainViewModel(INavigation navigation)
    {
        _navigation = navigation;
        goToScanPageCommand = new RelayCommand(async () => await _navigation.PushAsync(new ScanPage()));
    }
    
    
}