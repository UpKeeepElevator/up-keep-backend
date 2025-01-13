using System;
using System.Collections.Generic;

namespace UpKeep.Data.Models;

public partial class TipoSeccion
{
    public int TipoSeccionId { get; set; }

    public string TipoSeccionNombre { get; set; } = null!;

    public string TipoDescripcion { get; set; } = null!;

    public virtual ICollection<Seccion> Seccions { get; set; } = new List<Seccion>();
}
