
using SolarisScanner.ViewModels;
using ZXing.Net.Maui;

namespace SolarisScanner.Views;

public partial class ScanPage : ContentPage
{
    private readonly ScanViewModel _viewModel;
    public ScanPage()
    {
        InitializeComponent();
        Camera.Options = new BarcodeReaderOptions
        {
            AutoRotate = false,
            TryInverted = true,
            Multiple = false,
            Formats = ZXing.Net.Maui.BarcodeFormat.QrCode,
        };
        _viewModel = new ScanViewModel(Navigation);
        BindingContext = _viewModel;
        
        
        Unloaded += (sender, e) =>
        {
            Camera.IsEnabled = false;  // Désactivez la caméra mais ne la déconnectez pas
        };

    }
    
    protected override void OnAppearing()
    {
        base.OnAppearing();

        // Si BarcodeReader est activé, assure-toi qu'il est démarré

        Camera.IsEnabled = true;
        Camera.IsDetecting = true;
    }
    
    protected override void OnDisappearing()
    {
        base.OnDisappearing();

        // Arrêter la détection et libérer les ressources
        Camera.IsDetecting = false;
        Camera.IsEnabled = false;
    }
    
    void CameraView_OnBarcodesDetected(object? sender, BarcodeDetectionEventArgs e)
    {
        Camera.IsDetecting = false;
        if (e.Results != null && e.Results.Length > 0)
        {
            Dispatcher.Dispatch( () =>
            {
                string code = e.Results[0].Value;
                _viewModel.ProcessBarcode(code);
            });
            
        }
    }
}