using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ModuloProduccionPiscina.Models
{
    public class PedidoDetalle
    {
        [Key] public int IdPedidoDetalle { get; set; }
        public int IdPedido { get; set; }
        public int IdProducto { get; set; }
        public decimal Cantidad { get; set; }
        public char Estado { get; set; } = 'A';
        [ForeignKey("IdProducto")]
        public Producto? Producto { get; set; }

        [ForeignKey("IdPedido")]
        public Pedido? Pedido { get; set; }

    }
}
