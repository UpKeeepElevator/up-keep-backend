using UpKeep.Data.Contracts;
using UpKeep.Data.DTO.Core.Solicitudes;
using UpKepp.Services.Contracts;

namespace UpKepp.Services.Services;

public class SolicitudServicio : ServicioBase, ISolicitudService
{
    public SolicitudServicio(IRepositorioManager repositorioManager) : base(repositorioManager)
    {
    }

    public Task<bool> SolicitarServicio(SolicitudRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<SolicitudDto>> GetSolicitudes()
    {
        throw new NotImplementedException();
    }

    public Task<SolicitudDto> GetSolicitud(int solicitudId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<SolicitudDto>> GetSolicitudesAscensor(int ascensorId)
    {
        throw new NotImplementedException();
    }
}