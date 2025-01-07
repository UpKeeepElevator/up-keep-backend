using UpKeep.Data.DTO.Core.Ascensores;

namespace UpKeep.Data.Contracts;

public interface IAscensorRepositorio
{
    Task<AscensorDto> GetAscensor(int ascensorId);
    Task<bool> AgregarAscensor(AscensorRequest request);
    Task<IEnumerable<SeccionDto>> GetSecciones();
}