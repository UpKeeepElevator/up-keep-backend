namespace UpKeep.Data.Exceptions.NotFound;

public class MantenimientoNotFound : NotFoundException
{
    public MantenimientoNotFound(int mantenimientoId) : base($"Mantenimiento-{mantenimientoId} no encontrado")
    {
    }
}

public class ChequeoNotFound : NotFoundException
{
    public ChequeoNotFound(int chequeoId) : base($"Chequeo-{chequeoId} no encontrado")
    {
    }
}