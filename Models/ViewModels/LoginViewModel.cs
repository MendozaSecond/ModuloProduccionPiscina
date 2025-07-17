using System.ComponentModel.DataAnnotations;

namespace ModuloProduccionPiscina.Models.ViewModels;

public class LoginViewModel
{
    [Required(ErrorMessage = "El campo {0} es obligatorio.")]
    [EmailAddress(ErrorMessage = "Ingrese un correo electrónico válido.")]
    [Display(Name = "Correo electrónico")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "El campo {0} es obligatorio.")]
    [DataType(DataType.Password)]
    [Display(Name = "Contraseña")]
    public string? Password { get; set; }

    [Display(Name = "Recordarme")]
    public bool RememberMe { get; set; }
}