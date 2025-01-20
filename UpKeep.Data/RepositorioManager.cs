using Microsoft.Extensions.Options;
using UpKeep.Data.Configuration;
using UpKeep.Data.Context;
using UpKeep.Data.Contracts;
using UpKeep.Data.Repositories;

namespace UpKeep.Data;

public class RepositorioManager : IRepositorioManager
{
    private readonly Lazy<IUsuarioRepositorio> _usuarioRepositorio;
    private readonly Lazy<IBucketRepositorio> _bucketServicio;
    private readonly Lazy<IClienteRepositorio> _clienteRepositorio;
    private readonly Lazy<IAscensorRepositorio> _ascensorRepositorio;
    private readonly Lazy<IAveriaRepositorio> _averiaRepositorio;
    private readonly Lazy<ISolicitudRepositorio> _solicitudRepositorio;
    private readonly Lazy<IMantenimientoRepositorio> _mantenimientoRepositorio;
    private readonly Lazy<IRutaRepositorio> _rutaRepositorio;


    //Configuaracion
    private readonly UpKeepDbContext _postgresContext;
    private readonly IOptions<BucketConfig> _bucketConfig;


    public RepositorioManager(UpKeepDbContext postgresContext, IOptions<BucketConfig> bucketConfig)
    {
        _postgresContext = postgresContext;
        _bucketConfig = bucketConfig;

        //inicializar
        _bucketServicio = new Lazy<IBucketRepositorio>(() => new BucketRepositorio(_bucketConfig));
        _usuarioRepositorio =
            new Lazy<IUsuarioRepositorio>(() => new UsuarioRepositorio(_postgresContext));
        _clienteRepositorio =
            new Lazy<IClienteRepositorio>(() => new ClienteRepositorio(_postgresContext));
        _ascensorRepositorio =
            new Lazy<IAscensorRepositorio>(() => new AscensorRepositorio(_postgresContext));
        _averiaRepositorio =
            new Lazy<IAveriaRepositorio>(() => new AveriaRepositorio(_postgresContext, _bucketConfig));
        _solicitudRepositorio =
            new Lazy<ISolicitudRepositorio>(() => new SolicitudRepositorio(_postgresContext));
        _mantenimientoRepositorio =
            new Lazy<IMantenimientoRepositorio>(() => new MantenimientoRepositorio(_postgresContext, _bucketConfig));
        _rutaRepositorio =
            new Lazy<IRutaRepositorio>(() => new RutaRepositorio(_postgresContext));
    }


    public IUsuarioRepositorio usuarioRepositorio => _usuarioRepositorio.Value;
    public IClienteRepositorio clienteRepositorio => _clienteRepositorio.Value;
    public IAscensorRepositorio ascensorRepositorio => _ascensorRepositorio.Value;
    public IAveriaRepositorio averiaRepositorio => _averiaRepositorio.Value;
    public ISolicitudRepositorio solicitudRepositorio => _solicitudRepositorio.Value;
    public IMantenimientoRepositorio mantenimientoRepositorio => _mantenimientoRepositorio.Value;
    public IRutaRepositorio RutaRepositorio => _rutaRepositorio.Value;
    public IBucketRepositorio BucketRepositorio => _bucketServicio.Value;
}