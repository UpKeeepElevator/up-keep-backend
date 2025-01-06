namespace UpKeep.Data.Exceptions.NotFound;

public class UsuarioNotFound : NotFoundException
{
    public UsuarioNotFound(string cuenta) : base($"usuario-{cuenta} no encontrado")
    {
    }
}