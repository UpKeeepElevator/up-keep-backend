using UpKeep.Data.Context;
using UpKeep.Data.Contracts;
using UpKeep.Data.DTO.Core.Averias;

namespace UpKeep.Data.Repositories;

public class AveriaRepositorio : RepositorioBase, IAveriaRepositorio
{
    public AveriaRepositorio(UpKeepDbContext mySqlContext) : base(mySqlContext)
    {
    }

    public Task<IEnumerable<TipoAveriaDto>> GetTipoAverias()
    {
        throw new NotImplementedException();
    }

    public Task<bool> ReportarAveria(AveriaRegistroRequest registroRequest)
    {
        throw new NotImplementedException();
    }
}