namespace UpKeep.Data.Exceptions.Conflict;

public class GenericConflict : ConflictException
{
    public GenericConflict(string correo) : base(correo)
    {
    }
}