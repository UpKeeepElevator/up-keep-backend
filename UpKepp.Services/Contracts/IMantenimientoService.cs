using UpKeep.Data.DTO.Core.Cliente;

namespace UpKepp.Services.Contracts;

public interface IMantenimientoService
{
    Task<bool> PostMantenimiento(MantenimientoRequest mantenimientoRequest);
    Task<bool> PostMantenimientoChequeo(ChequeoRequest chequeoRequest);
    Task<IEnumerable<MantenimientoDto>> GetMantenimientos();
    Task<IEnumerable<MantenimientoDto>> GetMantenimientosTecnico(int tecnicoId);
    Task<MantenimientoDto> GetMantenimiento(int mantenimientoId);
    Task<IEnumerable<EstadoSeccionDto>> GetEstadosSeccion();
    Task<ChequeoDto> GetChequeo(int chequeoId);
}