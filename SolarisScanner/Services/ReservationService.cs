using System.Globalization;
using Newtonsoft.Json.Linq;
using RestSharp;
using SolarisScanner.Models;

namespace SolarisScanner.Services;

public class ReservationService : IReservationService
{
    private readonly ApiClient _apiClient;

    public ReservationService()
    {
        _apiClient = new ApiClient();
    }

    public async Task<RestResponse> ProcessReservation(string reference)
    {
        try
        {
            string datetime  = DateTime.Now.ToLocalTime().ToString("yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture);
            var body = new { reference = reference, datetime = datetime };
            string? token = await SecureStorage.GetAsync("user_token");
            return await _apiClient.PostAsync<RestResponse>("reservation/check", body, token);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);

            throw;
        }
    }
}