using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Newtonsoft.Json;
using SolarisScanner.Models;
using SolarisScanner.Services;
using SolarisScanner.Views;

namespace SolarisScanner.ViewModels;

public partial class ScanViewModel : BaseViewModel
{
   private readonly IReservationService _reservationService;
   private readonly INavigation _navigation;
   
   [ObservableProperty]
   private bool isLoading;

   [ObservableProperty] 
   string imageSource;
   

   public ScanViewModel(INavigation navigation)
   {
      _navigation = navigation;
      isLoading = true;
      _reservationService = new ReservationService();
      imageSource = "pausescan.svg";
   }
   
}