using SolarisScanner.Models;

namespace SolarisScanner.Services;

public interface ILoginService
{
    
    Task<User> LoginAsync(string username, string password);
}