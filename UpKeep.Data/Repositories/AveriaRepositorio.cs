using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Serilog;
using UpKeep.Data.Configuration;
using UpKeep.Data.Context;
using UpKeep.Data.Contracts;
using UpKeep.Data.DTO.Core.Averias;
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
        Averium averia = await dbContext.Averia.FirstAsync(x => x.AveriaId == averiaId);

        return averia.Adapt<AveriaDto>();
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
        var averias = dbContext.Averia;
        var averiasDto = averias.ProjectToType<AveriaDto>().AsEnumerable();
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