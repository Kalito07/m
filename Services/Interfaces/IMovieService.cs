using Movie_Catalog.Data.Models;

namespace Movie_Catalog.Services.Interfaces
{
    public interface IMovieService
    {
        Task<ICollection<Movie>> GetAllMoviesAsync();

        Task CreateAsync(string title, string? description, int? releaseYear, int? genreId, int? directorId, double rating);

        Task<Movie> GetMovieByIdAsync(int id);

        Task<Movie> GetMovieDetailsAsync(int id);
        Task EditAsync(int id, string title, string? description, int? releaseYear, int? genreId, int? directorId, double rating);

        Task DeleteAsync(int id);
    }
}