using System;
using System.Collections.Generic;

namespace UpKeep.Data.Models;

public partial class Servicio
{
    public int ServicioId { get; set; }

    public string? NombreServicio { get; set; }

    public string? Descripcion { get; set; }

    public int? TipoServicioId { get; set; }

    public virtual ICollection<Solicitud> Solicituds { get; set; } = new List<Solicitud>();

    public virtual TipoServicio? TipoServicio { get; set; }
}
