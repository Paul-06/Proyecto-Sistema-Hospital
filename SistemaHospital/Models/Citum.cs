using System;
using System.Collections.Generic;

namespace SistemaHospital.Models;

public partial class Citum
{
    public int IdCita { get; set; }

    public int? IdPaciente { get; set; }

    public int? IdEmpleado { get; set; }

    public DateTime? FechaHora { get; set; }

    public int? IdEspecialidad { get; set; }

    public string? Estado { get; set; }

    public string? MotivoConsulta { get; set; }

    public virtual Empleado? IdEmpleadoNavigation { get; set; }

    public virtual Especialidad? IdEspecialidadNavigation { get; set; }

    public virtual Paciente? IdPacienteNavigation { get; set; }

    public virtual ICollection<Recetum> Receta { get; set; } = new List<Recetum>();
}
