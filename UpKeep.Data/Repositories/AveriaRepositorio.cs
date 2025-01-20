using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Serilog;
using UpKeep.Data.Configuration;
using UpKeep.Data.Context;
using UpKeep.Data.Contracts;
using UpKeep.Data.DTO.Core.Averias;
using UpKeep.Data.DTO.Core.Cliente;
using UpKeep.Data.Exceptions.NotFound;
using UpKeep.Data.Models;

namespace UpKeep.Data.Repositories;

public class AveriaRepositorio : RepositorioBase, IAveriaRepositorio
{
    private readonly IOptions<BucketConfig> _bucketConfig;

    public AveriaRepositorio(UpKeepDbContext mySqlContext, IOptions<BucketConfig> bucketConfig) : base(mySqlContext)
    {
        _bucketConfig = bucketConfig;
    }

    public async Task<IEnumerable<TipoAveriaDto>> GetTipoAverias()
    {
        var tiposAverias = dbContext.TipoAveria;

        await Task.FromResult(tiposAverias);

        return tiposAverias.AsQueryable().ProjectToType<TipoAveriaDto>();
    }

    public Task<bool> ReportarAveria(Averium registroRequest)
    {
        registroRequest.Evidencia =
            $"{_bucketConfig.Value.VultrUrl}/{_bucketConfig.Value.S3Bucket}/{registroRequest.Evidencia}";

        try
        {
            dbContext.Add(registroRequest);
            Log.Information("Averia registrado con exito");
            SavesChanges();
            return Task.FromResult(true);
        }
        catch (Exception e)
        {
            Log.Error("Error registrando averia: {P1} {P2}", e.Message, e.InnerException);
            throw new Exception($"Error registrando averia");
        }
    }

    public async Task<AveriaDto> GetAveria(int averiaId)
    {
        Averium averia = await dbContext.Averia
            .Include(x => x.AnexoAveria)
            .FirstAsync(x => x.AveriaId == averiaId);


        AveriaDto averiaDto = averia.Adapt<AveriaDto>();
        averiaDto.AnexoAveria = averia.AnexoAveria.ToList().AsQueryable().ProjectToType<AnexoAveriaDto>();
        return averiaDto;
    }

    public Task CerrarAveria(AveriaCierreRequest cierreRequest)
    {
        Averium? averia = dbContext.Averia.FirstOrDefault(x => x.AveriaId == cierreRequest.AveriaId);
        if (averia == null) throw new GenericNotFound($"Averia-{cierreRequest.AveriaId} no encontrado");

        try
        {
            averia.FechaRespuesta = cierreRequest.FechaRespuesta;
            averia.ErrorEncontrado = cierreRequest.ErrorEncontrado;
            averia.ReparacionRealizada = cierreRequest.ReparacionRealizada;
            averia.SeccionAveria = cierreRequest.SeccionAveria;
            averia.TiempoReparacion = cierreRequest.TiempoReparacion;
            averia.TiempoRespuesta = cierreRequest.TiempoRespuesta;
            averia.Geolocalizacion = cierreRequest.Geolocalizacion;

            SavesChanges();
            Log.Information("Averia registrado con exito");

            return Task.FromResult(true);
        }
        catch (Exception e)
        {
            Log.Error("Error cerrando averia: {P1} {P2}", e.Message, e.InnerException);
            throw new Exception("Error cerrando averia");
        }
    }

    public Task AsignarTecnicoAveria(AveriaAsignacionRequest asignacionRequest)
    {
        Averium? averia = dbContext.Averia.FirstOrDefault(x => x.AveriaId == asignacionRequest.AveriaId);
        if (averia == null) throw new GenericNotFound($"Averia-{asignacionRequest.AveriaId} no encontrado");

        try
        {
            averia.TecnicoId = asignacionRequest.TecnicoId;
            SavesChanges();

            Log.Information("Averia-{P1} asignada a tecnico-{P2}", asignacionRequest.AveriaId, averia.TecnicoId);

            return Task.FromResult(true);
        }
        catch (Exception e)
        {
            Log.Error("Error asignando tecnico: {P1} {P2}", e.Message, e.InnerException);
            throw new Exception($"Error asignando tecnico");
        }
    }

