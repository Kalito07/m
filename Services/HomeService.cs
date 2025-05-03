using Microsoft.EntityFrameworkCore;
using Movie_Catalog.Data;
using Movie_Catalog.Data.Models;
using Movie_Catalog.Services.Interfaces;

namespace Movie_Catalog.Services
{
    public class HomeService : IHomeService
    {
        private readonly MovieCatalogContext _context;

        public HomeService(MovieCatalogContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Връща последните N добавени филма (според ID).
        /// </summary>
        public async Task<IEnumerable<Movie>> GetLatestMoviesAsync(int count)
        {
            return await _context.Movies
                .OrderByDescending(m => m.Id)
                .Take(count)
                .ToListAsync();
        }

        /// <summary>
        /// Връща общия брой филми и средната им оценка.
        /// </summary>
        public async Task<(int TotalMovies, double AverageRating)> GetStatisticsAsync()
        {
            var totalMovies = await _context.Movies.CountAsync();
            double averageRating = 0;

            if (totalMovies > 0)
            {
                averageRating = await _context.Movies
                    .Where(m => m.Rating > 0)
                    .AverageAsync(m => m.Rating);
            }

            return (totalMovies, averageRating);
        }

        /// <summary>
        /// Връща всички жанрове.
        /// </summary>
        public async Task<ICollection<Genre>> GetAllGenresAsync()
        {
            return await _context.Genres.ToListAsync();
        }

        /// <summary>
        /// Връща жанр по ID или хвърля грешка ако не съществува.
        /// </summary>
        public async Task<Genre> GetGenreByIdAsync(int id)
        {
            var genre = await _context.Genres.FindAsync(id);

            if (genre == null)
            {
                throw new KeyNotFoundException($"Жанр с ID {id} не е намерен.");
            }

            return genre;
        }
    }
}