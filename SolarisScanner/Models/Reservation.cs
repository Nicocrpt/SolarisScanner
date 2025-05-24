using System.Globalization;

namespace SolarisScanner.Models;

public class Reservation
{
    public string? Status { get; set; }
    public string? MoviePoster { get; set; }
    public string? MovieTitle { get; set; }
    public string? SessionDatetime { get; set; }
    public string? SessionRoom { get; set; }
    
    public string? NbPlaces { get; set; }
    
    //Constructeur
    public Reservation(string status, string moviePoster = "", string movieTitle = "", string sessionRoom = "", string nbPlaces = "", string sessionDatetime = "")
    {
        this.Status = status;
        this.MoviePoster = moviePoster;
        this.MovieTitle = movieTitle;
        this.SessionRoom = sessionRoom;
        this.NbPlaces = nbPlaces;
        
        try {
            var dto = DateTimeOffset.Parse(sessionDatetime, null, DateTimeStyles.RoundtripKind);
            this.SessionDatetime = dto.ToString("dd/MM/yyyy HH\\hmm", CultureInfo.InvariantCulture);
        }
        catch {
            this.SessionDatetime = null;
        }
    }
}