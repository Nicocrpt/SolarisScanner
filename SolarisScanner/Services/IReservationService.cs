using SolarisScanner.Models;

namespace SolarisScanner.Services;

public interface IReservationService
{
    Task<Reservation> ProcessReservation(string reference);
}