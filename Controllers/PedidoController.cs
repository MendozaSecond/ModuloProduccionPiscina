using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ModuloProduccionPiscina.Models;
using Microsoft.AspNetCore.Identity;
using ModuloProduccionPiscina.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace ModuloProduccionPiscina.Controllers
{
    [Authorize]
    public class PedidoController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public PedidoController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Pedido
        public async Task<IActionResult> Index()
        {
            var pedidos = await _context.Pedidos
                .Where(p => p.Estado == 'A')
                .Include(p => p.Detalles)
                .Include(p => p.Detalles!)
                    .ThenInclude(d => d.Producto)
                .Include(p => p.Piscina)
                .ToListAsync();

            return View(pedidos);
        }

        // GET: Pedido/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var pedido = await _context.Pedidos
                .Include(p => p.Piscina)
                .Include(p => p.Detalles)
                    .ThenInclude(d => d.Producto)
                .FirstOrDefaultAsync(p => p.IdPedido == id);

            if (pedido == null) return NotFound();

            return View(pedido);
        }

        // GET: Pedido/Create
        public async Task<IActionResult> Create()
        {
            var viewModel = new PedidoViewModel
            {
                Piscinas = await _context.Piscinas
                                .Where(p => p.Estado == 'A')
                                .Select(p => new SelectListItem { Value = p.IdPiscina.ToString(), Text = p.Nombre })
                                .ToListAsync(),

                ProductosDisponibles = await _context.Productos
                                .Where(p => p.Estado == 'A')
                                .Select(p => new ProductoSeleccionado
                                {
                                    IdProducto = p.IdProducto,
                                    Nombre = p.Nombre
                                }).ToListAsync()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PedidoViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                viewModel.Pedido.UsuarioId = _userManager.GetUserId(User);
                viewModel.Pedido.FechaConsumo = DateTime.Today;

                // Solo productos con cantidad > 0
                var detalles = viewModel.ProductosDisponibles
                                .Where(p => p.Cantidad > 0)
                                .Select(p => new PedidoDetalle
                                {
                                    IdProducto = p.IdProducto,
                                    Cantidad = p.Cantidad
                                }).ToList();

                viewModel.Pedido.Detalles = detalles;
                _context.Add(viewModel.Pedido);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }

        // GET: Pedido/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var pedido = await _context.Pedidos
                .Include(p => p.Detalles)
                .FirstOrDefaultAsync(p => p.IdPedido == id);

            if (pedido == null) return NotFound();

            var productosDb = await _context.Productos
                .Where(p => p.Estado == 'A')
                .ToListAsync();

            var viewModel = new PedidoViewModel
            {
                Pedido = pedido,
                Piscinas = await _context.Piscinas
                    .Where(p => p.Estado == 'A')
                    .Select(p => new SelectListItem
                    {
                        Value = p.IdPiscina.ToString(),
                        Text = p.Nombre
                    }).ToListAsync(),

                ProductosDisponibles = productosDb
                     .Select(p => new ProductoSeleccionado
                     {
                         IdProducto = p.IdProducto,
                         Nombre = p.Nombre,
                         Cantidad = pedido.Detalles
                                         .Where(d => d.IdProducto == p.IdProducto)
                                         .Select(d => d.Cantidad)
                                         .FirstOrDefault()
                     }).ToList()
            };

            return View(viewModel);
        }


        // POST: Pedido/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PedidoViewModel viewModel)
        {
            if (id != viewModel.Pedido.IdPedido) return NotFound();

            if (ModelState.IsValid)
            {
                var pedido = await _context.Pedidos
                    .Include(p => p.Detalles)
                    .FirstOrDefaultAsync(p => p.IdPedido == id);

                if (pedido == null) return NotFound();

                pedido.IdPiscina = viewModel.Pedido.IdPiscina;
                pedido.Estado = 'A';

                // Reemplazar detalles
                pedido.Detalles.Clear();
                foreach (var item in viewModel.ProductosDisponibles.Where(p => p.Cantidad > 0))
                {
                    pedido.Detalles.Add(new PedidoDetalle
                    {
                        IdProducto = item.IdProducto,
                        Cantidad = item.Cantidad
                    });
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }


        // GET: Pedido/Delete/5
        public async Task<IActionResult> Anular(int? id)
        {
            if (id == null) return NotFound();

            var pedido = await _context.Pedidos.FindAsync(id);
            if (pedido == null) return NotFound();

            pedido.Estado = 'I';
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        // POST: Pedido/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pedido = await _context.Pedidos.FindAsync(id);
            if (pedido != null)
            {
                _context.Pedidos.Remove(pedido);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PedidoExists(int id)
        {
            return _context.Pedidos.Any(e => e.IdPedido == id);
        }

    }
    
}
