namespace UpKepp.Services.Contracts;

public interface IServicioManager
{
    IUsuarioService UsuarioServicio { get; }
    IClienteService ClienteServicio { get; }
}