using UpKeep.Data.Contracts;
using UpKeep.Data.DTO.Core.Ascensores;
using UpKeep.Data.DTO.Core.Cliente;
using UpKeep.Data.Models;
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
        IEnumerable<SeccionDto> secciones = await _repositorioManager.ascensorRepositorio.GetSecciones();

        //BUG: Validar objeto
        //Agregar ascensor
        bool exito = await _repositorioManager.ascensorRepositorio.AgregarAscensor(request);

        // Agregar secciones
        // TODO: Agregar secciones


        return exito;
    }

    public async Task<bool> AgregarSeccionesAscensor(int ascensorId, AscensorRequest request)
    {
        AscensorDto ascensor = await _repositorioManager.ascensorRepositorio.GetAscensor(ascensorId);


        bool exito = await _repositorioManager.ascensorRepositorio.AgregarSeccionesAscensor(ascensorId, request);

        return exito;
    }

    public async Task<AscensorDto> GetAscensor(int ascensorId)
    {
        AscensorDto ascensor = await _repositorioManager.ascensorRepositorio.GetAscensor(ascensorId);

        return ascensor;
    }

    public async Task<IEnumerable<AscensorDto>> GetAscensoresEdificio(int edificioId)
    {
        return await _repositorioManager.ascensorRepositorio.GetAscensoresEdificio(edificioId);
    }

    public async Task<IEnumerable<AscensorDto>> GetAscensores()
    {
        return await _repositorioManager.ascensorRepositorio.GetAscensores();
    }
}