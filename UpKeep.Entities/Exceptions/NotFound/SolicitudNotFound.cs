namespace UpKeep.Data.Exceptions.NotFound;

public class SolicitudNotFound: NotFoundException
{
    public SolicitudNotFound(int solicitudId) : base($"Solicitud-{solicitudId} no encontrado")
    {
    }
}