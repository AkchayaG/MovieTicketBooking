
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MovieTicketBooking.Dtos;
using MovieTicketBooking.Interfaces;

namespace MovieTicketBooking.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TheatersController : ControllerBase
    {
        private readonly ITheaterService _theaterService;

        public TheatersController(ITheaterService theaterService)
        {
            _theaterService = theaterService;
        }

        // 1. Get All Theaters (Public)
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var theaters = await _theaterService.GetAllTheatersAsync();
            return Ok(theaters);
        }

        // 2. Get Theater By ID (Public)
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var theater = await _theaterService.GetTheaterByIdAsync(id);

            if (theater == null)
                return NotFound();

            return Ok(theater);
        }

        // 3. Create Theater (Admin Only)
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] CreateTheaterRequest request)
        {
            var created = await _theaterService.CreateTheaterAsync(request);

            return CreatedAtAction(
                nameof(GetById),
                new { id = created.TheaterId },
                created);
        }

        // 4. Update Theater (Admin Only)
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id,
            [FromBody] CreateTheaterRequest request)
        {
            var result = await _theaterService.UpdateTheaterAsync(id, request);

            if (!result)
                return NotFound();

            return Ok(new { message = "Theater updated successfully!" });
        }

        // 5. Delete Theater (Admin Only)
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _theaterService.DeleteTheaterAsync(id);

            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}
