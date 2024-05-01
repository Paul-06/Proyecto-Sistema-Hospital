using System;
using System.Collections.Generic;

namespace SistemaHospital.Models;

public partial class TipoPago
{
    public int IdTipoPago { get; set; }

    public string? Descripcion { get; set; }

    public virtual ICollection<Pago> Pagos { get; set; } = new List<Pago>();
}
