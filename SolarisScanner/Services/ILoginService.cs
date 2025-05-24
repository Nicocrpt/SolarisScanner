using Newtonsoft.Json.Linq;
using RestSharp;
using SolarisScanner.Models;

namespace SolarisScanner.Services;

public interface ILoginService
{
    
    Task<RestResponse> LoginAsync(string email, string password);
}