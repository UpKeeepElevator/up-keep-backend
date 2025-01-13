using System;
using System.Collections.Generic;

namespace UpKeep.Data.Models;

public partial class Seccion
{
    public int SeccionId { get; set; }

    public string NombreSeccion { get; set; } = null!;

    public int? TipoSeccionId { get; set; }

    public DateTime? UltimaRevision { get; set; }

    public virtual ICollection<SeccionAscensor> SeccionAscensors { get; set; } = new List<SeccionAscensor>();

    public virtual TipoSeccion? TipoSeccion { get; set; }
}
