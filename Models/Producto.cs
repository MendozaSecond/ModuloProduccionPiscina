using System.ComponentModel.DataAnnotations;

namespace ModuloProduccionPiscina.Models
{
    public class Producto
    {
        [Key] public int IdProducto { get; set; }
        public string? Nombre { get; set; }
        public string? Unidad { get; set; }
        public char Estado { get; set; } = 'A';
    }
}
