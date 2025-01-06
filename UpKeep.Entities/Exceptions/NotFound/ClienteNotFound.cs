namespace UpKeep.Data.Exceptions.NotFound;

public class ClienteNotFound : NotFoundException
{
    public ClienteNotFound(int clienteId) : base($"Cliente {clienteId} no existe")
    {
    }

    public ClienteNotFound(string clienteNombre) : base($"Cliente {clienteNombre} no existe")
    {
    }
}