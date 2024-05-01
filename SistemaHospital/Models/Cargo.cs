using System;
using System.Collections.Generic;

namespace SistemaHospital.Models;

public partial class Cargo
{
    public int IdCargo { get; set; }

    public string? Nombre { get; set; }

    public string? Descripcion { get; set; }

    public virtual ICollection<Empleado> Empleados { get; set; } = new List<Empleado>();
}
