using Microsoft.EntityFrameworkCore;
using PrestadorOnline.Models;
using Pomelo.EntityFrameworkCore.MySql;

namespace PrestadorOnline.Data
{
    public class ServicosDbContext : DbContext
    {
        public ServicosDbContext(DbContextOptions<ServicosDbContext> options)
            : base(options)
        {
        }
        public DbSet<Servico> Servico { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Servico>(entity =>
            {
                modelBuilder.Entity<Servico>()
                    .HasKey(e => e.servicoId);

                modelBuilder.Entity<Servico>()
                    .Property(e => e.servicoId)
                    .ValueGeneratedOnAdd();
            });
        }
    }
}