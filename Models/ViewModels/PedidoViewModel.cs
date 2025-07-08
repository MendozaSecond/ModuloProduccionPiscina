using Microsoft.AspNetCore.Mvc.Rendering;
using ModuloProduccionPiscina.Models;

namespace ModuloProduccionPiscina.Models.ViewModels
{
    public class PedidoViewModel
    {
        public Pedido Pedido { get; set; } = new Pedido();
        public List<SelectListItem> Piscinas { get; set; } = new List<SelectListItem>();
        public List<ProductoSeleccionado> ProductosDisponibles { get; set; } = new List<ProductoSeleccionado>();
    }

    public class ProductoSeleccionado
    {
        public int IdProducto { get; set; }
        public string? Nombre { get; set; }
        public decimal Cantidad { get; set; } = 0;
    }
}
