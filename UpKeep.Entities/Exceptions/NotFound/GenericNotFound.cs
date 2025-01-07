namespace UpKeep.Data.Exceptions.NotFound;

public class GenericNotFound : NotFoundException
{
    public GenericNotFound(string edificioNombre) : base(edificioNombre)
    {
    }
}