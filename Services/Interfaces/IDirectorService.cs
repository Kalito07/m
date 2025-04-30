using Movie_Catalog.Data.Models;

namespace Movie_Catalog.Services.Interfaces
{
    public interface IDirectorService
    {
        Task<ICollection<Director>> GetAllDirectorsAsync();

        Task<Director> GetDirectorByIdAsync(int id);

        Task AddDirectorAsync(string name);

        Task EditAsync(int id, string name);
    }
}