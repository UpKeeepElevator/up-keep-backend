namespace UpKeep.Data.Contracts;

public interface IRepositorioManager
{
    IBucketRepositorio BucketRepositorio { get; }
    IUsuarioRepositorio usuarioRepositorio { get; }
    IClienteRepositorio clienteRepositorio { get; }
}