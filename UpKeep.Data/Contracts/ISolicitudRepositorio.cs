using UpKeep.Data.DTO.Core.Solicitudes;

namespace UpKeep.Data.Contracts;

public interface ISolicitudRepositorio
{
    Task<SolicitudDto> GetSolicitud(int solicitudId);
    Task<IEnumerable<SolicitudDto>> GetSolicitudesAscensor(int ascensorId);
    Task<IEnumerable<SolicitudDto>> GetSolicitudes();
    Task<bool> SolicitarServicio(SolicitudRequest request);
    Task<ServicioDto> GetServicio(int requestServicioId);
    Task<IEnumerable<PrioridadDto>> GetPrioridades();
}