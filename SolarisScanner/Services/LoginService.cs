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
    
    public async Task<RestResponse> LoginAsync(string username, string password)
    {
        try
        {
            var body = new { name = username, password = password };
            return await _apiClient.PostAsync<RestResponse>("getAuth", body);
        }
        catch (Exception ex)
        {

            throw new Exception("Erreur de connexion : " + ex.Message);
        }
    }
}