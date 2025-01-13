using FluentEmail.Core;
using Microsoft.Extensions.Options;
using UpKeep.Data.Configuration;
using UpKeep.Data.Contracts;
using UpKepp.Services.Contracts;
using UpKepp.Services.Services;

namespace UpKepp.Services;

public class ServicioManager : IServicioManager
{
    private readonly Lazy<IUsuarioService> _usuarioServicio;
    private readonly Lazy<IClienteService> _clienteServicio;
    private readonly Lazy<IAscensorService> _ascensorServicio;
    private readonly Lazy<IAveriaService> _averiaServicio;
    private readonly Lazy<ISolicitudService> _solicitudServicio;


    //Configuracion
    private readonly IOptions<BucketConfig> _bucketConfig;
    private readonly IFluentEmail _fluentEmail;
    private readonly IRepositorioManager _repositorioManager;


    public ServicioManager(IRepositorioManager repositorioManager, IOptions<BucketConfig> bucketConfig,
        IFluentEmail fluentEmail, IFluentEmailFactory fluentEmailFactory)
    {
        _bucketConfig = bucketConfig;
        _repositorioManager = repositorioManager;
        _fluentEmail = fluentEmail;

        //Inicializar
        _usuarioServicio = new Lazy<IUsuarioService>(() => new UsuarioServicio(_repositorioManager));
        _clienteServicio = new Lazy<IClienteService>(() => new ClienteServicio(_repositorioManager));
        _ascensorServicio = new Lazy<IAscensorService>(() => new AscensorService(_repositorioManager));
        _averiaServicio = new Lazy<IAveriaService>(() => new AveriaServicio(_repositorioManager));
        _solicitudServicio = new Lazy<ISolicitudService>(() => new SolicitudServicio(_repositorioManager));
    }


    public IUsuarioService UsuarioServicio => _usuarioServicio.Value;
    public IClienteService ClienteServicio => _clienteServicio.Value;
    public IAveriaService AveriaServicio => _averiaServicio.Value;
    public ISolicitudService SolicitudServicio => _solicitudServicio.Value;
    public IAscensorService AscensorServicio => _ascensorServicio.Value;
}