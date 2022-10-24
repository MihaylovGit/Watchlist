using Microsoft.EntityFrameworkCore;
using Watchlist.Contracts;
using Watchlist.Data;
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
    }
}
