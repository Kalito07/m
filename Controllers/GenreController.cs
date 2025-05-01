using Microsoft.AspNetCore.Mvc;
using Movie_Catalog.Data.Models;
using Movie_Catalog.Models;
using Movie_Catalog.Services.Interfaces;

namespace Movie_Catalog.Controllers
{
    public class GenreController : Controller
    {
        private readonly IGenreService _genreService;

        public GenreController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        // GET: Genre/All
        [HttpGet]
        public async Task<IActionResult> All()
        {
            try
            {
                var genres = await _genreService.GetAllGenresAsync();
                return View(genres);
            }
            catch (Exception ex)
            {
                // Handle the error, e.g., log it
                return View("Error", new ErrorViewModel { Message = ex.Message });
            }
        }

        // GET: Genre/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Genre/Create
        [HttpPost]
        public async Task<IActionResult> Create(string name)
        {
            try
            {
                // Validate the input (could be done in service too)
                if (string.IsNullOrWhiteSpace(name))
                {
                    ModelState.AddModelError("Name", "Genre name cannot be empty.");
                    return View();
                }

                await _genreService.AddGenreAsync(name);
                return RedirectToAction("All");
            }
            catch (Exception ex)
            {
                // Handle the error, e.g., log it
                return View("Error", new ErrorViewModel { Message = ex.Message });
            }
        }

        // GET: Genre/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var genre = await _genreService.GetGenreByIdAsync(id);
                return View(genre);
            }
            catch (Exception ex)
            {
                // Handle the error, e.g., log it
                return View("Error", new ErrorViewModel { Message = ex.Message });
            }
        }

        // POST: Genre/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(int id, string name)
        {
            try
            {
                // Validate the input
                if (string.IsNullOrWhiteSpace(name))
                {
                    ModelState.AddModelError("Name", "Genre name cannot be empty.");
                    return View();
                }

                await _genreService.EditAsync(id, name);
                return RedirectToAction("All");
            }
            catch (Exception ex)
            {
                // Handle the error, e.g., log it
                return View("Error", new ErrorViewModel { Message = ex.Message });
            }
        }

        // GET: Genre/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var genre = await _genreService.GetGenreByIdAsync(id);
                return View(genre);
            }
            catch (Exception ex)
            {
                // Handle the error, e.g., log it
                return View("Error", new ErrorViewModel { Message = ex.Message });
            }
        }

        // POST: Genre/Delete/5
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _genreService.DeleteAsync(id);
                return RedirectToAction("All");
            }
            catch (Exception ex)
            {
                // Handle the error, e.g., log it
                return View("Error", new ErrorViewModel { Message = ex.Message });
            }
        }
    }
}
