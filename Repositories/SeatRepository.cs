using Microsoft.EntityFrameworkCore;
using MovieTicketBooking.Data;
using MovieTicketBooking.Interfaces;
using MovieTicketBooking.Models;

namespace MovieTicketBooking.Repositories
{
    public class SeatRepository : ISeatRepository
    {
        private readonly ApplicationDbContext _context;

        public SeatRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Seat>> GetAllAsync()
        {
            return await _context.Seats.Include(s => s.Screen).ToListAsync();
        }

        public async Task<Seat?> GetByIdAsync(int id)
        {
            return await _context.Seats.Include(s => s.Screen).FirstOrDefaultAsync(s => s.SeatId == id);
        }

        public async Task<IEnumerable<Seat>> GetByScreenIdAsync(int screenId)
        {
            return await _context.Seats
                .Where(s => s.ScreenId == screenId)
                .Include(s => s.Screen)
                .ToListAsync();
        }

        public async Task<Seat> AddAsync(Seat seat)
        {
            await _context.Seats.AddAsync(seat);
            await _context.SaveChangesAsync();
            return seat;
        }

        public async Task<bool> UpdateAsync(Seat seat)
        {
            _context.Seats.Update(seat);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var seat = await _context.Seats.FindAsync(id);
            if (seat == null) return false;

            _context.Seats.Remove(seat);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}