using Microsoft.AspNetCore.Http;
using UpKeep.Data.Configuration;
using UpKeep.Data.Contracts;
using UpKeep.Data.DTO;
using UpKeep.Data.DTO.Core.Cliente;
using UpKeep.Data.Models;
using UpKepp.Services.Contracts;

namespace UpKepp.Services.Services;

public class MantenimientoServicio : ServicioBase, IMantenimientoService
{
    public MantenimientoServicio(IRepositorioManager repositorioManager) : base(repositorioManager)
    {
    }

    public async Task<bool> PostMantenimiento(MantenimientoRequest mantenimientoRequest)
    {
        await _repositorioManager.usuarioRepositorio.GetUsuario(mantenimientoRequest.TecnicoId);
        await _repositorioManager.ascensorRepositorio.GetAscensor(mantenimientoRequest.AscensorId);

        bool exito = await _repositorioManager.mantenimientoRepositorio.PostMantenimiento(mantenimientoRequest);

        return exito;
    }

    public async Task<bool> PostMantenimientoChequeo(ChequeoRequest chequeoRequest)
    {
        await _repositorioManager.mantenimientoRepositorio.GetMantenimiento(chequeoRequest.MantenimientoId);


        List<AnexoChequeo> chequeos = new List<AnexoChequeo>();

        int contador = 1;
        foreach (IFormFile cierreRequestAnexo in chequeoRequest.Anexos)
        {
            Guid anexoId = Guid.NewGuid();
            //Subir Evidencia
            string FolderPath = $"{BucketConfig.MantenimientoFolder()}";
            var fileExt = Path.GetExtension(cierreRequestAnexo.FileName);
            string objName =
                $"{FolderPath}/mantenimiento-{chequeoRequest.MantenimientoId}/anexo-{anexoId}{fileExt}";

            var s3obj = await S3Object.CrearObjeto(objName, cierreRequestAnexo);

            S3ResponseDto s3Response = await _repositorioManager.BucketRepositorio.UploadFileAsync(s3obj);
            contador++;


            AnexoChequeo anexo = new()
            {
                AnexoId = anexoId,
                AnexoNombre = $"Evidencia-{contador}",
                AnexoTipo = "Foto",
                AnexoRuta = objName,
                AnexoPeso = s3obj.FileWeight,
            };
            chequeos.Add(anexo);
        }

        Chequeo nuevoChequeo = new()
        {
            EstadoSeccionId = chequeoRequest.EstadoSeccionId,
            ChequeoComentarios = chequeoRequest.ChequeoComentarios,
            ChequeoFecha = chequeoRequest.ChequeoFecha,
            ChequeoHora = chequeoRequest.ChequeoHora,
            MantenimientoId = chequeoRequest.MantenimientoId,
            SeccionId = chequeoRequest.SeccionId,
            AnexoChequeos = chequeos,
        };

        await _repositorioManager.mantenimientoRepositorio.PostChequeoMantenimiento(nuevoChequeo);


        return true;
    }

    public async Task<IEnumerable<MantenimientoDto>> GetMantenimientos()
    {
        return await _repositorioManager.mantenimientoRepositorio.GetMantenimientos();
    }

    public async Task<IEnumerable<MantenimientoDto>> GetMantenimientosTecnico(int tecnicoId)
    {
        return await _repositorioManager.mantenimientoRepositorio.GetMantenimientosTecnico(tecnicoId);
    }

    public async Task<MantenimientoDto> GetMantenimiento(int mantenimientoId)
    {
        return await _repositorioManager.mantenimientoRepositorio.GetMantenimiento(mantenimientoId);
    }

    public async Task<IEnumerable<EstadoSeccionDto>> GetEstadosSeccion()
    {
        return await _repositorioManager.mantenimientoRepositorio.GetEstadosSeccion();
    }

    public async Task<ChequeoDto> GetChequeo(int chequeoId)
    {
        return await _repositorioManager.mantenimientoRepositorio.GetChequeo(chequeoId);
    }
}