using UpKeep.Data.DTO.Core.Averias;
using UpKeep.Data.Models;

namespace UpKeep.Data.Contracts;

public interface IAveriaRepositorio
{
    Task<IEnumerable<TipoAveriaDto>> GetTipoAverias();
    Task<bool> ReportarAveria(Averium registroRequest);
    Task<AveriaDto> GetAveria(int averiaId);
    Task CerrarAveria(AveriaCierreRequest cierreRequest);
    Task AsignarTecnicoAveria(AveriaAsignacionRequest asignacionRequest);
    Task<IEnumerable<AveriaDto>> GetAverias();
    Task<IEnumerable<AveriaDto>> GetAveriasCliente(int clienteId);
    Task<IEnumerable<AveriaDto>> GetAveriasAsignadasTecnico(int tecnicoId);
    Task<IEnumerable<AveriaDto>> GetAveriasTecnicoAsignadasActivas(int tecnicoId);
    Task<IEnumerable<AveriaDto>> GetAveriasActivas();
    Task<IEnumerable<AveriaDto>> GetAveriasClienteActivas(int clienteId);
    Task<bool> AgregarAnexoAveria(AnexoAverium anexo);
}