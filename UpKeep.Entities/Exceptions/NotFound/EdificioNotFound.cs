namespace UpKeep.Data.Exceptions.NotFound;

public class EdificioNotFound : NotFoundException
{
    public EdificioNotFound(string edificioNombre) : base($"Edificio-{edificioNombre} no encontrado")
    {
    }

    public EdificioNotFound(int edificioId) : base($"Edificio-{edificioId} no encontrado")
    {
    }
}