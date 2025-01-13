namespace UpKeep.Data.Exceptions.NotFound;

public class ServicioNotFound : NotFoundException
{
    public ServicioNotFound(int servicioId) : base($"Servicio-{servicioId} no encontrado")
    {
    }
    public ServicioNotFound(string servicioId) : base($"Servicio-{servicioId} no encontrado")
    {
    }
}