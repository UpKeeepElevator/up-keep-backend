using UpKeep.Data.DTO.Core.Ascensores;

namespace UpKeep.Data.Contracts;

public interface IAscensorRepositorio
{
    Task<AscensorDto> GetAscensor(int ascensorId);
    Task<bool> AgregarAscensor(AscensorRequest request);
    Task<IEnumerable<SeccionDto>> GetSecciones();
    Task<bool> AgregarSeccionesAscensor(int ascensorId, AscensorRequest request);
    Task<SeccionAscensorDto> GetSeccionAscensor(int cierreRequestSeccionAveria);
    Task<IEnumerable<AscensorDto>> GetAscensoresEdificio(int edificioId);
    Task<IEnumerable<AscensorDto>> GetAscensores();
}