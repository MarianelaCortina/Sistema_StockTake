using Microsoft.EntityFrameworkCore;
using StockTakeAPI.Models;

namespace StockTakeAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Producto> Productos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }

        public DbSet<Rol> Rols { get; set; }

        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<Menu> Menus { get; set; }

        public DbSet<MenuRol> MenuRols { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Categoria>()
                .HasMany(c => c.Productos)
                .WithOne(p => p.Categoria)
                .HasForeignKey(p => p.CategoriaId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Rol>().ToTable("Rol");
            modelBuilder.Entity<Usuario>().ToTable("Usuario");
            modelBuilder.Entity<Menu>().ToTable("Menu");
            modelBuilder.Entity<MenuRol>().ToTable("MenuRol");

            modelBuilder.Entity<MenuRol>()
                .HasOne(mr => mr.Menu)
                .WithMany(m => m.MenuRoles)
                .HasForeignKey(mr => mr.MenuId);

            modelBuilder.Entity<MenuRol>()
                .HasOne(mr => mr.Rol)
                .WithMany(r => r.MenuRoles)
                .HasForeignKey(mr => mr.RolId);

            modelBuilder.Entity<Usuario>()
                .HasOne(u => u.Rol)
                .WithMany(r => r.Usuarios)
                .HasForeignKey(u => u.RolId);
        }

    }
}
