using UpKeep.Data.Contracts;
using UpKeep.Data.DTO.Core.Ascensores;
using UpKepp.Services.Contracts;

namespace UpKepp.Services.Services;

public class AscensorService : ServicioBase, IAscensorService
{
    public AscensorService(IRepositorioManager repositorioManager) : base(repositorioManager)
    {
    }

    public Task<bool> AgregarAscensor(AscensorRequest request)
    {
        //Buscar Edificio
        //Agregar ascensor
        //Agregar secciones
        throw new NotImplementedException();
    }
}