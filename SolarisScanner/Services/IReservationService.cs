using RestSharp;
using SolarisScanner.Models;

namespace SolarisScanner.Services;

public interface IReservationService
{
    Task<RestResponse> ProcessReservation(string reference);
}