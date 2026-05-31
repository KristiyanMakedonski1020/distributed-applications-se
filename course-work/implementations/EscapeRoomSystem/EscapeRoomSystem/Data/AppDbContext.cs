using EscapeRoomSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace EscapeRoomSystem.Data
{
   public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<EscapeRoom> EscapeRooms { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Booking> Bookings { get; set; }
    }
}