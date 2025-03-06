
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json.Linq;
using RestSharp;
using SolarisScanner.Models;
using SolarisScanner.Services;

namespace SolarisScanner.ViewModels;

public partial class ResultViewModel: BaseViewModel
{
    private readonly IReservationService _reservationService;
    
    [ObservableProperty]
    private Reservation reservation;
    
    [ObservableProperty]
    private ImageSource icon;
    
    [ObservableProperty]
    private ImageSource ageLogo = ImageSource.FromFile("twelvelogo.svg");

    [ObservableProperty] 
    Color statusColor;
    
    private INavigation _navigation;
    
    [ObservableProperty]
    private bool isLoading = true;

    public ResultViewModel(INavigation navigation)
    {
        IsLoading = true;
        _navigation = navigation;
        _reservationService = new ReservationService();
    }
    
    
    public async Task ProcessBarcode(string barcode)
    {
        try
        {
            IsLoading = true;
            
            RestResponse response = await _reservationService.ProcessReservation(barcode);
            JObject jsonResponse = JObject.Parse(response.Content);
            if (jsonResponse.ContainsKey("success"))
            {
                Reservation = new Reservation(
                    jsonResponse["success"]?.ToString(),
                    jsonResponse["data"]["film"]["image"]?.ToString().Replace("original", "w500"),
                    jsonResponse["data"]["film"]["titre"]?.ToString(),
                    jsonResponse["data"]["salle"]?.ToString(),
                    jsonResponse["data"]["places"] + " Places"
                );
            }
            else
            {
                Reservation = new Reservation(jsonResponse["error"]?.ToString());
            }
            
            StatusColor = !Reservation.Status.Contains("déjà") ? Color.FromArgb("#1DA747") : Color.FromArgb("#fb5d4b");
            Icon = !Reservation.Status.Contains("déjà")? ImageSource.FromFile("check.svg") : ImageSource.FromFile("fail.svg");
            IsLoading = false;
        }
        catch (Exception ex)
        {
            await MainThread.InvokeOnMainThreadAsync(async () =>
            {
               await Application.Current.MainPage.DisplayAlert("Une erreur est survenue", ex.Message, "OK");
                
               await _navigation.PopAsync();
                
            });
            IsLoading = false;
        }
    }
}