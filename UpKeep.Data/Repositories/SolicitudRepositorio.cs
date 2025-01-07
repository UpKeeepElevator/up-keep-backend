using UpKeep.Data.Context;
using UpKeep.Data.Contracts;

namespace UpKeep.Data.Repositories;

public class SolicitudRepositorio : RepositorioBase, ISolicitudRepositorio
{
    public SolicitudRepositorio(UpKeepDbContext mySqlContext) : base(mySqlContext)
    {
    }
}