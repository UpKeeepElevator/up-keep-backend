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
    }


    public IUsuarioRepositorio usuarioRepositorio => _usuarioRepositorio.Value;
    public IClienteRepositorio clienteRepositorio => _clienteRepositorio.Value;
    public IAscensorRepositorio ascensorRepositorio => _ascensorRepositorio.Value;
    public IAveriaRepositorio averiaRepositorio => _averiaRepositorio.Value;
    public ISolicitudRepositorio solicitudRepositorio => _solicitudRepositorio.Value;
    public IBucketRepositorio BucketRepositorio => _bucketServicio.Value;
}