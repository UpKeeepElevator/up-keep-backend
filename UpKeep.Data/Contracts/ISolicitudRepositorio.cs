using UpKeep.Data.DTO.Core.Solicitudes;

namespace UpKeep.Data.Contracts;

public interface ISolicitudRepositorio
{
    Task<SolicitudDto> GetSolicitud(int solicitudId);
    Task<IEnumerable<SolicitudDto>> GetSolicitudesAscensor(int ascensorId);
    Task<IEnumerable<SolicitudDto>> GetSolicitudes();
    Task<bool> SolicitarServicio(SolicitudRequest request);
    Task<ServicioDto> GetServicio(int requestServicioId);
    Task<ServicioDto> GetServicio(string nombreservicio);
    Task<IEnumerable<PrioridadDto>> GetPrioridades();
    Task<bool> AgregarServicio(ServicioRequest request);
    Task<IEnumerable<ServicioDto>> GetServicios();
    Task<IEnumerable<TipoSevicioDto>> GetTiposServicios();
}