using System.ComponentModel.DataAnnotations;
using static Watchlist.Data.DataConstants.Movie;

namespace Watchlist.Data.Entities
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(MovieTitleMaxLength)]
        public string Title { get; set; } = null!;

        [Required]
        [MaxLength(MovieDirectorMaxLength)]
        public string Director { get; set; } = null!;

        [Required]
        public string ImageUrl { get; set; } = null!;

        [Range(typeof(decimal), "0.00", "10.00")]
        public decimal Rating { get; set; }

        public int GenreId { get; set; }

        public Genre Genre { get; set; }

        public IEnumerable<UserMovie> UsersMovies { get; set; } = new List<UserMovie>();
    }
}
