using System;
using System.Collections.Generic;

namespace UpKeep.Data.Models;

public partial class EstadoSeccion
{
    public int EstadoSeccionId { get; set; }

    public string EstadoNombre { get; set; } = null!;

    public string EstadoDescripcion { get; set; } = null!;

    public virtual ICollection<Chequeo> Chequeos { get; set; } = new List<Chequeo>();
}
