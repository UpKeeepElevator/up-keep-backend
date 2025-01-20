using Amazon.Runtime;
using FluentEmail.Core;
using UpKeep.Data.Contracts;
using UpKeep.Data.DTO.Core.Solicitudes;
using UpKeep.Data.Exceptions.Conflict;
using UpKeep.Data.Exceptions.NotFound;
using UpKepp.Services.Contracts;

namespace UpKepp.Services.Services;

public class SolicitudServicio : ServicioBase, ISolicitudService
{
    public SolicitudServicio(IRepositorioManager repositorioManager) : base(repositorioManager)
    {
    }

    public async Task<bool> SolicitarServicio(SolicitudRequest request)
    {
        //Buscar tecnico
        var usuario = await _repositorioManager.usuarioRepositorio.GetUsuario(request.TecnicoId);

        var rolTecnico = usuario.Roles.FirstOrDefault(x => x.RolId == 1);
        if (rolTecnico == null) throw new GenericConflict("Usuario indicado no es tecnico");

        //Buscar ascensor
        await _repositorioManager.ascensorRepositorio.GetAscensor(request.AscensorId);
        //Buscar servicio
        ServicioDto servcio = await _repositorioManager.solicitudRepositorio.GetServicio(request.ServicioId);

        //buscar prioridades
        IEnumerable<PrioridadDto> prioridades = await _repositorioManager.solicitudRepositorio.GetPrioridades();

        PrioridadDto? prioridadElegida = prioridades.FirstOrDefault(x => x.PrioridadId == request.PrioridadId);

        if (prioridadElegida == null) throw new GenericNotFound("Prioridad no encontrada");

        bool exito = await _repositorioManager.solicitudRepositorio.SolicitarServicio(request);

        return exito;
    }

    public async Task<IEnumerable<SolicitudDto>> GetSolicitudes()
    {
        return await _repositorioManager.solicitudRepositorio.GetSolicitudes();
    }

    public async Task<SolicitudDto> GetSolicitud(int solicitudId)
    {
        return await _repositorioManager.solicitudRepositorio.GetSolicitud(solicitudId);
    }

    public async Task<IEnumerable<SolicitudDto>> GetSolicitudesAscensor(int ascensorId)
    {
        return await _repositorioManager.solicitudRepositorio.GetSolicitudesAscensor(ascensorId);
    }

    public async Task<bool> AgregarServicio(ServicioRequest request)
    {
        try
        {
            ServicioDto servicio = await _repositorioManager.solicitudRepositorio.GetServicio(request.nombreservicio);
            throw new GenericConflict("Servicio ya existe");
        }
        catch (ServicioNotFound e)
        {
        }

        bool exito = await _repositorioManager.solicitudRepositorio.AgregarServicio(request);

        return exito;
    }

    public async Task<IEnumerable<ServicioDto>> GetServicios()
    {
        var servicios = await _repositorioManager.solicitudRepositorio.GetServicios();

        return servicios;
    }

    public async Task<IEnumerable<TipoSevicioDto>> GetTipoServicios()
    {
        var tiposServicios = await _repositorioManager.solicitudRepositorio.GetTiposServicios();

        return tiposServicios;
    }

    public async Task<IEnumerable<PrioridadDto>> GetPrioridades()
    {
        return await _repositorioManager.solicitudRepositorio.GetPrioridades();
    }
}