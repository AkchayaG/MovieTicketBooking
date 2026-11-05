using Microsoft.AspNetCore.Authorization;
using MovieTicketBooking.Models;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace MovieTicketBooking.Authorization
{
    public class BookingOwnerHandler
        : AuthorizationHandler<BookingOwnerRequirement, Booking>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            BookingOwnerRequirement requirement,
            Booking booking)
        {
            // 1. Admin can access all bookings
            if (context.User.IsInRole("Admin"))
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            // 2. Get logged-in User ID from JWT
            var userIdValue =
                context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                ?? context.User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

            // 3. Validate User ID
            if (!int.TryParse(userIdValue, out int userId))
            {
                return Task.CompletedTask;
            }

            // 4. Check booking ownership
            if (booking.UserId == userId)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
