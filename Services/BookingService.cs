
using MovieTicketBooking.DTOs.Bookings;
using MovieTicketBooking.Interfaces;
using MovieTicketBooking.Models;
using MovieTicketBooking.Repositories;
using MovieTicketBooking.Repositories.Interfaces;
using MovieTicketBooking.Services.Interfaces;

namespace MovieTicketBooking.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IShowRepository _showRepository;

        public BookingService(
            IBookingRepository bookingRepository,
            IShowRepository showRepository)
        {
            _bookingRepository = bookingRepository;
            _showRepository = showRepository;
        }

        public async Task<BookingResponse?> GetBookingByIdAsync(
            int id, int userId, bool isAdmin)
        {
            var booking = await _bookingRepository.GetByIdAsync(id);

            if (booking == null)
                return null;

            if (!isAdmin && booking.UserId != userId)
                return null;

            return MapToResponse(booking);
        }

        public async Task<IEnumerable<BookingResponse>> GetAllBookingsAsync()
        {
            var bookings = await _bookingRepository.GetAllAsync();
            return bookings.Select(MapToResponse);
        }

        public async Task<IEnumerable<BookingResponse>> GetMyBookingsAsync(int userId)
        {
            var bookings = await _bookingRepository.GetByUserIdAsync(userId);
            return bookings.Select(MapToResponse);
        }

        public async Task<BookingResponse> CreateBookingAsync(
            CreateBookingRequest request, int userId)
        {
            var show = await _showRepository.GetByIdAsync(request.ShowId);

            if (show == null)
                throw new KeyNotFoundException("Show not found.");

            if (request.SeatIds == null || request.SeatIds.Count == 0)
                throw new InvalidOperationException("Select at least one seat.");

            if (request.SeatIds.Distinct().Count() != request.SeatIds.Count)
                throw new InvalidOperationException("Duplicate seats are not allowed.");

            var validSeats = await _bookingRepository
                .GetSeatsByScreenAndIdsAsync(show.ScreenId, request.SeatIds);

            if (validSeats.Count() != request.SeatIds.Count)
                throw new InvalidOperationException(
                    "One or more requested seats are invalid for this show.");

            var isAlreadyBooked = await _bookingRepository
                .IsAnySeatBookedAsync(request.ShowId, request.SeatIds);

            if (isAlreadyBooked)
                throw new InvalidOperationException(
                    "One or more requested seats are already booked.");

            decimal totalAmount = show.TicketPrice * request.SeatIds.Count;

            var booking = new Booking
            {
                BookingReference = "MOV" + Guid.NewGuid().ToString("N")[..8].ToUpper(),
                ShowId = request.ShowId,
                UserId = userId,
                CustomerName = request.CustomerName,
                CustomerEmail = request.CustomerEmail,
                CustomerPhone = request.CustomerPhone,
                BookingDate = DateTime.UtcNow,
                TotalAmount = totalAmount,
                Status = "Confirmed"
            };

            await _bookingRepository.AddAsync(booking);
            await _bookingRepository.SaveChangesAsync();

            foreach (var seatId in request.SeatIds)
            {
                await _bookingRepository.AddBookingSeatAsync(new BookingSeat
                {
                    BookingId = booking.BookingId,
                    SeatId = seatId,
                    Price = show.TicketPrice
                });
            }

            await _bookingRepository.SaveChangesAsync();

            var savedBooking = await _bookingRepository.GetByIdAsync(booking.BookingId);

            return MapToResponse(savedBooking ?? booking);
        }

        public async Task<bool> CancelBookingAsync(
            int id, int userId, bool isAdmin)
        {
            var booking = await _bookingRepository.GetByIdAsync(id);

            if (booking == null)
                return false;

            if (!isAdmin && booking.UserId != userId)
                return false;

            if (booking.Status == "Cancelled")
                return false;

            booking.Status = "Cancelled";

            await _bookingRepository.UpdateAsync(booking);
            await _bookingRepository.SaveChangesAsync();

            return true;
        }

        private BookingResponse MapToResponse(Booking booking)
        {
            return new BookingResponse
            {
                BookingId = booking.BookingId,
                BookingReference = booking.BookingReference,
                ShowId = booking.ShowId,
                MovieTitle = booking.Show?.Movie?.Title ?? string.Empty,
                TheatreName = booking.Show?.Screen?.Theater?.Name ?? string.Empty,
                ShowStartTime = booking.Show?.StartTime ?? default,
                CustomerName = booking.CustomerName,
                CustomerEmail = booking.CustomerEmail,
                CustomerPhone = booking.CustomerPhone,
                BookingDate = booking.BookingDate,
                TotalAmount = booking.TotalAmount,
                Status = booking.Status,
                Seats = booking.BookingSeats?
                    .Select(bs => $"{bs.Seat?.RowName}{bs.Seat?.SeatNumber}")
                    .ToList() ?? new List<string>()
            };
        }
    }
}
