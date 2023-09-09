using Cajero.Models.DAO;
using Microsoft.EntityFrameworkCore;

namespace Cajero.DataAccess
{
    public partial class ContextDB : DbContext
    {
        public ContextDB()
        {
        }
        public ContextDB(DbContextOptions<ContextDB> options) : base(options)
        {
        }
        public DbSet<Tarjeta> Tarjetas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Operaciones> Operaciones { get; set; }
        public DbSet<TipoOperacion> TipoOperacion { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tarjeta>()
               .HasOne(t => t.Usuario) // Una Tarjeta pertenece a un Usuario
               .WithMany(u => u.Tarjetas) // Un Usuario puede tener muchas Tarjetas
               .HasForeignKey(t => t.IdUsuario) // Clave foránea en Tarjeta
               .IsRequired();

            modelBuilder.Entity<Operaciones>()
               .HasOne(t => t.Usuario)
               .WithMany(u => u.Operaciones) 
               .HasForeignKey(t => t.IdUsuario) 
               .IsRequired();

            modelBuilder.Entity<Operaciones>()
               .HasOne(t => t.OperacionTipo) // Una Tarjeta pertenece a un Usuario
               .WithMany(u => u.Operaciones) // Un Usuario puede tener muchas Tarjetas
               .HasForeignKey(t => t.TipoOperacion) // Clave foránea en Tarjeta
               .IsRequired();
        }
    }
}
