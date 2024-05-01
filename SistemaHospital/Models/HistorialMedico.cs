using System;
using System.Collections.Generic;

namespace SistemaHospital.Models;

public partial class HistorialMedico
{
    public int IdHistorialMedico { get; set; }

    public int? IdPaciente { get; set; }

    public virtual ICollection<Examan> Examen { get; set; } = new List<Examan>();

    public virtual Paciente? IdPacienteNavigation { get; set; }

    public virtual ICollection<Tratamiento> Tratamientos { get; set; } = new List<Tratamiento>();
}
