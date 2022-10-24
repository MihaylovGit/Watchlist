using Watchlist.Data.Entities;
using Watchlist.Models;

namespace Watchlist.Contracts
{
    public interface IMovieService
    {
        Task<IEnumerable<MovieViewModel>> GetAllMoviesAsync();
        
        Task<IEnumerable<Genre>> GetGenresAsync();

        Task AddMovieAsync(AddMovieViewModel model);

        Task AddMovieToCollectionAsync(int movieId, string userId);

    }
}
