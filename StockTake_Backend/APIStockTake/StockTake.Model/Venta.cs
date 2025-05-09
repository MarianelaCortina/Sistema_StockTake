﻿using System;
using System.Collections.Generic;

namespace StockTake.Model;

public partial class Venta
{
    public int IdVenta { get; set; }

    public string NumeroDocumento { get; set; } = null!;

    public string? TipoPago { get; set; }

    public decimal? Total { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public virtual ICollection<DetalleVenta> DetalleVenta { get; set; } = new List<DetalleVenta>();
}
