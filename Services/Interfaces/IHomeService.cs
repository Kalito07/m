using Movie_Catalog.Data.Models;

namespace Movie_Catalog.Services.Interfaces
{
    public interface IHomeService
    {
        Task<IEnumerable<Movie>> GetLatestMoviesAsync(int count);

        Task<(int TotalMovies, double AverageRating)> GetStatisticsAsync();
    }
}