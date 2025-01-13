using Mapster;
using Microsoft.EntityFrameworkCore;
using Serilog;
using UpKeep.Data.Context;
using UpKeep.Data.Contracts;
using UpKeep.Data.DTO.Core.Solicitudes;
using UpKeep.Data.Exceptions.NotFound;
using UpKeep.Data.Models;

namespace UpKeep.Data.Repositories;

public class SolicitudRepositorio : RepositorioBase, ISolicitudRepositorio
{
    public SolicitudRepositorio(UpKeepDbContext mySqlContext) : base(mySqlContext)
    {
    }

    public Task<SolicitudDto> GetSolicitud(int solicitudId)
    {
        var solicitud = dbContext.Solicituds
            .FirstOrDefault(x => x.SolicitudId == solicitudId);
        if (solicitud == null) throw new SolicitudNotFound(solicitudId);


        var solictudDto = solicitud.Adapt<SolicitudDto>();

        return Task.FromResult(solictudDto);
    }

    public Task<IEnumerable<SolicitudDto>> GetSolicitudesAscensor(int ascensorId)
    {
        var solicitudes = dbContext.Solicituds
            .Where(x => x.AscensorId == ascensorId);

        IEnumerable<SolicitudDto> solicitudDtos = solicitudes.ProjectToType<SolicitudDto>();

        return Task.FromResult(solicitudDtos);
    }

    public Task<IEnumerable<SolicitudDto>> GetSolicitudes()
    {
        var solicitudes = dbContext.Solicituds;

        IEnumerable<SolicitudDto> solicitudDtos = solicitudes.ProjectToType<SolicitudDto>();

        return Task.FromResult(solicitudDtos);
    }

    public Task<bool> SolicitarServicio(SolicitudRequest request)
    {
        Solicitud nuevoSolicitud = new()
        {
            TecnicoId = request.TecnicoId,
            AscensorId = request.AscensorId,
            FechaSolicitud = request.FechaSolicitud,
            PrioridadId = request.PrioridadId,
            DescripcionSolicitud = request.DescripcionSolicitud,
            ServicioId = request.ServicioId,
        };

        try
        {
            dbContext.Solicituds.Add(nuevoSolicitud);
            SavesChanges();
            Log.Information("Solicitud de servicio-{P1} para ascensor-{P2} registrada", request.ServicioId, request.AscensorId);

            return Task.FromResult(true);

        }
        catch (Exception e)
        {
            Log.Error("Error solicitud servicio: {P1} {P2}", e.Message, e.InnerException);
            throw new Exception($"Error solicitud servicio-{request.ServicioId}");

        }
    }

    public Task<ServicioDto> GetServicio(int requestServicioId)
    {
        var servicio = dbContext.Servicios
            .Include(x => x.TipoServicio)
            .FirstOrDefault(x => x.ServicioId == requestServicioId);
        if (servicio == null) throw new ServicioNotFound(requestServicioId);

        ServicioDto servicioDto = servicio.Adapt<ServicioDto>();
        servicioDto.TipoServicio = servicio.TipoServicio.Adapt<TipoSevicioDto>();

        return Task.FromResult(servicioDto);
    }

    public Task<IEnumerable<PrioridadDto>> GetPrioridades()
    {
        var prioridades = dbContext.Prioridads;
        IEnumerable<PrioridadDto> prioridadDtos = prioridades.ProjectToType<PrioridadDto>();

        return Task.FromResult(prioridadDtos);
    }

    public Task<ServicioDto> GetServicio(string nombreservicio)
    {
        var servicio = dbContext.Servicios
            .Include(x => x.TipoServicio)
            .FirstOrDefault(x => x.NombreServicio == nombreservicio);
        if (servicio == null) throw new ServicioNotFound(nombreservicio);

        ServicioDto servicioDto = servicio.Adapt<ServicioDto>();
        servicioDto.TipoServicio = servicio.TipoServicio.Adapt<TipoSevicioDto>();

        return Task.FromResult(servicioDto);
    }

    public Task<bool> AgregarServicio(ServicioRequest request)
    {

        Servicio nuevoServicio = new()
        {
            NombreServicio = request.nombreservicio,
            Descripcion = request.descripcion

        };

        try
        {
            dbContext.Servicios.Add(nuevoServicio);
            SavesChanges();
            Log.Information("Servicio-{P1} agregado correctamente", request.nombreservicio);

            return Task.FromResult(true);

        }
        catch (System.Exception e)
        {

            Log.Error("Error agregando servicio: {P1} {P2}", e.Message, e.InnerException);
            throw new Exception($"Error agregar servicio-{request.nombreservicio}");

        }


    }
}