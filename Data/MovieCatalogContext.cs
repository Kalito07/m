using Microsoft.EntityFrameworkCore;
using Movie_Catalog.Data.Models;

namespace Movie_Catalog.Data
{

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

                result += base.SaveChanges();
            }

            return result;
        }

        private void UpdateMovieRating(int movieId)
        {
            var movie = Movies
                .Include(m => m.Ratings)
                .FirstOrDefault(m => m.Id == movieId);

            if (movie?.Ratings?.Any() == true)
            {
                movie.Rating = movie.Ratings
                    .Where(r => r.RatingValue >= 1.0 && r.RatingValue <= 10.0)
                    .Average(r => r.RatingValue);
            }
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>()
                .HasIndex(m => new { m.Title, m.ReleaseYear })
                .IsUnique();

            base.OnModelCreating(modelBuilder);
        }

    }
}