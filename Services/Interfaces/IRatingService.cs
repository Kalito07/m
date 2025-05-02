using Movie_Catalog.Data.Models;

namespace Movie_Catalog.Services.Interfaces
{
    /// <summary>
    /// Интерфейс за управление на оценки на филми.
    /// </summary>
    public interface IRatingService
    {
        Task<IEnumerable<Rating>> GetAllRatingsAsync();

        Task<Rating?> GetRatingByIdAsync(int id);

        Task AddRatingAsync(int movieId, double ratingValue);

        Task DeleteRatingAsync(int id);

        Task<IEnumerable<Rating>> GetRatingsByMovieIdAsync(int movieId);
    }
}