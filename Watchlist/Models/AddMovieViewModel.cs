using System.ComponentModel.DataAnnotations;
using Watchlist.Data.Entities;
using static Watchlist.Data.DataConstants.Movie;

namespace Watchlist.Models
{
    public class AddMovieViewModel
    {
        [Required]
        [StringLength(MovieTitleMaxLength, MinimumLength = MovieTitleMinLength)]
        public string Title { get; set; }

        [Required]
        [StringLength(MovieDirectorMaxLength, MinimumLength = MovieDirectorMinLength)]
        public string Director { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        [Range(typeof(decimal), "0.00", "10.00", ConvertValueInInvariantCulture = true)]
        public decimal Rating { get; set; }

        public int GenreId { get; set; }

        public IEnumerable<Genre> Genres { get; set; } = new List<Genre>();
    }
}
