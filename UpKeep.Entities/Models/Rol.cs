using System;
using System.Collections.Generic;

namespace UpKeep.Data.Models;

public partial class Rol
{
    public int RolId { get; set; }

    public string RolDescripcion { get; set; } = null!;

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
