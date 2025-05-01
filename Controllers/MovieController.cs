using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Movie_Catalog.Data.Models;
using Movie_Catalog.Models;
using Movie_Catalog.Services.Interfaces;

namespace Movie_Catalog.Controllers
{
    public class MovieController(
        ILogger<MovieController> logger,
        IMovieService movieService,
        IGenreService genreService,
        IDirectorService directorService)
        : Controller
    {
        [HttpGet("/")]
        public async Task<IActionResult> All()
        {
            try
            {
                var movies = await movieService.GetAllMoviesAsync();
                return View(movies);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error fetching all movies.");
                return View("Error", new ErrorViewModel { Message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            try
            {
                var genres = await genreService.GetAllGenresAsync();
                var directors = await directorService.GetAllDirectorsAsync();

                ViewBag.Genres = new SelectList(genres, "Id", "Name");
                ViewBag.Directors = new SelectList(directors, "Id", "FullName");

                return View();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error loading Create view.");
                return View("Error", new ErrorViewModel { Message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(string title, string? description, int? releaseYear, int? genreId, int? directorId, double rating)
        {
            try
            {
                await movieService.CreateAsync(title, description, releaseYear, genreId, directorId, rating);
                return RedirectToAction("All");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error creating movie.");
                return View("Error", new ErrorViewModel { Message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var movie = await movieService.GetMovieByIdAsync(id);

                var genres = await genreService.GetAllGenresAsync();
                var directors = await directorService.GetAllDirectorsAsync();

                ViewBag.Genres = new SelectList(genres, "Id", "Name", movie.GenreId);
                ViewBag.Directors = new SelectList(directors, "Id", "FullName", movie.DirectorId);

                return View(movie);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error loading movie with ID {id} for editing.");
                return View("Error", new ErrorViewModel { Message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Movie movie)
        {
            try
            {
                await movieService.EditAsync(movie.Id, movie.Title, movie.Description, movie.ReleaseYear, movie.GenreId, movie.DirectorId, movie.Rating);
                return RedirectToAction("All");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error editing movie.");
                return View("Error", new ErrorViewModel { Message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await movieService.DeleteAsync(id);
                return RedirectToAction("All");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error deleting movie with ID {id}.");
                return View("Error", new ErrorViewModel { Message = ex.Message });
            }
        }
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await movieService.DeleteAsync(id);
                return RedirectToAction("All");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error confirming deletion of movie with ID {id}.");
                
                return View("Error", new ErrorViewModel { Message = ex.Message });
            }
        }


    }
}