    public Task<IEnumerable<AveriaDto>> GetAverias()
    {
        var averias = dbContext.Averia
            .Include(x => x.AnexoAveria)
            .Join(
                dbContext.Ascensors,
                x => x.AscensorId,
                y => y.AscensorId,
                (averium, ascensor) => new { averium, ascensor }
            )
            .Join(
                dbContext.Edificios,
                x => x.ascensor.EdificioId,
                y => y.EdificioId,
                (averiaAscensor, edificio) => new { averia = averiaAscensor, edificio }
            )
            .Select(x =>
                new AveriaDto
                {
                    AveriaId = x.averia.averium.AveriaId,
                    AscensorId = x.averia.averium.AscensorId,
                    TipoAveriaId = x.averia.averium.TipoAveriaId,
                    FechaReporte = x.averia.averium.FechaReporte,
                    Evidencia = x.averia.averium.Evidencia,
                    ComentarioAveria = x.averia.averium.ComentarioAveria,
                    FechaRespuesta = x.averia.averium.FechaRespuesta,
                    ErrorEncontrado = x.averia.averium.ErrorEncontrado,
                    ReparacionRealizada = x.averia.averium.ReparacionRealizada,
                    SeccionAveria = x.averia.averium.SeccionAveria,
                    TecnicoId = x.averia.averium.TecnicoId,
                    TiempoReparacion = x.averia.averium.TiempoReparacion,
                    TiempoRespuesta = x.averia.averium.TiempoRespuesta,
                    Firma = x.averia.averium.Firma,
                    Geolocalizacion = x.averia.averium.Geolocalizacion,
                    Edificio = new()
                    {
                        EdificioId = x.edificio.EdificioId,
                        Edificio1 = x.edificio.Edificio1,
                        EdificioUbicacion = x.edificio.EdificioUbicacion,
                        Geolocalizacion = x.edificio.Geolocalizacion,
                        ClienteId = x.edificio.ClienteId,
                    },
                });


        return Task.FromResult(averias.AsEnumerable());
    }

    public Task<IEnumerable<AveriaDto>> GetAveriasCliente(int clienteId)
    {
        var averias = dbContext.Averia
            .Join(dbContext.Ascensors,
                x => x.AscensorId,
                x => x.AscensorId,
                (averia, ascensor) => new { averia, ascensor })
            .Join(dbContext.Edificios,
                x => x.ascensor.EdificioId,
                x => x.EdificioId,
                (averiaAscensor, edificio) => new { averia = averiaAscensor, edificio })
            .Where(x => x.edificio.ClienteId == clienteId)
            .Select(x =>
                x.averia.averia);

        IEnumerable<AveriaDto> averiasDto = averias.ProjectToType<AveriaDto>()
            .OrderByDescending(x => x.FechaReporte)
            .AsEnumerable();

        return Task.FromResult(averiasDto);
    }

    public Task<IEnumerable<AveriaDto>> GetAveriasAsignadasTecnico(int tecnicoId)
    {
        var averias = dbContext.Averia
            .Where(averia => averia.TecnicoId == tecnicoId);

        IEnumerable<AveriaDto> averiasDto = averias.ProjectToType<AveriaDto>().AsEnumerable();

        return Task.FromResult(averiasDto);
    }

    public Task<IEnumerable<AveriaDto>> GetAveriasTecnicoAsignadasActivas(int tecnicoId)
    {
        var averias = dbContext.Averia
            .Join(
                dbContext.Ascensors,
                x => x.AscensorId,
                y => y.AscensorId,
                (averium, ascensor) => new { averium, ascensor }
            )
            .Join(
                dbContext.Edificios,
                x => x.ascensor.EdificioId,
                y => y.EdificioId,
                (averiaAscensor, edificio) => new { averia = averiaAscensor, edificio }
            )
            .Where(x => x.averia.averium.ErrorEncontrado == null)
            .Where(averia => averia.averia.averium.TecnicoId == tecnicoId)
            .Select(x =>
                new AveriaDto
                {
                    AveriaId = x.averia.averium.AveriaId,
                    AscensorId = x.averia.averium.AscensorId,
                    TipoAveriaId = x.averia.averium.TipoAveriaId,
                    FechaReporte = x.averia.averium.FechaReporte,
                    Evidencia = x.averia.averium.Evidencia,
                    ComentarioAveria = x.averia.averium.ComentarioAveria,
                    FechaRespuesta = x.averia.averium.FechaRespuesta,
                    ErrorEncontrado = x.averia.averium.ErrorEncontrado,
                    ReparacionRealizada = x.averia.averium.ReparacionRealizada,
                    SeccionAveria = x.averia.averium.SeccionAveria,
                    TecnicoId = x.averia.averium.TecnicoId,
                    TiempoReparacion = x.averia.averium.TiempoReparacion,
                    TiempoRespuesta = x.averia.averium.TiempoRespuesta,
                    Firma = x.averia.averium.Firma,
                    Geolocalizacion = x.averia.averium.Geolocalizacion,
                    Edificio = new()
                    {
                        EdificioId = x.edificio.EdificioId,
                        Edificio1 = x.edificio.Edificio1,
                        EdificioUbicacion = x.edificio.EdificioUbicacion,
                        Geolocalizacion = x.edificio.Geolocalizacion,
                        ClienteId = x.edificio.ClienteId,
                    },
                });

        ;


        return Task.FromResult(averias.AsEnumerable());
    }

