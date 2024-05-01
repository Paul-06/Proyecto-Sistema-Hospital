using System;
using System.Collections.Generic;

namespace SistemaHospital.Models;

public partial class Paciente
{
    public int IdPaciente { get; set; }

    public int? IdPersona { get; set; }

    public virtual ICollection<Citum> Cita { get; set; } = new List<Citum>();

    public virtual ICollection<Factura> Facturas { get; set; } = new List<Factura>();

    public virtual ICollection<HistorialMedico> HistorialMedicos { get; set; } = new List<HistorialMedico>();

    public virtual Persona? IdPersonaNavigation { get; set; }
}
