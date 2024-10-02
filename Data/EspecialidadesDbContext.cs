using Microsoft.EntityFrameworkCore;
using PrestadorOnline.Models;
using Pomelo.EntityFrameworkCore.MySql;

namespace PrestadorOnline.Data
{
    public class EspecialidadesDbContext : DbContext
    {
        public EspecialidadesDbContext(DbContextOptions<EspecialidadesDbContext> options)
            : base(options)
        {
        }
        public DbSet<Especialidades> Especialidades { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Especialidades>(entity =>
            {
                modelBuilder.Entity<Especialidades>()
                    .HasKey(e => e.especialidadeId);

                modelBuilder.Entity<Especialidades>()
                    .Property(e => e.especialidadeId)
                    .ValueGeneratedOnAdd();
            });
        }
    }
}
