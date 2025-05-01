using Microsoft.AspNetCore.Mvc;
using Movie_Catalog.Models;
using Movie_Catalog.Services.Interfaces;
using System.Diagnostics;

namespace Movie_Catalog.Controllers
{
    public class RatingController : Controller
    {
        private readonly IRatingService _ratingService;

        public RatingController(IRatingService ratingService)
        {
            _ratingService = ratingService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var ratings = await _ratingService.GetAllRatingsAsync();
                return View(ratings);
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel
                {
                    Message = ex.Message,
                    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
                });
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var rating = await _ratingService.GetRatingByIdAsync(id);
                if (rating == null)
                    return NotFound();

                return View(rating);
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel
                {
                    Message = ex.Message,
                    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(int movieId, double ratingValue)
        {
            try
            {
                await _ratingService.AddRatingAsync(movieId, ratingValue);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel
                {
                    Message = ex.Message,
                    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _ratingService.DeleteRatingAsync(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel
                {
                    Message = ex.Message,
                    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
                });
            }
        }
    }
}
