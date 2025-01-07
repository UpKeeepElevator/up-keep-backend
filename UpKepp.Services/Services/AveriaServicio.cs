using UpKeep.Data.Contracts;
using UpKeep.Data.DTO.Core.Ascensores;
using UpKeep.Data.DTO.Core.Averias;
using UpKeep.Data.Exceptions.NotFound;
using UpKepp.Services.Contracts;

namespace UpKepp.Services.Services;

public class AveriaServicio : ServicioBase, IAveriaService
{
    public AveriaServicio(IRepositorioManager repositorioManager) : base(repositorioManager)
    {
    }

    public async Task<bool> ReportarAveria(AveriaRegistroRequest registroRequest)
    {
        //Validar tipo averia
        IEnumerable<TipoAveriaDto> tipoAverias = await _repositorioManager.averiaRepositorio.GetTipoAverias();

        TipoAveriaDto? obj = tipoAverias.FirstOrDefault(x => x.TipoAveriaId == registroRequest.TipoAveria.TipoAveriaId);
        if (obj == null)
            throw new GenericNotFound("Tipo de Averia no encontrado");

        //Buscar ascensor
        AscensorDto ascensor = await _repositorioManager.ascensorRepositorio.GetAscensor(registroRequest.AscensorId);

        //Registrar averia
        bool exito = await _repositorioManager.averiaRepositorio.ReportarAveria(registroRequest);

        return exito;
    }
}