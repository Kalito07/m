using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Movie_Catalog.Models;
using Movie_Catalog.Services.Interfaces;
using Movie_Catalog.Data.Models;

namespace Movie_Catalog.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMovieService _movieService;
        private readonly IRatingService _ratingService;

        public HomeController(IMovieService movieService, IRatingService ratingService)
        {
            _movieService = movieService;
            _ratingService = ratingService;
        }

        public async Task<IActionResult> Index()
        {
            var latestMovies = await _movieService.GetLatestMoviesAsync(5);
            var stats = await _movieService.GetStatisticsAsync();

            var model = new Home
            {
                LatestMovies = latestMovies,
                TotalMovies = stats.TotalMovies,
                AverageRating = stats.AverageRating
            };

            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}