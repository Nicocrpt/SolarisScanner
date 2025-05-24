
using BarcodeScanning;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Core.Platform;
using SolarisScanner.Tools;
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
        _viewModel = new ScanViewModel(Navigation);
        BindingContext = _viewModel;
        Scanner.CameraEnabled = true;
        
        _viewModel.PropertyChanged += ViewModel_PropertyChanged;

    }
    
    protected async override void OnAppearing()
    {
        base.OnAppearing();
        await Methods.AskForRequiredPermissionAsync();
        Scanner.CameraEnabled = true;
        Scanner.PauseScanning = false;
        StatusBar.SetStyle(StatusBarStyle.LightContent);
    }
    
    
    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        Scanner.CameraEnabled = false;
    }

    void CameraView_OnOnDetectionFinished(object? sender, OnDetectionFinishedEventArg e)
    {
        if (_viewModel.IsBusy) { return; }

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
    
    
    async void TapGestureRecognizer_OnTapped(object? sender, TappedEventArgs e)
    {
        ToggleScanButton.IsEnabled = !ToggleScanButton.IsEnabled;
        if (!ToggleScanButton.IsEnabled)
        {
            Transitions.AnimateOpacityChange(ToggleScanButton, ToggleScanButton.Opacity, 0.18, 250, Easing.CubicInOut);
            // Transitions.AnimateBackgroundColorChange(ToggleScanButton, ToggleScanButton.BackgroundColor, Colors.LightGray, 300, Easing.Linear);
        }
        else
        {
            Transitions.AnimateOpacityChange(ToggleScanButton, ToggleScanButton.Opacity, 1, 300, Easing.CubicInOut);
            // Transitions.AnimateBackgroundColorChange(ToggleScanButton, ToggleScanButton.BackgroundColor, Colors.LightGray, 300, Easing.Linear);
        }
        
        Vibration.Vibrate(TimeSpan.FromMilliseconds(3));
         _viewModel.ToggleSwitch();
         await Task.Delay(160);
         ThumbImage.Source = (ThumbImage.Source as FileImageSource)?.File == "autologooff.svg" ? "autologoon.svg" : "autologooff.svg";
         Vibration.Vibrate(TimeSpan.FromMilliseconds(8));
        
    }

    async void OnFlashTapped(object? sender, EventArgs e)
    {
        Scanner.TorchOn = !Scanner.TorchOn;

        Vibration.Vibrate(TimeSpan.FromMilliseconds(3));
        _viewModel.ToggleFlash();
        await Task.Delay(160);
        FlashImage.Source = (FlashImage.Source as FileImageSource)?.File == "flashoff.svg" ? "flashon.svg" : "flashoff.svg";
        Vibration.Vibrate(TimeSpan.FromMilliseconds(8));
    }
    
    private async void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(_viewModel.ThumbPositionX))
        {
            await Transitions.AnimateTranslationAsync(Thumb, _viewModel.ThumbPositionX, 0, 200, Easing.CubicInOut);
        }

        if (e.PropertyName == nameof(_viewModel.BackgroundSwitchColor))
        {
            Transitions.AnimateBackgroundColorChange(SwitchFrame, SwitchFrame.BackgroundColor, _viewModel.BackgroundSwitchColor, 200, Easing.CubicInOut);
        }

        if (e.PropertyName == nameof(_viewModel.SwitchColor))
        {
            Transitions.AnimateBackgroundColorChange(Thumb, Thumb.BackgroundColor, _viewModel.SwitchColor, 200, Easing.CubicInOut);
        }
        
        
        //FLASH SWITCH
        if (e.PropertyName == nameof(_viewModel.FlashPositionX))
        {
            await Transitions.AnimateTranslationAsync(FlashThumb, _viewModel.FlashPositionX, 0, 200, Easing.CubicInOut);
        }

        if (e.PropertyName == nameof(_viewModel.FlashColor))
        {
            Transitions.AnimateBackgroundColorChange(FlashThumb, FlashThumb.BackgroundColor, _viewModel.FlashColor, 200, Easing.CubicInOut);
        }

        if (e.PropertyName == nameof(_viewModel.BackgroundFlashColor))
        {
            Transitions.AnimateBackgroundColorChange(FlashFrame, FlashFrame.BackgroundColor, _viewModel.BackgroundFlashColor, 200, Easing.CubicInOut);
        }
    }

    async void ToggleScanButton_OnPressed(object? sender, EventArgs e)
    {
        if (!_viewModel.IsBusy) { return; }
        Transitions.AnimateBackgroundColorChange(ToggleScanButton, ToggleScanButton.BackgroundColor, Colors.White, 200, Easing.CubicInOut );
        await Task.Delay(100);
        Vibration.Vibrate(TimeSpan.FromMilliseconds(5));
        _viewModel.IsBusy = false;
    }

    void ToggleScanButton_OnReleased(object? sender, EventArgs e)
    {
        Vibration.Vibrate(TimeSpan.FromMilliseconds(3));
        Transitions.AnimateBackgroundColorChange(ToggleScanButton, ToggleScanButton.BackgroundColor, Colors.LightGray, 100, Easing.CubicInOut );
        _viewModel.IsBusy = true;
    }
}
