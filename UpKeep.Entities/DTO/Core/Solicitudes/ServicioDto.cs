namespace UpKeep.Data.DTO.Core.Solicitudes;

public class ServicioDto
{

    public int ServicioId { get; set; }


    public string nombreservicio { get; set; } = null!;
    public string descripcion { get; set; } = null!;

    public int TipoServicioId { get; set; }
    public TipoSevicioDto TipoServicio { get; set; }

}

public class ServicioRequest
{

    public string nombreservicio { get; set; } = null!;
    public string descripcion { get; set; } = null!;

    public int TipoServicioId { get; set; }
}