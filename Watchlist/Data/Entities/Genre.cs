using System.ComponentModel.DataAnnotations;
using static Watchlist.Data.DataConstants.Genre;

namespace Watchlist.Data.Entities
{
    public class Genre
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(GenreNameMaxLength)]
        public string Name { get; set; } = null!;

        public List<Movie> Movies { get; set; } = new List<Movie>();
    }
}
