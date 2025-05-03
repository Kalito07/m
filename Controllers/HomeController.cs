using Microsoft.AspNetCore.Mvc;
using Movie_Catalog.Data.Models;
using Movie_Catalog.Services.Interfaces;

namespace Movie_Catalog.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHomeService _homeService;

        public HomeController(IHomeService homeService)
        {
            _homeService = homeService;
        }

        public async Task<IActionResult> Index()
        {
            var stats = await _homeService.GetStatisticsAsync();
            var latestMovies = await _homeService.GetLatestMoviesAsync(5);

            var viewModel = new Home
            {
                TotalMovies = stats.TotalMovies,
                AverageRating = stats.AverageRating,
                LatestMovies = latestMovies.ToList()
            };

            return View(viewModel);
        }
        public IActionResult Privacy()
        {
            return View();
        }
    }
}