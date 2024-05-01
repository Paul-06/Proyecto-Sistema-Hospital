using System;
using System.Collections.Generic;

namespace SistemaHospital.Models;

public partial class TipoExaman
{
    public int IdTipoExamen { get; set; }

    public string? Nombre { get; set; }

    public string? Descripcion { get; set; }

    public virtual ICollection<Examan> Examen { get; set; } = new List<Examan>();
}
