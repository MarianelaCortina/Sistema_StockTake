using System;
using System.Collections.Generic;

namespace StockTake.Model;

public partial class Pedido
{
    public int IdPedido { get; set; }

    public string? NumeroPedido { get; set; }

    public int? IdProducto { get; set; }

    public int? IdProveedor { get; set; }

    public DateTime? FechaPedido { get; set; }

    public DateTime? FechaEntregaPedido { get; set; }

    public string? EstadoPedido { get; set; }

    public virtual Producto? IdProductoNavigation { get; set; }

    public virtual Proveedor? IdProveedorNavigation { get; set; }

    public virtual ICollection<PedidoProducto> PedidoProductos { get; set; } = new List<PedidoProducto>();
}
