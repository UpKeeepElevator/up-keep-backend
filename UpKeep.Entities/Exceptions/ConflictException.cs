namespace UpKeep.Data.Exceptions;

public abstract class ConflictException : Exception
{
    protected ConflictException(string correo)
        : base(correo)
    {
    }
}