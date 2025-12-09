using Back.Models;
using Microsoft.EntityFrameworkCore;

namespace Back.Data
{
    public class LocalDbContext : DbContext
    {
        public LocalDbContext(DbContextOptions<LocalDbContext> options) : base(options)
        {
        }

        public DbSet<Patients> Patients { get; set; }
    }
}
