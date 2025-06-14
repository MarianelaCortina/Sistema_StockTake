﻿namespace StockTakeAPI.DTOs
{
    public class ProductoDTO
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public int CategoriaId { get; set; }
        public string? CategoriaNombre { get; set; }

        public bool EsActivo { get; set; }
    }
}
