using UpKeep.Data.Contracts;
using UpKeep.Data.DTO.Core.Ascensores;
using UpKeep.Data.Exceptions.Conflict;
using UpKeep.Data.Exceptions.NotFound;
using UpKepp.Services.Contracts;

namespace UpKepp.Services.Services;

public class RutaServicio : ServicioBase, IRutaService
{
    public RutaServicio(IRepositorioManager repositorioManager) : base(repositorioManager)
    {
    }

    public async Task<bool> CrearRuta(RutaRequest request)
    {
        try
        {
            RutaDto ruta = await _repositorioManager.RutaRepositorio.GetRuta(request.RutaId);
            throw new GenericConflict("Ruta ya existe");
        }
        catch (RutaNotFound e)
        {
        }

        return await _repositorioManager.RutaRepositorio.CrearRuta(request);
    }

    public async Task<bool> AgregarAscensorARuta(AscensorRutaDto dto)
    {
        RutaDto ruta = await _repositorioManager.RutaRepositorio.GetRuta(dto.RutaId);
        var ascensor = await _repositorioManager.ascensorRepositorio.GetAscensor(dto.AscensorId);

        return await _repositorioManager.RutaRepositorio.AgregarAscensorARuta(dto);
    }

    public async Task<RutaDto> GetRuta(string rutaId)
    {
        return await _repositorioManager.RutaRepositorio.GetRuta(rutaId);
    }

    public async Task<IEnumerable<RutaDto>> GetRutas()
    {
        return await _repositorioManager.RutaRepositorio.GetRutas();
    }
}