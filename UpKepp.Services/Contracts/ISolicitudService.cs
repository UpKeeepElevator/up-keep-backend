using UpKeep.Data.DTO.Core.Solicitudes;

namespace UpKepp.Services.Contracts;

public interface ISolicitudService
{
    Task<bool> SolicitarServicio(SolicitudRequest request);
    Task<IEnumerable<SolicitudDto>> GetSolicitudes();
    Task<SolicitudDto> GetSolicitud(int solicitudId);
    Task<IEnumerable<SolicitudDto>> GetSolicitudesAscensor(int ascensorId);
    Task<bool> AgregarServicio(ServicioRequest request);
    Task<IEnumerable<ServicioDto>> GetServicios();
    Task<IEnumerable<TipoSevicioDto>> GetTipoServicios();
    Task<IEnumerable<PrioridadDto>> GetPrioridades();
}