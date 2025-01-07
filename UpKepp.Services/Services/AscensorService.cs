using UpKeep.Data.Contracts;
using UpKeep.Data.DTO.Core.Ascensores;
using UpKeep.Data.DTO.Core.Cliente;
using UpKepp.Services.Contracts;

namespace UpKepp.Services.Services;

public class AscensorService : ServicioBase, IAscensorService
{
    public AscensorService(IRepositorioManager repositorioManager) : base(repositorioManager)
    {
    }

    public async Task<bool> AgregarAscensor(AscensorRequest request)
    {
        //Buscar Edificio
        EdificioDto edificio = await _repositorioManager.clienteRepositorio.GetEdificio(request.EdificioId);

        //BUG: Validar objeto
        //Agregar ascensor
        bool exito = await _repositorioManager.ascensorRepositorio.AgregarAscensor(request);

        //Agregar secciones
        //TODO: Agregar secciones
        IEnumerable<SeccionDto> secciones = await _repositorioManager.ascensorRepositorio.GetSecciones();

        return exito;
    }
}