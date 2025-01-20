namespace UpKeep.Data.Contracts;

public interface IRepositorioManager
{
    IBucketRepositorio BucketRepositorio { get; }
    IUsuarioRepositorio usuarioRepositorio { get; }
    IClienteRepositorio clienteRepositorio { get; }
    IAscensorRepositorio ascensorRepositorio { get; }
    IAveriaRepositorio averiaRepositorio { get; }
    ISolicitudRepositorio solicitudRepositorio { get; }
    IMantenimientoRepositorio mantenimientoRepositorio { get; }
}