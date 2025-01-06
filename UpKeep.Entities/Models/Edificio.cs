using System;
using System.Collections.Generic;

namespace UpKeep.Data.Models;

public partial class Edificio
{
    public int EdificioId { get; set; }

    public string? Edificio1 { get; set; }

    public string? EdificioUbicacion { get; set; }

    public string? Geolocalizacion { get; set; }

    public int ClienteId { get; set; }

    public virtual ICollection<Ascensor> Ascensors { get; set; } = new List<Ascensor>();

    public virtual Cliente Cliente { get; set; } = null!;
}
