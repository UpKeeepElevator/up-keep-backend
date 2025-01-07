using UpKeep.Data.Contracts;
using UpKepp.Services.Contracts;

namespace UpKepp.Services.Services;

public class SolicitudServicio : ServicioBase, ISolicitudService
{
    public SolicitudServicio(IRepositorioManager repositorioManager) : base(repositorioManager)
    {
    }
}