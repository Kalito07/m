using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Movie_Catalog.Data.Models;
using Movie_Catalog.Services.Interfaces;

namespace Movie_Catalog.Controllers
{
    public class MovieController : Controller
    {
        private readonly ILogger<MovieController> _logger;
        private readonly IMovieService _movieService;
        private readonly IGenreService _genreService;
        private readonly IDirectorService _directorService;

        public MovieController(ILogger<MovieController> logger, IMovieService movieService, IGenreService genreService, IDirectorService directorService)
        {
            _logger = logger;
            _movieService = movieService;
            _genreService = genreService;
            _directorService = directorService;
        }

        // Action to list all movies
        [HttpGet("/")]
        public async Task<IActionResult> All()
        {
            // Get all movies
            ICollection<Movie> movies = await _movieService.GetAllMoviesAsync();

            // Log message
            _logger.Log(LogLevel.Information, "MovieController::All");

            // Pass data to view
            return View(movies);
        }

        // Action for creating a new movie - show form
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create()
        {
            // Get genres and directors for selection in form
            ICollection<Genre> genres = await _genreService.GetAllGenresAsync();
            ICollection<Director> directors = await _directorService.GetAllDirectorsAsync();

            // Passing data to the view
            ViewBag.Genres = genres;
            ViewBag.Directors = directors;

            return View();
        }

        // Action for handling movie creation form submission
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(string title, string? description, int? releaseYear, int? genreId, int? directorId, double rating)
        {
            await _movieService.CreateAsync(title, description, releaseYear, genreId, directorId, rating);

            return RedirectToAction("All");
        }

        // Action for editing an existing movie - show form
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            Movie? movie = await _movieService.GetMovieByIdAsync(id);
            ICollection<Genre> genres = await _genreService.GetAllGenresAsync();
            ICollection<Director> directors = await _directorService.GetAllDirectorsAsync();

            ViewBag.Genres = genres;
            ViewBag.Directors = directors;

            return View(movie);
        }

        // Action for handling movie editing form submission
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(Movie movie)
        {
            await _movieService.EditAsync(movie.Id, movie.Title, movie.Description, movie.ReleaseYear, movie.GenreId, movie.DirectorId, movie.Rating);
            return RedirectToAction("All");
        }

        // Action for deleting a movie
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _movieService.DeleteAsync(id);
            return RedirectToAction("All");
        }
    }
}
