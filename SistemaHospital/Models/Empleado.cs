using System;
using System.Collections.Generic;

namespace SistemaHospital.Models;

public partial class Empleado
{
    public int IdEmpleado { get; set; }

    public int? IdPersona { get; set; }

    public int? IdTipoEmpleado { get; set; }

    public int? IdEspecialidad { get; set; }

    public int? IdCargo { get; set; }

    public virtual ICollection<Citum> Cita { get; set; } = new List<Citum>();

    public virtual Cargo? IdCargoNavigation { get; set; }

    public virtual Especialidad? IdEspecialidadNavigation { get; set; }

    public virtual Persona? IdPersonaNavigation { get; set; }

    public virtual TipoEmpleado? IdTipoEmpleadoNavigation { get; set; }
}
