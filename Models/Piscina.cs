using System.ComponentModel.DataAnnotations;

namespace ModuloProduccionPiscina.Models;
public class Piscina
{
    [Key] public int IdPiscina { get; set; }
    public string? Nombre { get; set; }
    public char Estado { get; set; } = 'A';
}
