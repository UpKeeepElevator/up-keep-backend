using UpKeep.Data.Context;
using UpKeep.Data.Contracts;
using UpKeep.Data.DTO.Core.Ascensores;

namespace UpKeep.Data.Repositories;

public class AscensorRepositorio : RepositorioBase, IAscensorRepositorio
{
    public AscensorRepositorio(UpKeepDbContext mySqlContext) : base(mySqlContext)
    {
    }

    public Task<AscensorDto> GetAscensor(int registroRequestAscensorId)
    {
        throw new NotImplementedException();
    }
}