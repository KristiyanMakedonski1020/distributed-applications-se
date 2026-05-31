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
    public class PlayersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PlayersController(AppDbContext context)
        {
            _context = context;
        }

        
        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] string? firstName,
            [FromQuery] int? minGamesPlayed,
            [FromQuery] string sortBy = "LastName",
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            var query = _context.Players.AsQueryable();

            if (!string.IsNullOrEmpty(firstName))
                query = query.Where(p => p.FirstName.Contains(firstName));

            if (minGamesPlayed.HasValue)
                query = query.Where(p => p.TotalGamesPlayed >= minGamesPlayed.Value);

            query = sortBy.ToLower() switch
            {
                "games" => query.OrderByDescending(p => p.TotalGamesPlayed),
                "firstname" => query.OrderBy(p => p.FirstName),
                _ => query.OrderBy(p => p.LastName)
            };

            var totalItems = await query.CountAsync();
            var players = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            return Ok(new { TotalItems = totalItems, Page = page, Data = players });
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Player player)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _context.Players.AddAsync(player);
            await _context.SaveChangesAsync();
            return Ok(player);
        }
    }
}