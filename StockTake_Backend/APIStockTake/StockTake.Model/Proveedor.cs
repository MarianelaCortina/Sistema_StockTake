using System;
using System.Collections.Generic;

namespace StockTake.Model;

public partial class Proveedor
{
    public int IdProveedor { get; set; }

    public string? NombreProveedor { get; set; }

    public string? ContactoProveedor { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();

    public virtual ICollection<Producto> IdProductos { get; set; } = new List<Producto>();
}
