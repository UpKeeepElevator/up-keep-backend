using System.Security.Cryptography.Xml;
using Amazon.Runtime.SharedInterfaces;
using FluentEmail.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Serilog;
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

        ascensorDto.Secciones = ascensorDto.Secciones.ToList().AsQueryable().ProjectToType<SeccionAscensorDto>();
        ascensorDto.Edificio =
            dbContext.Edificios.First(x => x.EdificioId == ascensorDto.EdificioId).Adapt<EdificioDto>();
        ascensorDto.Secciones =
            dbContext.SeccionAscensors
                .Include(x => x.Seccion)
                .Where(x => x.AscensorId == ascensorId)
                .Select(x => new SeccionAscensorDto
                {
                    ParteAscensorId = x.ParteAscensorId,
                    SeccionId = x.SeccionId,
                    NombreSeccion = x.Seccion.NombreSeccion,
                    AscensorId = x.AscensorId,
                    UltimaRevision = x.UltimaRevision
                });

        return ascensorDto;
    }

    public Task<bool> AgregarAscensor(AscensorRequest request)
    {
        Ascensor ascensor = request.Adapt<Ascensor>();

        try
        {
            dbContext.Ascensors.Add(ascensor);

            SavesChanges();
            Log.Information("Ascensor agregado a edificio-{P1}", request.EdificioId);

            return Task.FromResult(true);
        }
        catch (Exception e)
        {
            Log.Error("Error Agregando ascensor: {P1} {P2}", e.Message, e.InnerException);
            throw new Exception("Error Agregando ascensor");
        }
    }

    public async Task<IEnumerable<SeccionDto>> GetSecciones()
    {
        var resultado = dbContext.Seccions;

        await Task.FromResult(resultado);

        return resultado.AsQueryable().ProjectToType<SeccionDto>();
    }

    public async Task<bool> AgregarSeccionesAscensor(int ascensorId, AscensorRequest request)
    {
        try
        {
            var secciones = await GetSecciones();

            secciones.ForEach(x =>
            {
                var sec = new SeccionAscensor
                {
                    SeccionId = x.SeccionId,
                    AscensorId = ascensorId,
                };

                dbContext.SeccionAscensors.Add(sec);
            });

            SavesChanges();
            Log.Information("Secciones agregadas a ascensor-{P1}", ascensorId);
            return true;
        }
        catch (Exception e)
        {
            Log.Error("Error agregando seecciones de ascensor-{P1}: {P2} ", ascensorId, e.Message);
            throw new Exception($"Error agregando seecciones de ascensor-{ascensorId}");
        }
    }

    public Task<SeccionAscensorDto> GetSeccionAscensor(int cierreRequestSeccionAveria)
    {
        SeccionAscensor? seccion =
            dbContext.SeccionAscensors.FirstOrDefault(x => x.ParteAscensorId == cierreRequestSeccionAveria);

        if (seccion == null) throw new GenericNotFound($"SeccionAscensor-{cierreRequestSeccionAveria} no encontrada");

        return Task.FromResult(seccion.Adapt<SeccionAscensorDto>());
    }

    public Task<IEnumerable<AscensorDto>> GetAscensoresEdificio(int edificioId)
    {
        var ascensores = dbContext.Ascensors
            .Where(x => x.EdificioId == edificioId)
            .Select(x => new AscensorDto()
            {
                AscensorId = x.AscensorId,
                Capacidad = x.Capacidad,
                Edificio = x.Edificio.Adapt<EdificioDto>(),
                EdificioId = x.EdificioId,
                Geolocalizacion = x.Geolocalizacion,
                Marca = x.Marca,
                Modelo = x.Modelo,
                NumeroPisos = x.NumeroPisos,
                TipoAscensor = x.TipoAscensor,
                TipoDeUso = x.TipoDeUso,
                UbicacionEnEdificio = x.UbicacionEnEdificio
            }).AsEnumerable();


        return Task.FromResult(ascensores);
    }

    public Task<IEnumerable<AscensorDto>> GetAscensores()
    {
        var ascensores = dbContext.Ascensors
            .Select(x => new AscensorDto()
            {
                AscensorId = x.AscensorId,
                Capacidad = x.Capacidad,
                Edificio = x.Edificio.Adapt<EdificioDto>(),
                EdificioId = x.EdificioId,
                Geolocalizacion = x.Geolocalizacion,
                Marca = x.Marca,
                Modelo = x.Modelo,
                NumeroPisos = x.NumeroPisos,
                TipoAscensor = x.TipoAscensor,
                TipoDeUso = x.TipoDeUso,
                UbicacionEnEdificio = x.UbicacionEnEdificio
            }).AsEnumerable();


        return Task.FromResult(ascensores);
    }
}