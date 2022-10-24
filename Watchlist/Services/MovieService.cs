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

        public async Task AddMovieToCollectionAsync(int movieId, string userId)
        {
            var user = await _dbContext.Users
                .Where(u => u.Id == userId)
                .Include(u => u.UsersMovies)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new ArgumentException("Invalid user ID");
            }

            var movie = await _dbContext.Movies.FirstOrDefaultAsync(u => u.Id == movieId);

            if (movie == null)
            {
                throw new ArgumentException("Invalid Movie ID");
            }

            if (!user.UsersMovies.Any(m => m.MovieId == movieId))
            {
                user.UsersMovies.Add(new UserMovie()
                {
                    MovieId = movie.Id,
                    UserId = user.Id,
                    Movie = movie,
                    User = user
                });

                await _dbContext.SaveChangesAsync();
            }
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

        public async Task<IEnumerable<MovieViewModel>> GetWatchedAsync(string userId)
        {
            var user = await _dbContext.Users
               .Where(u => u.Id == userId)
               .Include(u => u.UsersMovies)
               .ThenInclude(um => um.Movie)
               .ThenInclude(m => m.Genre)
               .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new ArgumentException("Invalid user ID");
            }

            return user.UsersMovies
                .Select(m => new MovieViewModel()
                {
                    Director = m.Movie.Director,
                    Genre = m.Movie.Genre?.Name,
                    Id = m.MovieId,
                    ImageUrl = m.Movie.ImageUrl,
                    Title = m.Movie.Title,
                    Rating = m.Movie.Rating,
                });
        }

        public async Task RemoveMovieFromCollectionAsync(int movieId, string userId)
        {
            var user = await _dbContext.Users
                .Where(u => u.Id == userId)
                .Include(u => u.UsersMovies)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new ArgumentException("Invalid user ID");
            }

            var movie = user.UsersMovies.FirstOrDefault(m => m.MovieId == movieId);

            if (movie != null)
            {
                user.UsersMovies.Remove(movie);

                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
