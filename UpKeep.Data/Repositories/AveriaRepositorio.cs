using Mapster;
using UpKeep.Data.Context;
using UpKeep.Data.Contracts;
using UpKeep.Data.DTO.Core.Averias;

namespace UpKeep.Data.Repositories;

public class AveriaRepositorio : RepositorioBase, IAveriaRepositorio
{
    public AveriaRepositorio(UpKeepDbContext mySqlContext) : base(mySqlContext)
    {
    }

    public async Task<IEnumerable<TipoAveriaDto>> GetTipoAverias()
    {
        var tiposAverias = dbContext.TipoAveria;

        await Task.FromResult(tiposAverias);

        return tiposAverias.AsQueryable().ProjectToType<TipoAveriaDto>();
    }

    public Task<bool> ReportarAveria(AveriaRegistroRequest registroRequest)
    {
        throw new NotImplementedException();
    }
}