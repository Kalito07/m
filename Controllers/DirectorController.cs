using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movie_Catalog.Data;
using Movie_Catalog.Data.Models;
using Movie_Catalog.Models;
using Movie_Catalog.Services.Interfaces;

namespace Movie_Catalog.Controllers
{
    public class DirectorController : Controller
    {
        private readonly IDirectorService _directorService;
        private readonly MovieCatalogContext _context;  // Add the context here

        // Inject the context into the constructor
        public DirectorController(IDirectorService directorService, MovieCatalogContext context)
        {
            _directorService = directorService;
            _context = context;
        }

        // GET: Director/All
        [HttpGet]
        public async Task<IActionResult> All()
        {
            try
            {
                var directors = await _directorService.GetAllDirectorsAsync();
                return View(directors);
            }
            catch (Exception ex)
            {
                // Handle the error, e.g., log it
                return View("Error", new ErrorViewModel { Message = ex.Message });
            }
        }

        // GET: Director/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Director/Create
        [HttpPost]
        public async Task<IActionResult> Create(string name)
        {
            try
            {
                // Validate the input
                if (string.IsNullOrWhiteSpace(name))
                {
                    ModelState.AddModelError("Name", "Director name cannot be empty.");
                    return View();
                }

                await _directorService.AddDirectorAsync(name);
                return RedirectToAction("All");
            }
            catch (Exception ex)
            {
                // Handle the error, e.g., log it
                return View("Error", new ErrorViewModel { Message = ex.Message });
            }
        }

        // GET: Director/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var director = await _directorService.GetDirectorByIdAsync(id);
                return View(director);
            }
            catch (Exception ex)
            {
                // Handle the error, e.g., log it
                return View("Error", new ErrorViewModel { Message = ex.Message });
            }
        }

        // POST: Director/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(int id, string name)
        {
            try
            {
                // Validate the input
                if (string.IsNullOrWhiteSpace(name))
                {
                    ModelState.AddModelError("Name", "Director name cannot be empty.");
                    return View();
                }

                await _directorService.EditAsync(id, name);
                return RedirectToAction("All");
            }
            catch (Exception ex)
            {
                // Handle the error, e.g., log it
                return View("Error", new ErrorViewModel { Message = ex.Message });
            }
        }

        // GET: Director/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var director = await _directorService.GetDirectorByIdAsync(id);
                return View(director);
            }
            catch (Exception ex)
            {
                // Handle the error, e.g., log it
                return View("Error", new ErrorViewModel { Message = ex.Message });
            }
        }

        // POST: Director/Delete/5
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var director = await _context.Directors.FindAsync(id); // Use _context to find the director
                if (director == null)
                {
                    return NotFound();
                }

                _context.Directors.Remove(director);  // Remove the director
                await _context.SaveChangesAsync();    // Save changes to the database

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
