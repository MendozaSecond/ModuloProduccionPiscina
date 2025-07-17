using ModuloProduccionPiscina.Models;

namespace ModuloProduccionPiscina.Models.ViewModels
{
    public class PedidoDetailsViewModel
    {
        public Pedido Pedido { get; set; } = new Pedido();
        public string? UserName { get; set; }
    }
}