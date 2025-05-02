using Microsoft.AspNetCore.Mvc;
using Movie_Catalog.Services.Interfaces;
using Movie_Catalog.Data.Models;

namespace Movie_Catalog.Controllers
{
    public class RatingController : Controller
    {
        private readonly IRatingService _ratingService;
        private readonly IMovieService _movieService;

        public RatingController(IRatingService ratingService, IMovieService movieService)
        {
            _ratingService = ratingService;
            _movieService = movieService;
        }

        // GET: /Rating
        public async Task<IActionResult> Index()
        {
            var ratings = await _ratingService.GetAllRatingsAsync();
            return View(ratings);
        }

        // GET: /Rating/AddRating/{movieId}
        public async Task<IActionResult> AddRating(int movieId)
        {
            var movie = await _movieService.GetMovieByIdAsync(movieId);
            if (movie == null) return NotFound();

            return View(movie);
        }

        // POST: /Rating/AddRating
        [HttpPost]
        public async Task<IActionResult> AddRating(int movieId, double ratingValue)
        {
            await _ratingService.AddRatingAsync(movieId, ratingValue);
            return RedirectToAction("Index");
        }

        // GET: /Rating/Details/{id}
        public async Task<IActionResult> Details(int id)
        {
            var rating = await _ratingService.GetRatingByIdAsync(id);
            if (rating == null) return NotFound();

            return View(rating);
        }

        // GET: /Rating/Delete/{id}
        public async Task<IActionResult> Delete(int id)
        {
            var rating = await _ratingService.GetRatingByIdAsync(id);
            if (rating == null) return NotFound();

            return View(rating);
        }

        // POST: /Rating/DeleteConfirmed
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _ratingService.DeleteRatingAsync(id);
            return RedirectToAction("Index");
        }
    }
}
