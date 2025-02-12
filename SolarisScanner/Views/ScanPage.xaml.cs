
using BarcodeScanning;
using Microsoft.Maui.Controls.PlatformConfiguration;
using SolarisScanner.Models;
using SolarisScanner.ViewModels;

namespace SolarisScanner.Views;

public partial class ScanPage : ContentPage
{
    private readonly ScanViewModel _viewModel;
    public ScanPage()
    {
        InitializeComponent();
        _viewModel = new ScanViewModel(Navigation);
        BindingContext = _viewModel;
        Scanner.CameraEnabled = true;
    }
    
    protected async override void OnAppearing()
    {
        base.OnAppearing();
        await Methods.AskForRequiredPermissionAsync();
        _viewModel.IsLoading = false;
        Scanner.CameraEnabled = true;
        Scanner.PauseScanning = false;
    }
    
    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        Scanner.CameraEnabled = false;
    }

    void CameraView_OnOnDetectionFinished(object? sender, OnDetectionFinishedEventArg e)
    {   
        Scanner.PauseScanning = true;
        if (e.BarcodeResults.Count > 0)
        {
            if (DeviceInfo.Platform == DevicePlatform.Android)
            {
                // Appelle directement la vibration sans classe supplémentaire
                Vibration.Vibrate(TimeSpan.FromMilliseconds(100));
            }
            
            Dispatcher.Dispatch(() =>
            {
                Navigation.PushAsync(new ResultPage(e.BarcodeResults.ElementAt(0).DisplayValue));
            });
        }
        else
        {
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await Task.Delay(1500);  // Attend 500 ms avant de reprendre la détection
                Scanner.PauseScanning = false;  // Reprendre la détection
            });
        }
    }
}