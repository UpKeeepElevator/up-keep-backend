using UpKeep.Data.DTO.Core.Ascensores;

namespace UpKepp.Services.Contracts;

public interface IAscensorService
{
    Task<bool> AgregarAscensor(AscensorRequest request);
}