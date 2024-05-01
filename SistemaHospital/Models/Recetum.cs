using System;
using System.Collections.Generic;

namespace SistemaHospital.Models;

public partial class Recetum
{
    public int RecetaId { get; set; }

    public int? IdCita { get; set; }

    public DateTime? FechaPrescripcion { get; set; }

    public string? Instrucciones { get; set; }

    public virtual Citum? IdCitaNavigation { get; set; }
}
