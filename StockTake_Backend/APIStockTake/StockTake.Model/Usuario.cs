using System;
using System.Collections.Generic;

namespace StockTake.Model;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string? NombreCompleto { get; set; }

    public int? IdRol { get; set; }

    public bool? EsActivo { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public string Email { get; set; } = null!;

    public string ClaveHash { get; set; } = null!;

    public string? RefreshToken { get; set; }

    public DateTime? RefreshTokenExpiry { get; set; }

    public bool? EmailVerificado { get; set; }

    public virtual Rol? IdRolNavigation { get; set; }
}
