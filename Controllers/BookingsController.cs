using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using MovieTicketBooking.DTOs.Bookings;
using MovieTicketBooking.Services.Interfaces;

namespace MovieTicketBooking.Controllers
{
    [Route("api/bookings")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingsController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        private int GetUserId()
        {
            var userIdClaim =
                User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value
                ?? User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!int.TryParse(userIdClaim, out int userId))
                throw new UnauthorizedAccessException();

            return userId;
        }

        private bool IsAdmin()
        {
            return User.IsInRole("Admin");
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateBooking(
            [FromBody] CreateBookingRequest request)
        {
            try
            {
                int userId = GetUserId();

                var response = await _bookingService
                    .CreateBookingAsync(request, userId);

                return CreatedAtAction(
                    nameof(GetBookingById),
                    new { id = response.BookingId },
                    response);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize(Roles = "Customer")]
        [HttpGet("my-bookings")]
        public async Task<IActionResult> GetMyBookings()
        {
            try
            {
                int userId = GetUserId();

                var bookings = await _bookingService
                    .GetMyBookingsAsync(userId);

                return Ok(bookings);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookingById(int id)
        {
            try
            {
                int userId = GetUserId();

                var booking = await _bookingService
                    .GetBookingByIdAsync(id, userId, IsAdmin());

                if (booking == null)
                    return NotFound();

                return Ok(booking);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllBookings()
        {
            var bookings = await _bookingService.GetAllBookingsAsync();
            return Ok(bookings);
        }

        [Authorize]
        [HttpPut("{id}/cancel")]
        public async Task<IActionResult> CancelBooking(int id)
        {
            try
            {
                int userId = GetUserId();

                bool result = await _bookingService
                    .CancelBookingAsync(id, userId, IsAdmin());

                if (!result)
                    return NotFound(new { message = "Booking not found or cannot be cancelled." });

                return Ok(new { message = "Booking cancelled successfully." });
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
        }
    }
}