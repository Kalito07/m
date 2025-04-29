using Microsoft.EntityFrameworkCore;
using Movie_Catalog.Data.Models;

public class MovieCatalogContext : DbContext
{
    public MovieCatalogContext(DbContextOptions<MovieCatalogContext> options) : base(options)
    {
        
    }
    public DbSet<Movie> Movies { get; set; }
    public DbSet<Director> Directors { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Rating> Ratings { get; set; }
    public override int SaveChanges()
    {
        var addedRatings = ChangeTracker.Entries<Rating>()
            .Where(e => e.State == EntityState.Added)
            .Select(e => e.Entity)
            .ToList();

        var result = base.SaveChanges();

        if (addedRatings.Any())
        {
            foreach (var rating in addedRatings)
            {
                if (rating.MovieId.HasValue)
                {
                    UpdateMovieRating(rating.MovieId.Value);
                }
            }

            base.SaveChanges(); // за да се запише обновеният рейтинг на филма
        }

        return result;
    }
    private void UpdateMovieRating(int movieId)
    {
        var movie = Movies
            .Include(m => m.Ratings)
            .FirstOrDefault(m => m.Id == movieId);

        if (movie != null && movie.Ratings.Any())
        {
            movie.Rating = movie.Ratings.Average(r => r.RatingValue);
        }
    }
}