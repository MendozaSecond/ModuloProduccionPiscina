using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ModuloProduccionPiscina.Models;
using ModuloProduccionPiscina.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace ModuloProduccionPiscina.Controllers;

[Authorize]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _context;

    public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    
    public async Task<IActionResult> Dashboard()
    {
        var usuarios = await _context.Users
            .Select(u => u.UserName)
            .ToListAsync();

        var soloNombres = usuarios
            .Where(u => !string.IsNullOrEmpty(u))
            .Select(u => u!.Split('@')[0])
            .ToList();

        var pedidosPorFecha = await _context.Pedidos
            .GroupBy(p => p.FechaConsumo.Date)
            .Select(g => new PedidosPorFecha
            {
                Fecha = g.Key,
                Realizados = g.Count(p => p.Estado == 'A'),
                Anulados = g.Count(p => p.Estado == 'I')
            })
            .OrderByDescending(g => g.Fecha)
            .ToListAsync();

        var piscinas = await _context.Piscinas.CountAsync();
        var productos = await _context.Productos.CountAsync();

        var hoy = DateTime.Today;
        var consumoHoy = await _context.PedidoDetalles
            .Where(d => d.Pedido!.FechaConsumo.Date == hoy && d.Pedido.Estado == 'A')
            .GroupBy(d => d.Producto!.Nombre)
            .Select(g => new ConsumoProducto
            {
                NombreProducto = g.Key,
                Total = g.Sum(x => x.Cantidad)
            }).ToListAsync();

            var inicio = DateTime.Today.AddMonths(-3).Date;
        var rangoFechas = Enumerable.Range(0, (DateTime.Today - inicio).Days + 1)
            .Select(i => inicio.AddDays(i))
            .ToList();

        var consumoRango = await _context.PedidoDetalles
            .Where(d => d.Pedido!.FechaConsumo.Date >= inicio && d.Pedido.Estado == 'A')
            .GroupBy(d => new { d.Producto!.Nombre, Fecha = d.Pedido.FechaConsumo.Date })
            .Select(g => new
            {
                g.Key.Nombre,
                g.Key.Fecha,
                Total = g.Sum(x => x.Cantidad)
            }).ToListAsync();

        var productosRango = consumoRango.Select(c => c.Nombre).Distinct().ToList();
        var consumoTresMeses = new Dictionary<string, List<decimal>>();
        foreach (var prod in productosRango)
        {
            var lista = new List<decimal>();
            foreach (var fecha in rangoFechas)
            {
                var total = consumoRango
                    .Where(c => c.Nombre == prod && c.Fecha == fecha)
                    .Select(c => c.Total)
                    .FirstOrDefault();
                lista.Add(total);
            }
            consumoTresMeses[prod] = lista;
        }

        var viewModel = new DashboardViewModel
        {
            Usuarios = soloNombres,
            PedidosPorFecha = pedidosPorFecha,
            PiscinasRegistradas = piscinas,
            ProductosRegistrados = productos,
            ConsumoProductosHoy = consumoHoy,
            FechasUltimosTresMeses = rangoFechas.Select(f => f.ToString("yyyy-MM-dd")).ToList(),
            ConsumoTresMeses = consumoTresMeses
        };

        return View(viewModel);
    }
}
