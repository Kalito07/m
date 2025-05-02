using Movie_Catalog.Data.Models;

public class Home
{
    public IEnumerable<Movie> LatestMovies { get; set; }
    public int TotalMovies { get; set; }
    public double AverageRating { get; set; }
}