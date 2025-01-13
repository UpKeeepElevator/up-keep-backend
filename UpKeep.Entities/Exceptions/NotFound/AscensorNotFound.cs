namespace UpKeep.Data.Exceptions.NotFound;

public class AscensorNotFound : NotFoundException
{
    public AscensorNotFound(int ascensorId) : base($"Ascensor-{ascensorId} no encontrado")
    {
    }
}