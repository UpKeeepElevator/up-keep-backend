using UpKeep.Data.DTO.Core.Averias;

namespace UpKepp.Services.Contracts;

public interface IAveriaService
{
    Task<bool> ReportarAveria(AveriaRegistroRequest registroRequest);
}