using Microsoft.EntityFrameworkCore;
using Movie_Catalog.Data;
using Movie_Catalog.Data.Models;
using Movie_Catalog.Services.Interfaces;

namespace Movie_Catalog.Services
{
    public class RatingService : IRatingService
    {
        private readonly MovieCatalogContext _context;

        public RatingService(MovieCatalogContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Rating>> GetAllRatingsAsync()
        {
            return await _context.Ratings
                .Include(r => r.Movie)
                .ToListAsync();
        }

        public async Task<Rating?> GetRatingByIdAsync(int id)
        {
            return await _context.Ratings
                .Include(r => r.Movie)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<IEnumerable<Rating>> GetRatingsByMovieIdAsync(int movieId)
        {
            return await _context.Ratings
                .Where(r => r.MovieId == movieId)
                .ToListAsync();
        }

        public async Task AddRatingAsync(int movieId, double ratingValue)
        {
            var movie = await _context.Movies
                .Include(m => m.Ratings)
                .FirstOrDefaultAsync(m => m.Id == movieId);

            if (movie == null)
            {
                throw new KeyNotFoundException($"Movie with ID {movieId} was not found.");
            }

            var rating = new Rating
            {
                MovieId = movieId,
                RatingValue = ratingValue
            };

            _context.Ratings.Add(rating);
            await _context.SaveChangesAsync();
            movie.Rating = movie.Ratings.Append(rating).Average(r => r.RatingValue);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteRatingAsync(int id)
        {
            var rating = await _context.Ratings.FindAsync(id);

            if (rating == null)
            {
                throw new KeyNotFoundException($"Rating with ID {id} was not found.");
            }

            int? movieId = rating.MovieId;

            _context.Ratings.Remove(rating);
            await _context.SaveChangesAsync();

            // Bewertung im Movie aktualisieren (nach dem Löschen)
            if (movieId.HasValue)
            {
                var movie = await _context.Movies
                    .Include(m => m.Ratings)
                    .FirstOrDefaultAsync(m => m.Id == movieId.Value);

                if (movie != null)
                {
                    movie.Rating = movie.Ratings.Any()
                        ? movie.Ratings.Average(r => r.RatingValue)
                        : 0;

                    await _context.SaveChangesAsync();
                }
            }
        }
    }
}
