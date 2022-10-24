using Microsoft.EntityFrameworkCore;
using Watchlist.Contracts;
using Watchlist.Data;
using Watchlist.Data.Entities;
using Watchlist.Models;

namespace Watchlist.Services
{
    public class MovieService : IMovieService
    {
        private readonly WatchlistDbContext _dbContext;

        public MovieService(WatchlistDbContext dbContext)
        {
           _dbContext = dbContext;
        }

        public async Task AddMovieAsync(AddMovieViewModel model)
        {
            var entity = new Movie()
            {
                Title = model.Title,
                Director = model.Director,
                ImageUrl = model.ImageUrl,
                Rating = model.Rating,
                GenreId = model.GenreId,
            };

            await _dbContext.Movies.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<MovieViewModel>> GetAllMoviesAsync()
        {
            var entities = await _dbContext.Movies
                                           .Include(m => m.Genre)
                                           .ToListAsync();

            return entities.Select(x => new MovieViewModel()
            {
                Id = x.Id,
                Title = x.Title,
                Director = x.Director,
                ImageUrl = x.ImageUrl,
                Rating = x.Rating,
                Genre = x.Genre?.Name
            });
        }

        public async Task<IEnumerable<Genre>> GetGenresAsync()
        {
          return await _dbContext.Genres.ToListAsync();
        }
    }
}
