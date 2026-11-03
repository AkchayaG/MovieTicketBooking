
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MovieTicketBooking.Dtos;
using MovieTicketBooking.Interfaces;

namespace MovieTicketBooking.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShowsController : ControllerBase
    {
        private readonly IShowService _showService;

        public ShowsController(IShowService showService)
        {
            _showService = showService;
        }

        // 1. Get All Shows (Public)
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _showService.GetAllShowsAsync());
        }

        // 2. Get Show By ID (Public)
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var show = await _showService.GetShowByIdAsync(id);

            if (show == null)
                return NotFound();

            return Ok(show);
        }

        // 3. Get Shows By Movie (Public)
        [HttpGet("movie/{movieId}")]
        public async Task<IActionResult> GetByMovie(int movieId)
        {
            return Ok(await _showService.GetShowsByMovieIdAsync(movieId));
        }

        // 4. Create Show (Admin Only)
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] CreateShowRequest request)
        {
            var created = await _showService.CreateShowAsync(request);

            return CreatedAtAction(
                nameof(GetById),
                new { id = created.ShowId },
                created);
        }

        // 5. Update Show (Admin Only)
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id,
            [FromBody] CreateShowRequest request)
        {
            var result = await _showService.UpdateShowAsync(id, request);

            if (!result)
                return NotFound();

            return Ok(new { message = "Show updated successfully!" });
        }

        // 6. Delete Show (Admin Only)
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _showService.DeleteShowAsync(id);

            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}
