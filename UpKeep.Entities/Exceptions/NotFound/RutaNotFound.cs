namespace UpKeep.Data.Exceptions.NotFound;

public class RutaNotFound : NotFoundException
{
    public RutaNotFound(string rutaId) : base($"Ruta-{rutaId} no encontrada")
    {
    }
}