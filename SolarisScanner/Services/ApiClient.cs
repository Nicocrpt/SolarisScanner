using RestSharp;

namespace SolarisScanner.Services;

public class ApiClient
{
    RestClient Client;

    public ApiClient()
    {
        Client = new RestClient("https://cinemasolaris.fr/api");
    }
    
    public async Task<RestResponse<T>> PostAsync<T>(string endpoint, object body, string token = null)
    {
        try
        {
            var request = new RestRequest(endpoint, Method.Post);
            request.AddJsonBody(body);
            
            if (!string.IsNullOrEmpty(token))
            {
                request.AddHeader("Authorization", $"Bearer {token}");
            }

            var response = await Client.ExecuteAsync<T>(request);
            return response;
        }
        catch (Exception ex)
        {
            throw new Exception($"Erreur lors de la requête: {ex.Message}");
        }
    }
}