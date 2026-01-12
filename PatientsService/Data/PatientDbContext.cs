using Back.Models;
using Microsoft.EntityFrameworkCore;

namespace Back.Data
{
    public class PatientDbContext : DbContext
    {
        public PatientDbContext(DbContextOptions<PatientDbContext> options) : base(options)
        {
        }

        public DbSet<Patients> Patients { get; set; }
    }
}
