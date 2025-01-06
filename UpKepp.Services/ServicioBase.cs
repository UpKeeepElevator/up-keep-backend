using UpKeep.Data.Contracts;

namespace UpKepp.Services;

public class ServicioBase
{
    protected readonly IRepositorioManager _repositorioManager;

    public ServicioBase(IRepositorioManager repositorioManager)
    {
        _repositorioManager = repositorioManager;
    }
}