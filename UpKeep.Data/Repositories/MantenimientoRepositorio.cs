using FluentEmail.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Serilog;
using UpKeep.Data.Configuration;
using UpKeep.Data.Context;
using UpKeep.Data.Contracts;
using UpKeep.Data.DTO.Core.Cliente;
using UpKeep.Data.Exceptions.NotFound;
using UpKeep.Data.Models;

namespace UpKeep.Data.Repositories;

public class MantenimientoRepositorio : RepositorioBase, IMantenimientoRepositorio
{
    private readonly IOptions<BucketConfig> _bucketConfig;

    public MantenimientoRepositorio(UpKeepDbContext mySqlContext, IOptions<BucketConfig> bucketConfig) :
        base(mySqlContext)
    {
    }

    public Task<MantenimientoDto> GetMantenimiento(int mantenimientoId)
    {
        var mantenimientos = dbContext.Mantenimientos
            .Include(x => x.Chequeos)
            .FirstOrDefault(x => x.MantenimientoId == mantenimientoId);
        if (mantenimientos == null)
            throw new MantenimientoNotFound(mantenimientoId);


        var mantenimientoDto = mantenimientos.Adapt<MantenimientoDto>();
        mantenimientoDto.Chequeos = mantenimientoDto.Chequeos.ToList().AsQueryable().ProjectToType<ChequeoDto>();

        return Task.FromResult(mantenimientoDto);
    }

    public Task<IEnumerable<MantenimientoDto>> GetMantenimientosTecnico(int tecnicoId)
    {
        var mantenimientos = dbContext.Mantenimientos
                .Where(x => x.TecnicoId == tecnicoId)
                .OrderByDescending(x => x.Fecha)
            ;

        var mantenimientosDto = mantenimientos.ProjectToType<MantenimientoDto>();

        return Task.FromResult<IEnumerable<MantenimientoDto>>(mantenimientosDto);
    }

    public Task<IEnumerable<MantenimientoDto>> GetMantenimientos()
    {
        var mantenimientos = dbContext.Mantenimientos
                .OrderByDescending(x => x.Fecha)
            ;

        var mantenimientosDto = mantenimientos.ProjectToType<MantenimientoDto>();

        return Task.FromResult<IEnumerable<MantenimientoDto>>(mantenimientosDto);
    }

    public Task<bool> PostMantenimiento(MantenimientoRequest mantenimientoRequest)
    {
        Mantenimiento nuevoMantenimiento = new()
        {
            Fecha = mantenimientoRequest.Fecha,
            TecnicoId = mantenimientoRequest.TecnicoId,
            AscensorId = mantenimientoRequest.AscensorId,
            Duracion = mantenimientoRequest.Duracion,
            Hora = mantenimientoRequest.Hora,
            Firma = mantenimientoRequest.Firma,
            Geolocalizacion = mantenimientoRequest.Geolocalizacion
        };


        try
        {
            dbContext.Mantenimientos.Add(nuevoMantenimiento);
            dbContext.SaveChanges();
            Log.Information("Mantenimiento registrado con exito");
            return Task.FromResult(true);
        }
        catch (Exception e)
        {
            Log.Error("Error registrando mantenimiento: {P1} {P2}", e.Message, e.InnerException);
            throw new Exception("Error registrando mantenimiento: {P1} {P2}");
        }
    }

    public Task<bool> PostChequeoMantenimiento(Chequeo chequeoRequest)
    {
        chequeoRequest.AnexoChequeos.ForEach(anexo =>
        {
            anexo.AnexoRuta =
                $"{_bucketConfig.Value.VultrUrl}/{_bucketConfig.Value.S3Bucket}/{anexo.AnexoRuta}";
        });


        try
        {
            dbContext.Chequeos.Add(chequeoRequest);
            dbContext.SaveChanges();
            Log.Information("Chequeo a mantenimiento-{mantenimientoId} regitrado con exito ",
                chequeoRequest.MantenimientoId);
            return Task.FromResult(true);
        }
        catch (Exception e)
        {
            Log.Error("Error Registrando chequeo a mantenimiento-{chequeoRequest.MantenimientoId}: {P1} {P2}",
                chequeoRequest.MantenimientoId, e.Message, e.InnerException);
            throw new Exception($"Error Registrando chequeo a mantenimiento-{chequeoRequest.MantenimientoId}");
        }
    }

    public Task<ChequeoDto> GetChequeo(int chequeoId)
    {
        var chequeo = dbContext.Chequeos
            .Include(x => x.AnexoChequeos)
            .FirstOrDefault(x => x.ChequeoId == chequeoId);
        if (chequeo is null)
            throw new ChequeoNotFound(chequeoId);

        var chequeoDto = chequeo.Adapt<ChequeoDto>();
        chequeoDto.Anexos = chequeo.AnexoChequeos.ToList().AsQueryable().ProjectToType<AnexoChequeoDto>();

        return Task.FromResult(chequeoDto);
    }

    public Task<IEnumerable<EstadoSeccionDto>> GetEstadosSeccion()
    {
        var estado = dbContext.EstadoSeccions;

        var estadoDto = estado.ProjectToType<EstadoSeccionDto>();

        return Task.FromResult<IEnumerable<EstadoSeccionDto>>(estadoDto);
    }
}