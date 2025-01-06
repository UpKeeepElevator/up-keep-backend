using System;
using System.Collections.Generic;

namespace UpKeep.Data.Models;

public partial class Seccion
{
    public int SeccionId { get; set; }

    public string? NombreSeccion { get; set; }

    public int? TipoSeccionId { get; set; }

    public DateTime? UltimaRevision { get; set; }

    public virtual ICollection<Averium> Averia { get; set; } = new List<Averium>();

    public virtual ICollection<Chequeo> Chequeos { get; set; } = new List<Chequeo>();

    public virtual TipoSeccion? TipoSeccion { get; set; }
}
