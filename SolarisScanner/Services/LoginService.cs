using System.Net;
using Newtonsoft.Json.Linq;
using RestSharp;
using SolarisScanner.Models;

namespace SolarisScanner.Services;

public class LoginService : ILoginService
{
    private readonly ApiClient _apiClient;
    
    public LoginService()
    {
        _apiClient = new ApiClient();
    }
    
    public async Task<User> LoginAsync(string username, string password)
    {
        try
        {
            var body = new { name = username, password = password };
            
            var response = await _apiClient.PostAsync<RestResponse>("getAuth", body);
            
            JObject jsonResponse = JObject.Parse(response.Content);
            var success = jsonResponse["success"]?.ToObject<bool>() ?? false;
            var token = jsonResponse["user"]?["token"]?.ToString();

            // if (response.StatusCode != HttpStatusCode.OK)
            // {
            //     throw new Exception($"Erreur HTTP: {response.StatusCode}");
            // }

            // Vérifier que le token est présent dans la réponse
            if (success && !string.IsNullOrEmpty(token))
            {
                return new User(token,jsonResponse["name"]?.ToString());
            }

            return null;
        }
        catch (Exception ex)
        {
            // Si une exception se produit, la relayer
            throw new Exception("Erreur de connexion : " + ex.Message);
        }
    }
}