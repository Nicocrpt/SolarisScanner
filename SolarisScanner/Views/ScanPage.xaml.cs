
using BarcodeScanning;
using Microsoft.Maui.Controls.PlatformConfiguration;
using SolarisScanner.Models;
using SolarisScanner.ViewModels;
using ZXing.Net.Maui;

namespace SolarisScanner.Views;

public partial class ScanPage : ContentPage
{
    private readonly ScanViewModel _viewModel;
    public ScanPage()
    {
        InitializeComponent();
        // Camera.Options = new BarcodeReaderOptions
        // {
        //     AutoRotate = false,
        //     TryInverted = true,
        //     Multiple = false,
        //     Formats = ZXing.Net.Maui.BarcodeFormat.QrCode,
        // };
        
        
        _viewModel = new ScanViewModel(Navigation);
        BindingContext = _viewModel;
        Scanner.CameraEnabled = true;


        // Unloaded += (sender, e) =>
        // {
        //     Camera.IsEnabled = false;  // Désactivez la caméra mais ne la déconnectez pas
        // };

    }
    
    protected async override void OnAppearing()
    {
        base.OnAppearing();
        await Methods.AskForRequiredPermissionAsync();

        // Si BarcodeReader est activé, assure-toi qu'il est démarré

        Scanner.CameraEnabled = true;
        Scanner.PauseScanning = false;
        // Camera.IsEnabled = true;
        // Camera.IsDetecting = true;
    }
    
    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        Scanner.CameraEnabled = false;

        // Camera.IsDetecting = false;
        // Camera.IsEnabled = false;
    }
    
    private void ContentPage_Unloaded(object sender, EventArgs e)
    {
        Scanner.Handler?.DisconnectHandler();
    }
    
    // void CameraView_OnBarcodesDetected(object? sender, BarcodeDetectionEventArgs e)
    // {
    //     // Camera.IsDetecting = false;
    //     // if (e.Results != null && e.Results.Length > 0)
    //     // {
    //     //     Dispatcher.Dispatch( () =>
    //     //     {
    //     //         string code = e.Results[0].Value;
    //     //         _viewModel.ProcessBarcode(code);
    //     //     });
    //     //     
    //     // }
    // }

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
                _viewModel.ProcessBarcode(e.BarcodeResults.ElementAt(0).DisplayValue);
            });
        }
        else
        {
            Scanner.PauseScanning = false;
        }
    }
}