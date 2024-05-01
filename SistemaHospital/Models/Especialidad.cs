using System;
using System.Collections.Generic;

namespace SistemaHospital.Models;

public partial class Especialidad
{
    public int IdEspecialidad { get; set; }

    public string? Nombre { get; set; }

    public string? Descripcion { get; set; }

    public virtual ICollection<Citum> Cita { get; set; } = new List<Citum>();

    public virtual ICollection<Empleado> Empleados { get; set; } = new List<Empleado>();
}
