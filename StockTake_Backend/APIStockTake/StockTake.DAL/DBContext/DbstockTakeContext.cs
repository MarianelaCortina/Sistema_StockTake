using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using StockTake.Model;

namespace StockTake.DAL.DBContext;

public partial class DbstockTakeContext : DbContext
{
    public DbstockTakeContext()
    {
    }

    public DbstockTakeContext(DbContextOptions<DbstockTakeContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Categoria> Categoria { get; set; }

    public virtual DbSet<DetalleVenta> DetalleVenta { get; set; }

    public virtual DbSet<Inventario> Inventarios { get; set; }

    public virtual DbSet<Menu> Menus { get; set; }

    public virtual DbSet<MenuRol> MenuRols { get; set; }

    public virtual DbSet<Pedido> Pedidos { get; set; }

    public virtual DbSet<PedidoProducto> PedidoProductos { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Proveedor> Proveedors { get; set; }

    public virtual DbSet<Rol> Rols { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<Venta> Venta { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.IdCategoria).HasName("PK__Categori__8A3D240CEC788B75");

            entity.Property(e => e.IdCategoria).HasColumnName("idCategoria");
            entity.Property(e => e.EsActivo)
                .HasDefaultValue(true)
                .HasColumnName("esActivo");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<DetalleVenta>(entity =>
        {
            entity.HasKey(e => e.IdDetalleVenta).HasName("PK__DetalleV__BFE2843F84008EFC");

            entity.Property(e => e.IdDetalleVenta).HasColumnName("idDetalleVenta");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.IdProducto).HasColumnName("idProducto");
            entity.Property(e => e.IdVenta).HasColumnName("idVenta");
            entity.Property(e => e.Precio)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("precio");
            entity.Property(e => e.Total)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("total");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.DetalleVenta)
                .HasForeignKey(d => d.IdProducto)
                .HasConstraintName("FK__DetalleVe__idPro__778AC167");

            entity.HasOne(d => d.IdVentaNavigation).WithMany(p => p.DetalleVenta)
                .HasForeignKey(d => d.IdVenta)
                .HasConstraintName("FK__DetalleVe__idVen__76969D2E");
        });

        modelBuilder.Entity<Inventario>(entity =>
        {
            entity.HasKey(e => e.IdInventario).HasName("PK__Inventar__8F145B0D82B5077D");

            entity.ToTable("Inventario");

            entity.Property(e => e.IdInventario).HasColumnName("idInventario");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.FechaUltimaModificacion).HasColumnName("fechaUltimaModificacion");
            entity.Property(e => e.IdProducto).HasColumnName("idProducto");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.Inventarios)
                .HasForeignKey(d => d.IdProducto)
                .HasConstraintName("FK__Inventari__idPro__628FA481");
        });

        modelBuilder.Entity<Menu>(entity =>
        {
            entity.HasKey(e => e.IdMenu).HasName("PK__Menu__C26AF483F3F36448");

            entity.ToTable("Menu");

            entity.Property(e => e.IdMenu).HasColumnName("idMenu");
            entity.Property(e => e.Icono)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("icono");
            entity.Property(e => e.NombreMenu)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombreMenu");
            entity.Property(e => e.Url)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("url");
        });

        modelBuilder.Entity<MenuRol>(entity =>
        {
            entity.HasKey(e => e.IdMenuRol).HasName("PK__MenuRol__9D6D61A4412D1239");

            entity.ToTable("MenuRol");

            entity.Property(e => e.IdMenuRol).HasColumnName("idMenuRol");
            entity.Property(e => e.IdMenu).HasColumnName("idMenu");
            entity.Property(e => e.IdRol).HasColumnName("idRol");

            entity.HasOne(d => d.IdMenuNavigation).WithMany(p => p.MenuRols)
                .HasForeignKey(d => d.IdMenu)
                .HasConstraintName("FK__MenuRol__idMenu__4E88ABD4");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.MenuRols)
                .HasForeignKey(d => d.IdRol)
                .HasConstraintName("FK__MenuRol__idRol__4F7CD00D");
        });

        modelBuilder.Entity<Pedido>(entity =>
        {
            entity.HasKey(e => e.IdPedido).HasName("PK__Pedido__A9F619B7FDB9C005");

            entity.ToTable("Pedido");

            entity.HasIndex(e => e.NumeroPedido, "UQ__Pedido__90DD61499F969A83").IsUnique();

            entity.Property(e => e.IdPedido).HasColumnName("idPedido");
            entity.Property(e => e.EstadoPedido)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("estadoPedido");
            entity.Property(e => e.FechaEntregaPedido)
                .HasColumnType("datetime")
                .HasColumnName("fechaEntregaPedido");
            entity.Property(e => e.FechaPedido)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaPedido");
            entity.Property(e => e.IdProducto).HasColumnName("idProducto");
            entity.Property(e => e.IdProveedor).HasColumnName("idProveedor");
            entity.Property(e => e.NumeroPedido)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("numeroPedido");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.Pedidos)
                .HasForeignKey(d => d.IdProducto)
                .HasConstraintName("FK__Pedido__idProduc__6A30C649");

            entity.HasOne(d => d.IdProveedorNavigation).WithMany(p => p.Pedidos)
                .HasForeignKey(d => d.IdProveedor)
                .HasConstraintName("FK__Pedido__idProvee__6B24EA82");
        });

        modelBuilder.Entity<PedidoProducto>(entity =>
        {
            entity.HasKey(e => new { e.IdPedido, e.IdProducto }).HasName("PK__Pedido_P__998953A4E584D86D");

            entity.ToTable("Pedido_Producto");

            entity.Property(e => e.IdPedido).HasColumnName("idPedido");
            entity.Property(e => e.IdProducto).HasColumnName("idProducto");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");

            entity.HasOne(d => d.IdPedidoNavigation).WithMany(p => p.PedidoProductos)
                .HasForeignKey(d => d.IdPedido)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Pedido_Pr__idPed__6EF57B66");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.PedidoProductos)
                .HasForeignKey(d => d.IdProducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Pedido_Pr__idPro__6FE99F9F");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.IdProducto).HasName("PK__Producto__07F4A132B90CC436");

            entity.ToTable("Producto");

            entity.Property(e => e.IdProducto).HasColumnName("idProducto");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.EsActivo)
                .HasDefaultValue(true)
                .HasColumnName("esActivo");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.IdCategoria).HasColumnName("idCategoria");
            entity.Property(e => e.NombreProd)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombreProd");
            entity.Property(e => e.Precio)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("precio");

            entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdCategoria)
                .HasConstraintName("FK__Producto__idCate__5AEE82B9");
        });

        modelBuilder.Entity<Proveedor>(entity =>
        {
            entity.HasKey(e => e.IdProveedor).HasName("PK__Proveedo__A3FA8E6B17899099");

            entity.ToTable("Proveedor");

            entity.Property(e => e.IdProveedor).HasColumnName("idProveedor");
            entity.Property(e => e.ContactoProveedor)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("contactoProveedor");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.NombreProveedor)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombreProveedor");

            entity.HasMany(d => d.IdProductos).WithMany(p => p.IdProveedors)
                .UsingEntity<Dictionary<string, object>>(
                    "ProveedorProducto",
                    r => r.HasOne<Producto>().WithMany()
                        .HasForeignKey("IdProducto")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Proveedor__idPro__66603565"),
                    l => l.HasOne<Proveedor>().WithMany()
                        .HasForeignKey("IdProveedor")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Proveedor__idPro__656C112C"),
                    j =>
                    {
                        j.HasKey("IdProveedor", "IdProducto").HasName("PK__Proveedo__9385C478E8F508E5");
                        j.ToTable("ProveedorProducto");
                        j.IndexerProperty<int>("IdProveedor").HasColumnName("idProveedor");
                        j.IndexerProperty<int>("IdProducto").HasColumnName("idProducto");
                    });
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("PK__Rol__3C872F769CD0CBD5");

            entity.ToTable("Rol");

            entity.Property(e => e.IdRol).HasColumnName("idRol");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuario__645723A64EFCA042");

            entity.ToTable("Usuario");

            entity.HasIndex(e => e.Email, "UQ__Usuario__AB6E616403C9CE96").IsUnique();

            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");
            entity.Property(e => e.ClaveHash)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("claveHash");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.EmailVerificado)
                .HasDefaultValue(false)
                .HasColumnName("emailVerificado");
            entity.Property(e => e.EsActivo)
                .HasDefaultValue(true)
                .HasColumnName("esActivo");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.IdRol).HasColumnName("idRol");
            entity.Property(e => e.NombreCompleto)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombreCompleto");
            entity.Property(e => e.RefreshToken)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("refreshToken");
            entity.Property(e => e.RefreshTokenExpiry)
                .HasColumnType("datetime")
                .HasColumnName("refreshTokenExpiry");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdRol)
                .HasConstraintName("FK__Usuario__idRol__52593CB8");
        });

        modelBuilder.Entity<Venta>(entity =>
        {
            entity.HasKey(e => e.IdVenta).HasName("PK__Venta__077D56144732AD0A");

            entity.HasIndex(e => e.NumeroDocumento, "UQ__Venta__4CC511E4D0741128").IsUnique();

            entity.Property(e => e.IdVenta).HasColumnName("idVenta");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.NumeroDocumento)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("numeroDocumento");
            entity.Property(e => e.TipoPago)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("tipoPago");
            entity.Property(e => e.Total)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("total");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
