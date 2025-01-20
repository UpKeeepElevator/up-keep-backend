using Mapster;
using Microsoft.EntityFrameworkCore;
using Serilog;
using UpKeep.Data.Context;
using UpKeep.Data.Contracts;
using UpKeep.Data.DTO.Core.Ascensores;
using UpKeep.Data.Exceptions.NotFound;
using UpKeep.Data.Models;

namespace UpKeep.Data.Repositories;

public class RutaRepositorio : RepositorioBase, IRutaRepositorio
{
    public RutaRepositorio(UpKeepDbContext mySqlContext) : base(mySqlContext)
    {
    }

    public Task<RutaDto> GetRuta(string requestRutaId)
    {
        var ruta = dbContext.Ruta
            .Include(x => x.AscensorRuta)
            .FirstOrDefault(x => x.RutaId == requestRutaId);
        if (ruta == null)
        {
            throw new RutaNotFound(requestRutaId);
        }

        var rutaDto = ruta.Adapt<RutaDto>();
        rutaDto.Ascensores = ruta.AscensorRuta.AsQueryable().ProjectToType<AscensorRutaDto>().AsEnumerable();
        return Task.FromResult(rutaDto);
    }

    public Task<bool> CrearRuta(RutaRequest request)
    {
        Rutum nuevaRUta = new()
        {
            RutaId = request.RutaId,
            NombreRuta = request.NombreRuta,
            CantidadAscensores = 0,
            CantidadVisitas = 0,
        };
        try
        {
            dbContext.Ruta.Add(nuevaRUta);
            dbContext.SaveChanges();
            Log.Information("Nueva ruta agregada correctametne");
            return Task.FromResult(true);
        }
        catch (Exception e)
        {
            Log.Error("Error registrando ruta: {P1} {P2}", e.Message, e.InnerException);
            throw new Exception("Error registrando ruta");
        }
    }

    public Task<bool> AgregarAscensorARuta(AscensorRutaDto dto)
    {
        Rutum ruta = dbContext.Ruta.First(x => x.RutaId == dto.RutaId);

        AscensorRutum nuevoAscensorRuta = new()
        {
            RutaId = dto.RutaId,
            AscensorId = dto.AscensorId,
            FechaVisita = dto.FechaVisita,
            Orden = dto.Orden,
        };

        try
        {
            ruta.AscensorRuta.Add(nuevoAscensorRuta);

            dbContext.SaveChanges();
            Log.Information("Ascensor-{ascensorId} agregado a la ruta-{rutaId}", dto.AscensorId, dto.RutaId);
            return Task.FromResult(true);
        }
        catch (Exception e)
        {
            Log.Error("Error agregando asensor a ruta: {P1} {P2}", e.Message, e.InnerException);
            throw new Exception("Error agregando asensor a ruta");
        }
    }

    public Task<IEnumerable<RutaDto>> GetRutas()
    {
        var rutas = dbContext.Ruta.ToList();

        var rutasdto = rutas.AsQueryable().ProjectToType<RutaDto>();

        return Task.FromResult(rutasdto.AsEnumerable());
    }
}