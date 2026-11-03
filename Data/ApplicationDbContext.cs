using Microsoft.EntityFrameworkCore;
using MovieTicketBooking.Models;

namespace MovieTicketBooking.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Theater> Theaters { get; set; }
        public DbSet<Screen> Screens { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<Show> Shows { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<BookingSeat> BookingSeats { get; set; }
        public DbSet<ShowSeat> ShowSeats { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // User -> Bookings
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.User)
                .WithMany(u => u.Bookings)
                .HasForeignKey(b => b.UserId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            // Screen -> Theater
            modelBuilder.Entity<Screen>()
                .HasOne(s => s.Theater)
                .WithMany(t => t.Screens)
                .HasForeignKey(s => s.TheaterId)
                .OnDelete(DeleteBehavior.Cascade);

            // Seat -> Screen
            modelBuilder.Entity<Seat>()
                .HasOne(s => s.Screen)
                .WithMany(s => s.Seats)
                .HasForeignKey(s => s.ScreenId)
                .OnDelete(DeleteBehavior.Cascade);

            // Show -> Movie
            modelBuilder.Entity<Show>()
                .HasOne(s => s.Movie)
                .WithMany(m => m.Shows)
                .HasForeignKey(s => s.MovieId)
                .OnDelete(DeleteBehavior.Cascade);

            // Show -> Screen
            modelBuilder.Entity<Show>()
                .HasOne(s => s.Screen)
                .WithMany(s => s.Shows)
                .HasForeignKey(s => s.ScreenId)
                .OnDelete(DeleteBehavior.Cascade);

            // Decimal precision
            modelBuilder.Entity<Show>()
                .Property(s => s.Price)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Show>()
                .Property(s => s.TicketPrice)
                .HasPrecision(10, 2);

            // Booking -> Show
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Show)
                .WithMany()
                .HasForeignKey(b => b.ShowId)
                .OnDelete(DeleteBehavior.Cascade);

            // BookingSeat -> Booking
            modelBuilder.Entity<BookingSeat>()
                .HasOne(bs => bs.Booking)
                .WithMany(b => b.BookingSeats)
                .HasForeignKey(bs => bs.BookingId)
                .OnDelete(DeleteBehavior.Cascade);

            // BookingSeat -> Seat
            modelBuilder.Entity<BookingSeat>()
                .HasOne(bs => bs.Seat)
                .WithMany()
                .HasForeignKey(bs => bs.SeatId)
                .OnDelete(DeleteBehavior.Restrict);

            // ShowSeat -> Show
            modelBuilder.Entity<ShowSeat>()
                .HasOne(ss => ss.Show)
                .WithMany()
                .HasForeignKey(ss => ss.ShowId)
                .OnDelete(DeleteBehavior.Cascade);

            // ShowSeat -> Seat
            modelBuilder.Entity<ShowSeat>()
                .HasOne(ss => ss.Seat)
                .WithMany()
                .HasForeignKey(ss => ss.SeatId)
                .OnDelete(DeleteBehavior.Restrict);
        }

    }
}