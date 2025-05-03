using Microsoft.EntityFrameworkCore;
using Movie_Catalog.Data;
using Movie_Catalog.Data.Models;
using Movie_Catalog.Services.Interfaces;

namespace Movie_Catalog.Services
{
    public class MovieService : IMovieService
    {
        private readonly MovieCatalogContext _context;

        public MovieService(MovieCatalogContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Movie>> AllAsync()
        {
            return await _context.Movies
                .Include(m => m.Genre)
                .Include(m => m.Director)
                .ToListAsync();
        }

        public async Task<Movie> GetMovieByIdAsync(int id)
        {
            var movie = await _context.Movies
                .Include(m => m.Genre)
                .Include(m => m.Director)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (movie == null)
            {
                throw new KeyNotFoundException($"Movie with id {id} was not found.");
            }

            return movie;
        }

        public async Task<Movie> GetMovieDetailsAsync(int id)
        {
            var movie = await _context.Movies
                .Include(m => m.Genre)
                .Include(m => m.Director)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (movie == null)
            {
                throw new KeyNotFoundException($"Movie with id {id} was not found.");
            }

            return movie;
        }

        public async Task CreateAsync(string title, string? description, int? releaseYear, int? genreId, int? directorId, double rating)
        {
            var exists = await _context.Movies
                .AnyAsync(m => m.Title == title && m.ReleaseYear == releaseYear);

            if (exists)
            {
                throw new InvalidOperationException("A movie with the same title and release year already exists.");
            }

            var movie = new Movie
            {
                Title = title,
                Description = description,
                ReleaseYear = releaseYear,
                GenreId = genreId,
                DirectorId = directorId,
                Rating = rating
            };

            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();
        }


        public async Task EditAsync(int id, string title, string? description, int? releaseYear, int? genreId, int? directorId, double rating)
        {
            var movie = await GetMovieByIdAsync(id);

            movie.Title = title;
            movie.Description = description;
            movie.ReleaseYear = releaseYear;
            movie.GenreId = genreId;
            movie.DirectorId = directorId;
            movie.Rating = rating;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var movie = await GetMovieByIdAsync(id);

            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();
        }
        
    }
}
