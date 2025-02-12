using SolarisScanner.Models;

namespace SolarisScanner.ViewModels;

public class ResultViewModel: BaseViewModel
{
    private readonly Reservation _reservation;

    public ResultViewModel(Reservation reservation)
    {
        _reservation = reservation;
    }
}