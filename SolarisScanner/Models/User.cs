namespace SolarisScanner.Models;

public class User
{
   public string? token { get; set; }
   public string? name {get; set;}

   public User(string token, string name)
   {
      this.token = token;
      this.name = name;
   }
}