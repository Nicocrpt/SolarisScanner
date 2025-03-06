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
   int thumbPositionX;
   
   [ObservableProperty] 
   int flashPositionX;
   
   [ObservableProperty]
   private Color backgroundSwitchColor;
   
   [ObservableProperty]
   private Color backgroundFlashColor;
   
   [ObservableProperty]
   private Color switchColor;
   
   [ObservableProperty]
   private Color flashColor;
   

   public ScanViewModel(INavigation navigation)
   {
      IsBusy = true;
      _navigation = navigation;
      _reservationService = new ReservationService();
   }
   
   
   public void ToggleSwitch()
   {
      // Inverser l'état du switch
      IsBusy = !IsBusy;
        
      // Définir les nouvelles valeurs de couleur et de position
      ThumbPositionX = !IsBusy ? 19 : 0;
      BackgroundSwitchColor = !IsBusy ? Colors.DodgerBlue : Color.FromArgb("#272727");
      SwitchColor = !IsBusy ? Colors.White : Color.FromArgb("#ABABAB");
   }

   public void ToggleFlash()
   {
      FlashPositionX = FlashPositionX == 19 ? 0 : 19;
      BackgroundFlashColor = BackgroundFlashColor == Colors.Gold ? Color.FromArgb("#272727") : Colors.Gold;
      FlashColor = FlashColor == Colors.White ? Color.FromArgb("#ABABAB") : Colors.White;
   }
}