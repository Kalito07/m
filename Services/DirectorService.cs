using Microsoft.EntityFrameworkCore;
using Movie_Catalog.Data.Models;
using Movie_Catalog.Services.Interfaces;

namespace Movie_Catalog.Services
{
    public class DirectorService : IDirectorService
    {
        private readonly MovieCatalogContext _context;

        public DirectorService(MovieCatalogContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Director>> GetAllDirectorsAsync()
        {
            return await _context.Directors.ToListAsync();
        }

        public async Task<Director> GetDirectorByIdAsync(int id)
        {
            var director = await _context.Directors.FindAsync(id);

            if (director == null)
            {
                throw new KeyNotFoundException($"Director with ID {id} was not found.");
            }

            return director;
        }

        public async Task AddDirectorAsync(string name)
        {
            var director = new Director
            {
                Name = name
            };

            await _context.Directors.AddAsync(director);
            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(int id, string name)
        {
            var director = await GetDirectorByIdAsync(id);
            director.Name = name;

            await _context.SaveChangesAsync();
        }
    }
}