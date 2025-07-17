using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ModuloProduccionPiscina.Controllers;
// Controlador para manejar las peticiones de ping
// Este controlador es accesible sin autenticación
[AllowAnonymous]
[Route("ping")]
public class PingController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return Ok("pong");
    }
}
