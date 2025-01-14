using UpKeep.Data.DTO.Core.Averias;

namespace UpKepp.Services.Contracts;

public interface IAveriaService
{
    Task<bool> ReportarAveria(AveriaRegistroRequest registroRequest);
    Task<bool> CerrarAveria(AveriaCierreRequest cierreRequest);
    Task<AveriaDto> AsignarTecnicoAveria(AveriaAsignacionRequest asignacionRequest);
    Task<IEnumerable<AveriaDto>> GetAverias();
    Task<IEnumerable<AveriaDto>> GetAveriasCliente(int clienteId);
    Task<IEnumerable<AveriaDto>> GetAveriasTecnicoAsignadas(int tecnicoId);
    Task<IEnumerable<AveriaDto>> GetAveriasTecnicoAsignadasActivas(int tecnicoId);
    Task<IEnumerable<AveriaDto>> GetAveriasClienteActivas(int clienteId);
    Task<IEnumerable<AveriaDto>> GetAveriasActivas();
    Task<AveriaDto> GetAveria(int averiaId);
    Task<IEnumerable<TipoAveriaDto>> GetTiposAverias();
}