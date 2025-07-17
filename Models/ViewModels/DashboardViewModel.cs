using System;
using System.Collections.Generic;

namespace ModuloProduccionPiscina.Models.ViewModels
{
    public class DashboardViewModel
    {
        public List<string> Usuarios { get; set; } = new List<string>();
        public List<PedidosPorFecha> PedidosPorFecha { get; set; } = new List<PedidosPorFecha>();
        public int PiscinasRegistradas { get; set; }
        public int ProductosRegistrados { get; set; }
        public List<ConsumoProducto> ConsumoProductosHoy { get; set; } = new List<ConsumoProducto>();
        public List<string> FechasUltimosTresMeses { get; set; } = new List<string>();
        public Dictionary<string, List<decimal>> ConsumoTresMeses { get; set; } = new();
    }

    public class PedidosPorFecha
    {
        public DateTime Fecha { get; set; }
        public int Realizados { get; set; }
        public int Anulados { get; set; }
    }

    public class ConsumoProducto
    {
        public string? NombreProducto { get; set; }
        public decimal Total { get; set; }
    }
}