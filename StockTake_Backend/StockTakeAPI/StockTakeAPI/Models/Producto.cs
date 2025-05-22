using System.Text.Json.Serialization;

namespace StockTakeAPI.Models
{
        public class Producto
        {
            public int Id { get; set; }
            public string? Nombre { get; set; }
            public string? Descripcion { get; set; }
            public decimal Precio { get; set; }
            public int Stock { get; set; } // 👈 Nuevo atributo agregado
            
            public bool EsActivo { get; set; }
            public DateTime FechaRegistro { get; set; } = DateTime.Now;
    
            //relación n:1 muchos productos pertenecen a una categoría
            public int CategoriaId { get; set; }
           
            [JsonIgnore]
            public Categoria? Categoria { get; set; }
        }


}
