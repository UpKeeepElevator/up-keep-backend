using System;
using System.Collections.Generic;

namespace UpKeep.Data.Models;

public partial class Factura
{
    public int FacturaId { get; set; }

    public DateTime FechaFactura { get; set; }

    public decimal MontoFactura { get; set; }

    public decimal ImpuestoFactura { get; set; }

    public decimal TotalFactura { get; set; }

    public DateTime? FechaPagado { get; set; }

    public int ClienteId { get; set; }

    public virtual Cliente Cliente { get; set; } = null!;
}
