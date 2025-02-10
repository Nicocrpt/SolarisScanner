namespace SolarisScanner.Models;

public class Reservation
{
    public string? Status { get; set; }
    public string? MoviePoster { get; set; }
    public string? MovieTitle { get; set; }
    public string? SessionDatetime { get; set; }
    public int? SessionRoom { get; set; }
    
    //Constructeur
    public Reservation(string status, string moviePoster, string movieTitle, string sessionDatetime, int sessionRoom)
    {
        this.Status = status;
        this.MoviePoster = moviePoster;
        this.MovieTitle = movieTitle;
        this.SessionDatetime = sessionDatetime;
        this.SessionRoom = sessionRoom;
    }
}