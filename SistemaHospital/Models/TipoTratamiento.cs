using System;
using System.Collections.Generic;

namespace SistemaHospital.Models;

public partial class TipoTratamiento
{
    public int IdTipoTratamiento { get; set; }

    public string? Nombre { get; set; }

    public string? Descripcion { get; set; }

    public virtual ICollection<Tratamiento> Tratamientos { get; set; } = new List<Tratamiento>();
}
