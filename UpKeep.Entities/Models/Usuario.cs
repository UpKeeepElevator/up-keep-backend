using System;
using System.Collections.Generic;

namespace UpKeep.Data.Models;

public partial class Usuario
{
    public int UsuarioId { get; set; }

    public string Correo { get; set; } = null!;

    public string Nombres { get; set; } = null!;

    public string? RutaId { get; set; }

    public string Password { get; set; } = null!;

    public string Salt { get; set; } = null!;

    public string? Telefono { get; set; }

    public virtual ICollection<Averium> Averia { get; set; } = new List<Averium>();

    public virtual ICollection<Cliente> Clientes { get; set; } = new List<Cliente>();

    public virtual ICollection<Mantenimiento> Mantenimientos { get; set; } = new List<Mantenimiento>();

    public virtual Rutum? Ruta { get; set; }

    public virtual ICollection<Solicitud> Solicituds { get; set; } = new List<Solicitud>();

    public virtual ICollection<Rol> Rols { get; set; } = new List<Rol>();
}
