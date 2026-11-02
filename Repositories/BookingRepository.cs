using Microsoft.EntityFrameworkCore;
using MovieTicketBooking.Data;
using MovieTicketBooking.Models;
using MovieTicketBooking.Repositories.Interfaces;

namespace MovieTicketBooking.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly ApplicationDbContext _context;

        public BookingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Booking?> GetByIdAsync(int id)
        {
            return await _context.Bookings
                .Include(b => b.Show)
                    .ThenInclude(s => s.Movie)
                .Include(b => b.Show)
                    .ThenInclude(s => s.Screen)
                        .ThenInclude(sc => sc.Theater)
                .Include(b => b.BookingSeats)
                    .ThenInclude(bs => bs.Seat)
                .FirstOrDefaultAsync(b => b.BookingId == id);
        }

        public async Task<IEnumerable<Booking>> GetAllAsync()
        {
            return await _context.Bookings
                .Include(b => b.Show)
                    .ThenInclude(s => s.Movie)
                .Include(b => b.Show)
                    .ThenInclude(s => s.Screen)
                        .ThenInclude(sc => sc.Theater)
                .Include(b => b.BookingSeats)
                    .ThenInclude(bs => bs.Seat)
                .ToListAsync();
        }

        public async Task AddAsync(Booking booking)
        {
            await _context.Bookings.AddAsync(booking);
        }

        public async Task UpdateAsync(Booking booking)
        {
            _context.Bookings.Update(booking);
            await Task.CompletedTask;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<List<ShowSeat>> GetShowSeatsAsync(int showId, List<int> seatIds)
        {
            return await _context.ShowSeats
                .Include(ss => ss.Seat)
                .Where(ss => ss.ShowId == showId && seatIds.Contains(ss.SeatId))
                .ToListAsync();
        }

        public async Task<IEnumerable<Seat>> GetSeatsByScreenAndIdsAsync(int screenId, List<int> seatIds)
        {
            return await _context.Seats
                .Where(s => s.ScreenId == screenId && seatIds.Contains(s.SeatId))
                .ToListAsync();
        }

        public async Task<bool> IsAnySeatBookedAsync(int showId, List<int> seatIds)
        {
            return await _context.BookingSeats
                .AnyAsync(bs => bs.Booking.ShowId == showId && seatIds.Contains(bs.SeatId));
        }

        public async Task AddBookingSeatAsync(BookingSeat bookingSeat)
        {
            await _context.BookingSeats.AddAsync(bookingSeat);
        }
    }
}