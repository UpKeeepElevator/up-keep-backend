namespace UpKeep.Data.Exceptions;

public abstract class NotFoundException : Exception
{
    protected NotFoundException(string cuenta)
        : base(cuenta)
    {
    }
}