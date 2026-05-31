using EscapeRoomSystem.Data;
using EscapeRoomSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EscapeRoomSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class BookingsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BookingsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] string? status,
            [FromQuery] int? minGroupSize,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
           
            var query = _context.Bookings
                .Include(b => b.EscapeRoom)
                .Include(b => b.Player)
                .AsQueryable();

           
            if (!string.IsNullOrEmpty(status))
                query = query.Where(b => b.Status == status);

            if (minGroupSize.HasValue)
                query = query.Where(b => b.GroupSize >= minGroupSize.Value);

            var totalItems = await query.CountAsync();
            var bookings = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            return Ok(new { TotalItems = totalItems, Page = page, Data = bookings });
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Booking booking)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var roomExists = await _context.EscapeRooms.AnyAsync(r => r.Id == booking.EscapeRoomId);
            var playerExists = await _context.Players.AnyAsync(p => p.Id == booking.PlayerId);

            if (!roomExists || !playerExists)
                return BadRequest("Invalid room or player ID.");

            await _context.Bookings.AddAsync(booking);
            await _context.SaveChangesAsync();
            return Ok(booking);
        }
    }
}