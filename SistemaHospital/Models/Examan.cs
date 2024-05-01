using System;
using System.Collections.Generic;

namespace SistemaHospital.Models;

public partial class Examan
{
    public int IdExamen { get; set; }

    public int? IdHistorialMedico { get; set; }

    public DateTime? FechaSolicitud { get; set; }

    public DateTime? FechaAplicacion { get; set; }

    public string? Observaciones { get; set; }

    public int? IdTipoExamen { get; set; }

    public virtual HistorialMedico? IdHistorialMedicoNavigation { get; set; }

    public virtual TipoExaman? IdTipoExamenNavigation { get; set; }

    public virtual ICollection<Resultado> Resultados { get; set; } = new List<Resultado>();
}
