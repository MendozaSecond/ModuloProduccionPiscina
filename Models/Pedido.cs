using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ModuloProduccionPiscina.Models
{
    public class Pedido
    {
        [Key] public int IdPedido { get; set; }
        public string? UsuarioId { get; set; }
        public int IdPiscina { get; set; }
        public DateTime FechaConsumo { get; set; }
        public char Estado { get; set; } = 'A';
        public List<PedidoDetalle> Detalles { get; set; } = new List<PedidoDetalle>();
        [ForeignKey("IdPiscina")]
        public Piscina? Piscina { get; set; }
        public int? PiscinaId { get; set; }
    }
}
