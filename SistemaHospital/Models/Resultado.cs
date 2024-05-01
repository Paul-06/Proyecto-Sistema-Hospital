using System;
using System.Collections.Generic;

namespace SistemaHospital.Models;

public partial class Resultado
{
    public int IdResultado { get; set; }

    public int? IdExamen { get; set; }

    public DateTime? FechaEntrega { get; set; }

    public string? Descripcion { get; set; }

    public virtual Examan? IdExamenNavigation { get; set; }
}
