using System;
using System.Collections.Generic;

namespace SistemaHospital.Models;

public partial class TipoEmpleado
{
    public int IdTipoEmpleado { get; set; }

    public string? Nombre { get; set; }

    public string? Descripcion { get; set; }

    public virtual ICollection<Empleado> Empleados { get; set; } = new List<Empleado>();
}
