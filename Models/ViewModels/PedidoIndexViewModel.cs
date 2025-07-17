using ModuloProduccionPiscina.Models;

namespace ModuloProduccionPiscina.Models.ViewModels
{
    public class PedidoIndexViewModel
    {
        public Pedido Pedido { get; set; } = new Pedido();
        public string? UserName { get; set; }
    }
}