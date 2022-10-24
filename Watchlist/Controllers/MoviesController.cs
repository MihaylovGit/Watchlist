using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Watchlist.Contracts;
using Watchlist.Models;

namespace Watchlist.Controllers
{
    [Authorize]
    public class MoviesController : Controller
    {
        private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var model = await _movieService.GetAllMoviesAsync();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var model = new AddMovieViewModel()
            {
                Genres = await _movieService.GetGenresAsync()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddMovieViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await _movieService.AddMovieAsync(model);

                return RedirectToAction(nameof(All));
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Ooops something went wrong");

                return View(model);
            }
        }

        public async Task<IActionResult> AddToCollection(int movieId)
        {
            try
            {
                var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                await _movieService.AddMovieToCollectionAsync(movieId, userId);
            }
            catch (Exception)
            {
                throw;
            }

            return RedirectToAction(nameof(All));
        }
    }
}
