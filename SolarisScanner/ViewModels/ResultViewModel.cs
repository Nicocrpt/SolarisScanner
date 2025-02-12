using CommunityToolkit.Mvvm.ComponentModel;
using SolarisScanner.Models;

namespace SolarisScanner.ViewModels;

public partial class ResultViewModel: BaseViewModel
{
    [ObservableProperty] 
    private Reservation reservation;
    
    public string Status => reservation.Status;


    public ResultViewModel(Reservation reservation)
    {
        Reservation = reservation;
    }
}