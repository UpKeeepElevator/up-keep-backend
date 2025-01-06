using FluentEmail.Core;

namespace UpKeep.Data.DTO.Core;

public class UsuarioRequest : AuthUsuario
{
    public string Nombres { get; set; }
    public string? Telefono { get; set; }
    public string salt { get; set; } = "";

    public IEnumerable<RolDto> Roles { get; set; } = new List<RolDto>();

    public bool Validar(List<RolDto> rolesReales)
    {
        bool valido = true;
        cuenta = cuenta.ToLower();

        valido = !string.IsNullOrWhiteSpace(Nombres);
        valido = !string.IsNullOrWhiteSpace(cuenta);

        Roles.ForEach(x =>
        {
            var obj = rolesReales.FirstOrDefault(y => y.RolId == x.RolId);
            if (obj == null)
                valido = false;
        });


        return valido;
    }
}