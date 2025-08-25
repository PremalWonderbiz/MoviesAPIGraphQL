using Microsoft.EntityFrameworkCore;

namespace Movies.API.Data
{
    public class MoviesDbContext : DbContext
    {
        public DbSet<Models.Movie> Movies { get; set; }
        public DbSet<Models.MovieReview> MovieReviews { get; set; }
        public MoviesDbContext(DbContextOptions<MoviesDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.Movie>().Property(m => m.Genre).HasConversion<string>();                
        }
    }
}
