using Microsoft.EntityFrameworkCore;
using Movie_Catalog.Data;
using Movie_Catalog.Services.Interfaces;
using Movie_Catalog.Data.Models;

namespace Movie_Catalog.Services
{
    /// <summary>
    /// Сервизен клас за работа с оценки.
    /// </summary>
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

        public async Task AddRatingAsync(int movieId, double ratingValue)
        {
            var movie = await _context.Movies
                .Include(m => m.Ratings)
                .FirstOrDefaultAsync(m => m.Id == movieId);

            if (movie == null)
            {
                throw new KeyNotFoundException("Movie not found.");
            }

            // Добавяме новия рейтинг
            var rating = new Rating
            {
                MovieId = movieId,
                RatingValue = ratingValue
            };

            _context.Ratings.Add(rating);

            // Записваме промените, за да се създаде rating
            await _context.SaveChangesAsync();

            // Пресмятаме средната стойност на рейтингите
            var newAverage = await _context.Ratings
                .Where(r => r.MovieId == movieId)
                .AverageAsync(r => r.RatingValue);

            movie.Rating = newAverage;
            await _context.SaveChangesAsync();
        }


        public async Task DeleteRatingAsync(int id)
        {
            var rating = await _context.Ratings.FindAsync(id);

            if (rating == null)
            {
                throw new KeyNotFoundException($"Rating with ID {id} not found.");
            }

            _context.Ratings.Remove(rating);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Rating>> GetRatingsByMovieIdAsync(int movieId)
        {
            return await _context.Ratings
                .Where(r => r.MovieId == movieId)
                .Include(r => r.Movie)
                .ToListAsync();
        }
    }
}
