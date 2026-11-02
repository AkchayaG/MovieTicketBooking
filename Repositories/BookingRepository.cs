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
                .ThenInclude(s => s!.Movie)
                .Include(b => b.Show)
                .ThenInclude(s => s!.Screen)
                .ThenInclude(sc => sc!.Theater)
                .FirstOrDefaultAsync(b => b.BookingId == id);
        }

        public async Task<IEnumerable<Booking>> GetAllAsync()
        {
            return await _context.Bookings
                .Include(b => b.Show)
                .ThenInclude(s => s!.Movie)
                .ToListAsync();
        }

        public async Task<IEnumerable<Booking>> GetByShowIdAsync(int showId)
        {
            return await _context.Bookings
                .Where(b => b.ShowId == showId)
                .ToListAsync();
        }
        public async Task<Booking> AddAsync(Booking booking)
        {
            await _context.Bookings.AddAsync(booking);
            await _context.SaveChangesAsync();
            return booking;
        }

        public async Task UpdateAsync(Booking booking)
        {
            _context.Bookings.Update(booking);
            await Task.CompletedTask;
        }

        public async Task<List<ShowSeat>> GetShowSeatsAsync(int showId, List<int> seatIds)
        {
            return await _context.ShowSeats
                .Include(ss => ss.Seat)
                .Where(ss => ss.ShowId == showId && seatIds.Contains(ss.SeatId))
                .ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
       
    }
}