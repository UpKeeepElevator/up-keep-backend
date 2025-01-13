using UpKeep.Data.DTO.Core.Ascensores;

namespace UpKepp.Services.Contracts;

public interface IAscensorService
{
    Task<bool> AgregarAscensor(AscensorRequest request);
    Task<bool> AgregarSeccionesAscensor(int ascensorId, AscensorRequest request);
    Task<AscensorDto> GetAscensor(int ascensorId);
    Task<IEnumerable<AscensorDto>> GetAscensoresEdificio(int edificioId);
}