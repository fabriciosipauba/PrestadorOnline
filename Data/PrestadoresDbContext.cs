using Microsoft.EntityFrameworkCore;
using PrestadorOnline.Models;
using Pomelo.EntityFrameworkCore.MySql;


namespace PrestadorOnline.Data
{
    public class PrestadoresDbContext : DbContext
    {
        public PrestadoresDbContext(DbContextOptions<PrestadoresDbContext> options) 
            : base(options) 
        { 
        }
        public DbSet<Prestador> Prestadores { get; set; }        
    }
}
