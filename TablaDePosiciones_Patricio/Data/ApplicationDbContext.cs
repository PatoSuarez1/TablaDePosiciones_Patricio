using Microsoft.EntityFrameworkCore;
using TablaDePosiciones_Patricio.Models;

namespace TablaDePosiciones_Patricio.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<User> User { get; set; }
        public DbSet<Team> Team { get; set; }
        public DbSet<TeamRegistration> TeamRegistration { get; set; }
        public DbSet<Match> Match { get; set; }
    }
}
