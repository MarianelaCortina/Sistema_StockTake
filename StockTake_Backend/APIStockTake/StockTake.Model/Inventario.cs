using System;
using System.Collections.Generic;

namespace StockTake.Model;

public partial class Inventario
{
    public int IdInventario { get; set; }

    public int? IdProducto { get; set; }

    public int? Cantidad { get; set; }

    public DateOnly? FechaUltimaModificacion { get; set; }

    public virtual Producto? IdProductoNavigation { get; set; }
}
