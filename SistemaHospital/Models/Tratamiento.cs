using System;
using System.Collections.Generic;

namespace SistemaHospital.Models;

public partial class Tratamiento
{
    public int IdTratamiento { get; set; }

    public int? IdHistorialMedico { get; set; }

    public int? IdTipoTratamiento { get; set; }

    public DateTime? FechaSolicitud { get; set; }

    public DateTime? FechaInicio { get; set; }

    public DateTime? FechaFinalizacion { get; set; }

    public string? Observaciones { get; set; }

    public virtual HistorialMedico? IdHistorialMedicoNavigation { get; set; }

    public virtual TipoTratamiento? IdTipoTratamientoNavigation { get; set; }
}
