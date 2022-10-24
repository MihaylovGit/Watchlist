using System.ComponentModel.DataAnnotations;
using static Watchlist.Data.DataConstants.User;

namespace Watchlist.Models
{
    public class RegisterViewModel
    {
        [Required]
        [StringLength(UserUserNameMaxLength, MinimumLength = UserUserNameMinLength)]
        public string UserName { get; set; } = null!;

        [Required]
        [StringLength(UserEmailMaxLength, MinimumLength = UserEmailMinLength)]
        public string Email { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [Required]
        [Compare(nameof(Password))]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; } = null!;
    }
}
