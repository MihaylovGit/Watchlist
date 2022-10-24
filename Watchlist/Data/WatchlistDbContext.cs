using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Watchlist.Data.Entities;
using static Watchlist.Data.DataConstants.User;

namespace Watchlist.Data
{
    public class WatchlistDbContext : IdentityDbContext
    {
        public WatchlistDbContext(DbContextOptions<WatchlistDbContext> options)
            : base(options)
        {
        }

        public DbSet<Movie> Movies { get; set; }

        public DbSet<Genre> Genres { get; set; }

        public DbSet<UserMovie> UsersMovies { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<Genre>()
                .HasData(new Genre()
                {
                    Id = 1,
                    Name = "Action"
                },
                new Genre()
                {
                    Id = 2,
                    Name = "Comedy"
                },
                new Genre()
                {
                    Id = 3,
                    Name = "Drama"
                },
                new Genre()
                {
                    Id = 4,
                    Name = "Horror"
                },
                new Genre()
                {
                    Id = 5,
                    Name = "Romantic"
                });

            builder.Entity<Movie>()
                   .Property(x => x.Rating)
                   .HasPrecision(18, 2);

            builder.Entity<UserMovie>()
                   .HasKey(x => new
                   {
                       x.UserId,
                       x.MovieId,
                   });

            builder.Entity<User>()
                   .Property(x => x.UserName)
                   .HasMaxLength(UserUserNameMaxLength)
                   .IsRequired(true);

            builder.Entity<User>()
                   .Property(x => x.Email)
                   .HasMaxLength(UserEmailMaxLength)
                   .IsRequired(true);

            base.OnModelCreating(builder);
        }
    }
}