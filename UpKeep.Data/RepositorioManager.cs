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
    }


    public IUsuarioRepositorio usuarioRepositorio => _usuarioRepositorio.Value;
    public IClienteRepositorio clienteRepositorio => _clienteRepositorio.Value;
    public IBucketRepositorio BucketRepositorio => _bucketServicio.Value;
}