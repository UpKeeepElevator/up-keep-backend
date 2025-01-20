using UpKeep.Data.DTO.Core.Ascensores;

namespace UpKepp.Services.Contracts;

public interface IRutaService
{
    Task<bool> CrearRuta(RutaRequest request);
    Task<bool> AgregarAscensorARuta(AscensorRutaDto dto);
    Task<RutaDto> GetRuta(string rutaId);
    Task<IEnumerable<RutaDto>> GetRutas();
}