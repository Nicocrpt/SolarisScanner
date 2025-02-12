using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Newtonsoft.Json;
using SolarisScanner.Models;
using SolarisScanner.Services;
using SolarisScanner.Views;

namespace SolarisScanner.ViewModels;

public class ScanViewModel : BaseViewModel
{
   private readonly IReservationService _reservationService;
   private readonly INavigation _navigation;

   public ScanViewModel(INavigation navigation)
   {
      _navigation = navigation;
      _reservationService = new ReservationService();
   }

   public async void ProcessBarcode(string barcode)
   {
      Reservation reservation = await _reservationService.ProcessReservation(barcode);
      await _navigation.PushAsync(new ResultPage(reservation));
      
      
   }
}