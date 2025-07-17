using Microsoft.AspNetCore.Identity;

namespace ModuloProduccionPiscina.Models;

public class SpanishIdentityErrorDescriber : IdentityErrorDescriber
{
    public override IdentityError PasswordTooShort(int length)
        => new IdentityError
        {
            Code = nameof(PasswordTooShort),
            Description = $"La contraseña debe tener al menos {length} caracteres."
        };

    public override IdentityError PasswordRequiresNonAlphanumeric()
        => new IdentityError
        {
            Code = nameof(PasswordRequiresNonAlphanumeric),
            Description = "La contraseña debe contener un caracter no alfanumérico."
        };

    public override IdentityError PasswordRequiresDigit()
        => new IdentityError
        {
            Code = nameof(PasswordRequiresDigit),
            Description = "La contraseña debe incluir un número."
        };

    public override IdentityError PasswordRequiresLower()
        => new IdentityError
        {
            Code = nameof(PasswordRequiresLower),
            Description = "La contraseña debe tener una letra minúscula."
        };

    public override IdentityError PasswordRequiresUpper()
        => new IdentityError
        {
            Code = nameof(PasswordRequiresUpper),
            Description = "La contraseña debe tener una letra mayúscula."
        };

    public override IdentityError PasswordRequiresUniqueChars(int uniqueChars)
        => new IdentityError
        {
            Code = nameof(PasswordRequiresUniqueChars),
            Description = $"La contraseña debe tener al menos {uniqueChars} caracteres únicos."
        };
}