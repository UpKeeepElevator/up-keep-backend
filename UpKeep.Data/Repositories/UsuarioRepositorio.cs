using UpKeep.Data.Context;
using UpKeep.Data.Contracts;

namespace UpKeep.Data.Repositories;

public class UsuarioRepositorio : RepositorioBase, IUsuarioRepositorio
{
    public UsuarioRepositorio(UpKeepDbContext mySqlContext) : base(mySqlContext)
    {
    }
}