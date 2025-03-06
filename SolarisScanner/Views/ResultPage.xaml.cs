
using SolarisScanner.ViewModels;

namespace SolarisScanner.Views;

public partial class ResultPage : ContentPage
{
    private readonly ResultViewModel _viewModel;
    private readonly string _barcode;
    public ResultPage(string barcode)
    {
        InitializeComponent();
        _barcode = barcode;
        _viewModel = new ResultViewModel(Navigation);
        BindingContext = _viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await Task.Run(async () => await _viewModel.ProcessBarcode(_barcode));
    }
    
}