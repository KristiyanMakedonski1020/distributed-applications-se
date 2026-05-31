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
    public class EscapeRoomsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EscapeRoomsController(AppDbContext context)
        {
            _context = context;
        }

        
        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] int? minDifficulty,
            [FromQuery] decimal? maxPrice,
            [FromQuery] string sortBy = "Name",
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            var query = _context.EscapeRooms.AsQueryable();

            if (minDifficulty.HasValue)
                query = query.Where(r => r.DifficultyLevel >= minDifficulty.Value);

            if (maxPrice.HasValue)
                query = query.Where(r => r.PricePerPerson <= maxPrice.Value);

            query = sortBy.ToLower() switch
            {
                "price" => query.OrderBy(r => r.PricePerPerson),
                "difficulty" => query.OrderBy(r => r.DifficultyLevel),
                _ => query.OrderBy(r => r.Name)
            };

            var totalItems = await query.CountAsync();
            var rooms = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            return Ok(new { TotalItems = totalItems, Page = page, Data = rooms });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var room = await _context.EscapeRooms.FindAsync(id);
            if (room == null) return NotFound();
            return Ok(room);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EscapeRoom room)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _context.EscapeRooms.AddAsync(room);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = room.Id }, room);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] EscapeRoom updatedRoom)
        {
            if (id != updatedRoom.Id) return BadRequest();

            _context.Entry(updatedRoom).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var room = await _context.EscapeRooms.FindAsync(id);
            if (room == null) return NotFound();

            _context.EscapeRooms.Remove(room);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}