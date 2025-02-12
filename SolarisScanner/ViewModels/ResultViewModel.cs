using CommunityToolkit.Mvvm.ComponentModel;
using SolarisScanner.Models;
using SolarisScanner.Services;

namespace SolarisScanner.ViewModels;

public partial class ResultViewModel: BaseViewModel
{
    private readonly IReservationService _reservationService;
    
    [ObservableProperty]
    private Reservation reservation;

    [ObservableProperty] 
    Color statusColor;
    
    [ObservableProperty]
    private bool isLoading;

    public ResultViewModel(INavigation navigation)
    {
        _reservationService = new ReservationService();
    }
    
    
    public async Task ProcessBarcode(string barcode)
    {
        try
        {
            IsLoading = true;
            Reservation result = await _reservationService.ProcessReservation(barcode);
            Reservation = result;
            StatusColor = !result.Status.Contains("déjà") ? Colors.Green : Colors.Red;
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
        }
        finally
        {
            IsLoading = false;
        }
        
        
    }
}