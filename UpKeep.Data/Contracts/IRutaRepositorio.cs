using UpKeep.Data.DTO.Core.Ascensores;

namespace UpKeep.Data.Contracts;

public interface IRutaRepositorio
{
    Task<RutaDto> GetRuta(string requestRutaId);
    Task<bool> CrearRuta(RutaRequest request);
    Task<bool> AgregarAscensorARuta(AscensorRutaDto dto);
    Task<IEnumerable<RutaDto>> GetRutas();
}