    public Task<IEnumerable<AveriaDto>> GetAveriasActivas()
    {
        var averias = dbContext.Averia
            .Join(
                dbContext.Ascensors,
                x => x.AscensorId,
                y => y.AscensorId,
                (averium, ascensor) => new { averium, ascensor }
            )
            .Join(
                dbContext.Edificios,
                x => x.ascensor.EdificioId,
                y => y.EdificioId,
                (averiaAscensor, edificio) => new { averia = averiaAscensor, edificio }
            )
            .Where(x => x.averia.averium.ErrorEncontrado == null)
            .Select(x =>
                new AveriaDto
                {
                    AveriaId = x.averia.averium.AveriaId,
                    AscensorId = x.averia.averium.AscensorId,
                    TipoAveriaId = x.averia.averium.TipoAveriaId,
                    FechaReporte = x.averia.averium.FechaReporte,
                    Evidencia = x.averia.averium.Evidencia,
                    ComentarioAveria = x.averia.averium.ComentarioAveria,
                    FechaRespuesta = x.averia.averium.FechaRespuesta,
                    ErrorEncontrado = x.averia.averium.ErrorEncontrado,
                    ReparacionRealizada = x.averia.averium.ReparacionRealizada,
                    SeccionAveria = x.averia.averium.SeccionAveria,
                    TecnicoId = x.averia.averium.TecnicoId,
                    TiempoReparacion = x.averia.averium.TiempoReparacion,
                    TiempoRespuesta = x.averia.averium.TiempoRespuesta,
                    Firma = x.averia.averium.Firma,
                    Geolocalizacion = x.averia.averium.Geolocalizacion,
                    Edificio = new()
                    {
                        EdificioId = x.edificio.EdificioId,
                        Edificio1 = x.edificio.Edificio1,
                        EdificioUbicacion = x.edificio.EdificioUbicacion,
                        Geolocalizacion = x.edificio.Geolocalizacion,
                        ClienteId = x.edificio.ClienteId,
                    },
                });


        return Task.FromResult(averias.AsEnumerable());
    }

    public Task<IEnumerable<AveriaDto>> GetAveriasClienteActivas(int clienteId)
    {
        var averias = dbContext.Averia
            .Join(dbContext.Ascensors,
                x => x.AscensorId,
                x => x.AscensorId,
                (averia, ascensor) => new { averia, ascensor })
            .Join(dbContext.Edificios,
                x => x.ascensor.EdificioId,
                x => x.EdificioId,
                (averiaAscensor, edificio) => new { averiaAscensor = averiaAscensor, edificio })
            .Where(x => x.edificio.ClienteId == clienteId)
            .Where(x => x.averiaAscensor.averia.ErrorEncontrado == null)
            .Select(x =>
                x.averiaAscensor.averia);

        IEnumerable<AveriaDto> averiasDto = averias.ProjectToType<AveriaDto>().AsEnumerable();

        return Task.FromResult(averiasDto);
    }

    public Task<bool> AgregarAnexoAveria(AnexoAverium anexo)
    {
        anexo.AnexoRuta =
            $"{_bucketConfig.Value.VultrUrl}/{_bucketConfig.Value.S3Bucket}/{anexo.AnexoRuta}";
        try
        {
            dbContext.AnexoAveria.Add(anexo);
            SavesChanges();

            Log.Information("Averia-{P1} anexo agregado ", anexo.AveriaId);

            return Task.FromResult(true);
        }
        catch (Exception e)
        {
            Log.Error("Error agregando anexo de averia-{P1}: {P2} {P3}", anexo.AveriaId, e.Message, e.InnerException);
            throw new Exception($"Error agregando anexo de averia");
        }
    }
}