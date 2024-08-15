using Microsoft.EntityFrameworkCore;

namespace WEBAPI.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<DatosSensor> DatosSensores { get; set; }
        public DbSet<ParametrosSensor> ParametrosSensores { get; set; } 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}