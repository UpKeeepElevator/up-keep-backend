namespace UpKepp.Services.Contracts;

public interface IServicioManager
{
    IUsuarioService UsuarioServicio { get; }
    IClienteService ClienteServicio { get; }
    IAveriaService AveriaServicio { get; }
    ISolicitudService SolicitudServicio { get; }
    IAscensorService AscensorServicio { get; }
    IMantenimientoService MantenimientoService { get; }
    IRutaService RutaService { get; }
}