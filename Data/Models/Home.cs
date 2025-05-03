using Movie_Catalog.Data.Models;
namespace Movie_Catalog.Data.Models
{
public class Home
{
    public int TotalMovies { get; set; }
    public double AverageRating { get; set; }
    public List<Movie> LatestMovies { get; set; }
}

}