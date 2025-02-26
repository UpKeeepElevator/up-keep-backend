﻿using System;
using System.Collections.Generic;

namespace UpKeep.Data.Models;

public partial class TipoAverium
{
    public int TipoAveriaId { get; set; }

    public string? TipoNombre { get; set; }

    public string? TipoDescripcion { get; set; }

    public virtual ICollection<Averium> Averia { get; set; } = new List<Averium>();
}
