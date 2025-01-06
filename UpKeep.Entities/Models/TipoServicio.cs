using System;
using System.Collections.Generic;

namespace UpKeep.Data.Models;

public partial class TipoServicio
{
    public int TipoServicioId { get; set; }

    public string? Descripcion { get; set; }

    public string? NombreServicio { get; set; }

    public virtual ICollection<Servicio> Servicios { get; set; } = new List<Servicio>();
}
