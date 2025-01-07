using UpKeep.Data.DTO.Core.Averias;

namespace UpKeep.Data.Contracts;

public interface IAveriaRepositorio
{
    Task<IEnumerable<TipoAveriaDto>> GetTipoAverias();
    Task<bool> ReportarAveria(AveriaRegistroRequest registroRequest);
}