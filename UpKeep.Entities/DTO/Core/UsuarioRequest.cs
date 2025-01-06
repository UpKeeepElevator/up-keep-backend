namespace UpKeep.Data.DTO.Core;

public class UsuarioRequest : AuthUsuario
{
    public string Nombres { get; set; }
    public string? Telefono { get; set; }
    public string salt { get; set; } = "";

    public IEnumerable<RolDto> Roles { get; set; } = new List<RolDto>();

    public bool Validar()
    {
        bool valido = true;
        cuenta = cuenta.ToLower();

        valido = !string.IsNullOrWhiteSpace(Nombres);
        valido = !string.IsNullOrWhiteSpace(cuenta);


        return valido;
    }
}