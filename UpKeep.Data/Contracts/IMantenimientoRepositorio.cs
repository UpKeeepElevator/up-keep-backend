using UpKeep.Data.DTO.Core.Cliente;
using UpKeep.Data.Models;

namespace UpKeep.Data.Contracts;

public interface IMantenimientoRepositorio
{
    Task<MantenimientoDto> GetMantenimiento(int mantenimientoId);
    Task<IEnumerable<MantenimientoDto>> GetMantenimientosTecnico(int tecnicoId);
    Task<IEnumerable<MantenimientoDto>> GetMantenimientos();
    Task<bool> PostMantenimiento(MantenimientoRequest mantenimientoRequest);
    Task<bool> PostChequeoMantenimiento(Chequeo chequeoRequest);
    Task<ChequeoDto> GetChequeo(int chequeoId);
    Task<IEnumerable<EstadoSeccionDto>> GetEstadosSeccion();
}