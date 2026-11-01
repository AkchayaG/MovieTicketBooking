using Microsoft.EntityFrameworkCore;
using MovieTicketBooking.Models;
using System.Reflection.Emit;

namespace MovieTicketBooking.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Movie> Movies { get; set; } = null!;
        public DbSet<Theater> Theaters { get; set; }
        public DbSet<Screen> Screens { get; set; }
        public DbSet<Seat> Seats { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Movie>(entity =>
            {
                entity.HasKey(e => e.MovieId);
                entity.Property(e => e.Title).IsRequired().HasMaxLength(150);
                entity.Property(e => e.Description).HasMaxLength(500);
                entity.Property(e => e.DurationMinutes).IsRequired();
                entity.Property(e => e.ReleaseDate).HasColumnType("date");
                entity.Property(e => e.Language).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Genre).IsRequired().HasMaxLength(50);
            });
        }
    }
}