
using System.Diagnostics;
using BarcodeScanning;
using Microsoft.Maui.Controls.PlatformConfiguration;
using SolarisScanner.Models;
using SolarisScanner.ViewModels;

namespace SolarisScanner.Views;

public partial class ScanPage : ContentPage
{
    private readonly ScanViewModel _viewModel;
    
    private bool _isScanning;
    public ScanPage()
    {
        InitializeComponent();
        _isScanning = true;
        BindingContext = _viewModel;
        _viewModel = new ScanViewModel(Navigation);
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
        if (_isScanning)
        {
            Scanner.PauseScanning = true;
            if (e.BarcodeResults.Count > 0)
            {

                string code = e.BarcodeResults.ElementAt(0).DisplayValue;
                if (SecureStorage.GetAsync("barcode").Result == null)
                {
                    if (DeviceInfo.Platform == DevicePlatform.Android)
                    {
                        Vibration.Vibrate(TimeSpan.FromMilliseconds(100));
                    }
                    SecureStorage.SetAsync("barcode", code);
            
                    Dispatcher.Dispatch(async () =>
                    {
                        await Navigation.PushAsync(new ResultPage(code));
                        SecureStorage.Remove("barcode");
                    });
                }
            
            }
            else
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await Task.Delay(500);
                    Scanner.PauseScanning = false;
                });
            }
        }
    }
    

    async void Button_OnClicked(object? sender, EventArgs e)
    {
        _isScanning = !_isScanning;
        ToggleScanButton.ImageSource = _isScanning ? "pausescan.svg" : "playscan.svg";
        ToggleScanButton.BackgroundColor = _isScanning ? Colors.DarkOrange : Colors.OliveDrab;
    }
}