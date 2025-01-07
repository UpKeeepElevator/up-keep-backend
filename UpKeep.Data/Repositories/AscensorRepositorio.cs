using Mapster;
using Microsoft.EntityFrameworkCore;
using UpKeep.Data.Context;
using UpKeep.Data.Contracts;
using UpKeep.Data.DTO.Core.Ascensores;
using UpKeep.Data.DTO.Core.Cliente;
using UpKeep.Data.Exceptions.NotFound;
using UpKeep.Data.Models;

namespace UpKeep.Data.Repositories;

public class AscensorRepositorio : RepositorioBase, IAscensorRepositorio
{
    public AscensorRepositorio(UpKeepDbContext mySqlContext) : base(mySqlContext)
    {
    }

    public async Task<AscensorDto> GetAscensor(int ascensorId)
    {
        Ascensor? ascensor = await dbContext.Ascensors.FirstOrDefaultAsync(x => x.AscensorId == ascensorId);
        if (ascensor == null)
            throw new AscensorNotFound(ascensorId);

        AscensorDto ascensorDto = ascensor.Adapt<AscensorDto>();
        //BUG: Corregir query de secciones
        ascensorDto.Secciones = ascensorDto.Secciones.ToList().AsQueryable().ProjectToType<SeccionAscensorDto>();
        ascensorDto.Edificio =
            dbContext.Edificios.First(x => x.EdificioId == ascensorDto.EdificioId).Adapt<EdificioDto>();

        return ascensorDto;
    }

    public Task<bool> AgregarAscensor(AscensorRequest request)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<SeccionDto>> GetSecciones()
    {
        var resultado = dbContext.Seccions;

        await Task.FromResult(resultado);

        return resultado.AsQueryable().ProjectToType<SeccionDto>();
    }
}