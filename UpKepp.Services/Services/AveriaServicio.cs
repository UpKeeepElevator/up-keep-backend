using System.Net.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing.Constraints;
using UpKeep.Data.Configuration;
using UpKeep.Data.Contracts;
using UpKeep.Data.DTO;
using UpKeep.Data.DTO.Core.Ascensores;
using UpKeep.Data.DTO.Core.Averias;
using UpKeep.Data.DTO.Core.Usuarios;
using UpKeep.Data.Exceptions.Conflict;
using UpKeep.Data.Exceptions.NotFound;
using UpKeep.Data.Models;
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

        TipoAveriaDto? obj = tipoAverias.FirstOrDefault(x => x.TipoAveriaId == registroRequest.TipoAveriaId);
        if (obj == null)
            throw new GenericNotFound("Tipo de Averia no encontrado");

        //Buscar ascensor
        AscensorDto ascensor = await _repositorioManager.ascensorRepositorio.GetAscensor(registroRequest.AscensorId);

        //Registrar averia

        //Subir Evidencia
        string FolderPath = $"{BucketConfig.AveriaFolder()}/{registroRequest.AscensorId}/evidencias";
        var fileExt = Path.GetExtension(registroRequest.Evidencia.FileName);
        string objName =
            $"{FolderPath}/averia-evidencia-{registroRequest.FechaReporte.ToString("yyyy-MM-dd")}{fileExt}";

        var s3obj = await S3Object.CrearObjeto(objName, registroRequest.Evidencia);

        S3ResponseDto s3Response = await _repositorioManager.BucketRepositorio.UploadFileAsync(s3obj);

        Averium averia = new()
        {
            AscensorId = registroRequest.AscensorId,
            TipoAveriaId = registroRequest.TipoAveriaId,
            FechaReporte = registroRequest.FechaReporte,
            Evidencia = $"{objName}",
            ComentarioAveria = registroRequest.ComentarioAveria,
        };

        bool exito = await _repositorioManager.averiaRepositorio.ReportarAveria(averia);

        return exito;
    }

    public async Task<bool> CerrarAveria(AveriaCierreRequest cierreRequest)
    {
        AveriaDto averia = await _repositorioManager.averiaRepositorio.GetAveria(cierreRequest.AveriaId);

        await _repositorioManager.usuarioRepositorio.GetUsuario(cierreRequest.TecnicoId);

        //BuscarSeccion
        SeccionAscensorDto secciones =
            await _repositorioManager.ascensorRepositorio.GetSeccionAscensor(cierreRequest.SeccionAveria);


        int contador = 1;
        foreach (IFormFile cierreRequestAnexo in cierreRequest.Anexos)
        {
            Guid anexoId = Guid.NewGuid();
            //Subir Evidencia
            string FolderPath = $"{BucketConfig.AveriaFolder()}/{averia.AscensorId}/reparacion";
            var fileExt = Path.GetExtension(cierreRequestAnexo.FileName);
            string objName =
                $"{FolderPath}/averia-{cierreRequest.AveriaId}/anexo-{anexoId}{fileExt}";

            var s3obj = await S3Object.CrearObjeto(objName, cierreRequestAnexo);

            S3ResponseDto s3Response = await _repositorioManager.BucketRepositorio.UploadFileAsync(s3obj);
            contador++;

            AnexoAverium anexo = new()
            {
                AnexoId = anexoId,
                AnexoNombre = "Evidencia",
                AnexoTipo = "Foto",
                AnexoRuta = objName,
                AveriaId = cierreRequest.AveriaId,
                AnexoPeso = s3obj.FileWeight,
            };

            bool exito = await _repositorioManager.averiaRepositorio.AgregarAnexoAveria(anexo);
        }


        //Cerrar
        await _repositorioManager.averiaRepositorio.CerrarAveria(cierreRequest);

        return true;
    }

    public async Task<AveriaDto> AsignarTecnicoAveria(AveriaAsignacionRequest asignacionRequest)
    {
        UsuarioDTO usuario = await _repositorioManager.usuarioRepositorio.GetUsuario(asignacionRequest.TecnicoId);
        int? rolId = usuario.Roles.Select(x => x.RolId).FirstOrDefault(x => x == 1);
        if (rolId == null)
            throw new GenericConflict($"Rol de usuario-{asignacionRequest.TecnicoId} no tiene rol valido");

        await _repositorioManager.averiaRepositorio.AsignarTecnicoAveria(asignacionRequest);

        return await _repositorioManager.averiaRepositorio.GetAveria(asignacionRequest.AveriaId);
    }

    public async Task<IEnumerable<AveriaDto>> GetAverias()
    {
        return await _repositorioManager.averiaRepositorio.GetAverias();
    }

    public async Task<IEnumerable<AveriaDto>> GetAveriasCliente(int clienteId)
    {
        return await _repositorioManager.averiaRepositorio.GetAveriasCliente(clienteId);
    }

    public async Task<IEnumerable<AveriaDto>> GetAveriasTecnicoAsignadas(int tecnicoId)
    {
        return await _repositorioManager.averiaRepositorio.GetAveriasAsignadasTecnico(tecnicoId);
    }

    public async Task<IEnumerable<AveriaDto>> GetAveriasTecnicoAsignadasActivas(int tecnicoId)
    {
        return await _repositorioManager.averiaRepositorio.GetAveriasTecnicoAsignadasActivas(tecnicoId);
    }

    public async Task<IEnumerable<AveriaDto>> GetAveriasClienteActivas(int clienteId)
    {
        return await _repositorioManager.averiaRepositorio.GetAveriasClienteActivas(clienteId);
    }

    public Task<IEnumerable<AveriaDto>> GetAveriasActivas()
    {
        return _repositorioManager.averiaRepositorio.GetAveriasActivas();
    }

    public async Task<AveriaDto> GetAveria(int averiaId)
    {
        return await _repositorioManager.averiaRepositorio.GetAveria(averiaId);
    }

    public async Task<IEnumerable<TipoAveriaDto>> GetTiposAverias()
    {
        return await _repositorioManager.averiaRepositorio.GetTipoAverias();
    }
}