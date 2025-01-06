using System;
using System.Collections.Generic;

namespace UpKeep.Data.Models;

public partial class Ascensor
{
    public int AscensorId { get; set; }

    public int? NumeroPisos { get; set; }

    public string? Marca { get; set; }

    public string? Modelo { get; set; }

    public int? Capacidad { get; set; }

    public string? TipoAscensor { get; set; }

    public string? TipoDeUso { get; set; }

    public string? UbicacionEnEdificio { get; set; }

    public string? Geolocalizacion { get; set; }

    public int? EdificioId { get; set; }

    public virtual ICollection<AscensorRutum> AscensorRuta { get; set; } = new List<AscensorRutum>();

    public virtual ICollection<Averium> Averia { get; set; } = new List<Averium>();

    public virtual Edificio? Edificio { get; set; }

    public virtual ICollection<Mantenimiento> Mantenimientos { get; set; } = new List<Mantenimiento>();

    public virtual ICollection<Solicitud> Solicituds { get; set; } = new List<Solicitud>();
}
