using System;
using System.Collections.Generic;

namespace UpKeep.Data.Models;

public partial class Rutum
{
    public string RutaId { get; set; } = null!;

    public string NombreRuta { get; set; } = null!;

    public int CantidadAscensores { get; set; }

    public int CantidadVisitas { get; set; }

    public virtual ICollection<AscensorRutum> AscensorRuta { get; set; } = new List<AscensorRutum>();

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
