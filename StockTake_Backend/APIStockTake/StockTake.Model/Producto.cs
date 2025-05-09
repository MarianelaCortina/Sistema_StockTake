using System;
using System.Collections.Generic;

namespace StockTake.Model;

public partial class Producto
{
    public int IdProducto { get; set; }

    public string? NombreProd { get; set; }

    public int? IdCategoria { get; set; }

    public string? Descripcion { get; set; }

    public decimal? Precio { get; set; }

    public bool? EsActivo { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public virtual ICollection<DetalleVenta> DetalleVenta { get; set; } = new List<DetalleVenta>();

    public virtual Categoria? IdCategoriaNavigation { get; set; }

    public virtual ICollection<Inventario> Inventarios { get; set; } = new List<Inventario>();

    public virtual ICollection<PedidoProducto> PedidoProductos { get; set; } = new List<PedidoProducto>();

    public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();

    public virtual ICollection<Proveedor> IdProveedors { get; set; } = new List<Proveedor>();
}
