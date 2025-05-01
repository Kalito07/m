using Microsoft.EntityFrameworkCore;
using Movie_Catalog.Data;
using Movie_Catalog.Data.Models;
using Movie_Catalog.Services.Interfaces;

namespace Movie_Catalog.Services
{
    public class GenreService : IGenreService
    {
        private readonly MovieCatalogContext _context;

        public GenreService(MovieCatalogContext context)
        {
            _context = context;
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

        public async Task AddGenreAsync(string name)
        {
            var genre = new Genre
            {
                Name = name
            };

            await _context.Genres.AddAsync(genre);
            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(int id, string name)
        {
            var genre = await GetGenreByIdAsync(id);
            genre.Name = name;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var genre = await _context.Genres.FindAsync(id);

            if (genre == null)
            {
                throw new KeyNotFoundException($"Genre with ID {id} was not found.");
            }

            _context.Genres.Remove(genre);
            await _context.SaveChangesAsync();
        }

    }
}