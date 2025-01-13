using System;
using System.Collections.Generic;

namespace UpKeep.Data.Models;

public partial class SeccionAscensor
{
    public int SeccionId { get; set; }

    public int AscensorId { get; set; }

    public DateTime? UltimaRevision { get; set; }

    public int ParteAscensorId { get; set; }

    public virtual Ascensor Ascensor { get; set; } = null!;

    public virtual ICollection<Averium> Averia { get; set; } = new List<Averium>();

    public virtual ICollection<Chequeo> Chequeos { get; set; } = new List<Chequeo>();

    public virtual Seccion Seccion { get; set; } = null!;
}
