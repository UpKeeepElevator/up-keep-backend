namespace UpKeep.Data.Exceptions.Conflict;

public class UsuarioConflict : ConflictException
{
    public UsuarioConflict(string correo) : base($"Usuario-{correo} already exists.")
    {
    }
}