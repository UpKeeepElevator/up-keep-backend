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
    Task<bool> AgregarAnexoAveria(AnexoAverium anexo);
}