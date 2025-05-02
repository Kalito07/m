using Microsoft.EntityFrameworkCore;
using Movie_Catalog.Data;
using Movie_Catalog.Data.Models;
using Movie_Catalog.Services.Interfaces;

namespace Movie_Catalog.Services
{
    public class HomeService: IHomeService
    {
        private readonly MovieCatalogContext _context;

        public HomeService(MovieCatalogContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Movie>> GetLatestMoviesAsync(int count)
        {
            return await _context.Movies
                .OrderByDescending(m => m.Id)
                .Take(count)
                .ToListAsync();
        }

        public async Task<(int TotalMovies, double AverageRating)> GetStatisticsAsync()
        {
            var totalMovies = await _context.Movies.CountAsync();
            var averageRating = await _context.Movies.AverageAsync(m => m.Rating);

            return (totalMovies, averageRating);
        }

    public async Task<ICollection<Genre>> GetAllGenresAsync()
    {
        return await _context.Genres.ToListAsync();
    }

    public async Task<Genre> GetGenreByIdAsync(int id)
    {
        var genre = await _context.Genres.FindAsync(id);

        if (genre == null)
        {
            throw new KeyNotFoundException($"Genre with ID {id} was not found.");
        }

        return genre;
    }

    }
}