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

    public async Task<Reservation> ProcessReservation(string reference)
    {
        try
        {
            var body = new { reference = reference };
            var response = await  _apiClient.PostAsync<RestResponse>("reservation/check", body, SecureStorage.GetAsync("user_token").Result);
            JObject jsonResponse = JObject.Parse(response.Content);
            if (jsonResponse.ContainsKey("success"))
            {
                return new Reservation(
                    jsonResponse["success"]?.ToString(),
                    jsonResponse["data"]["film"]["image"]?.ToString(),
                    jsonResponse["data"]["film"]["titre"]?.ToString(),
                    jsonResponse["data"]["salle"]?.ToString()
                );
            }
            else
            {
                return new Reservation(jsonResponse["error"]?.ToString());
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);

            throw;
        }
    }
}