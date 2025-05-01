using Movie_Catalog.Data.Models;

namespace Movie_Catalog.Services.Interfaces
{
    public interface IGenreService
    {
        Task<ICollection<Genre>> GetAllGenresAsync();

        Task<Genre> GetGenreByIdAsync(int id);

        Task AddGenreAsync(string name);

        Task EditAsync(int id, string name);
        Task DeleteAsync(int id); 
    }
}