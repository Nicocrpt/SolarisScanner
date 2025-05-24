
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
        statusColor = Color.FromRgb(255, 255, 255);
    }
    
    
    public async Task ProcessBarcode(string barcode)
    {
        try
        {
            IsLoading = true;
            
            RestResponse response = await _reservationService.ProcessReservation(barcode);
            JObject jsonResponse = JObject.Parse(response.Content);
            if ((int) jsonResponse["responseCode"] >= 2000)
            {
                Reservation = new Reservation(
                    jsonResponse["message"]?.ToString(),
                    jsonResponse["data"]["film"]["image"]?.ToString().Replace("original", "w500"),
                    jsonResponse["data"]["film"]["titre"]?.ToString(),
                    jsonResponse["data"]["salle"]?.ToString(),
                    jsonResponse["data"].Contains("places") ? jsonResponse["data"]["places"] + " Place(s)" : null,
                    jsonResponse["data"]["datetimeSeance"]?.ToString()
                );
            }
            else
            {
                Reservation = new Reservation(jsonResponse["message"]?.ToString());
            }

            switch ((int) jsonResponse["responseCode"])
            {
                case 3000:
                    StatusColor = Color.FromArgb("#fbc64b");
                    Icon = ImageSource.FromFile("fail.svg");
                    break;
                case 2000:
                    StatusColor = Color.FromArgb("#1DA747");
                    Icon = ImageSource.FromFile("check.svg");
                    break;
                default:
                    StatusColor = Color.FromArgb("#fb5d4b");
                    Icon = ImageSource.FromFile("fail.svg");
                    break;
            }
            
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