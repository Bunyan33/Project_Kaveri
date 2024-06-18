using Microsoft.EntityFrameworkCore;
using Project_Kaveri.Models;

namespace Project_Kaveri.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Patient> Patients { get; set; }
    }
}
